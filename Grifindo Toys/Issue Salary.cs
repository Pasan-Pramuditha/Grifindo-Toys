using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;
using ComponentFactory.Krypton.Toolkit;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Grifindo_Toys
{
    public partial class Issue_Salary : KryptonForm
    {
        int Salary_ID, Setting_ID, Emp_ID, Present, Absent, OT_Hours, Allowances;
        float Net_Salary;
        
        SqlConnection con = new SqlConnection(DbConfig.ConnectionString);
        public Issue_Salary()
        {
            InitializeComponent();
            GetEmployeesID();
            LoadGridView();
        }


        //LoadElement
        private void LoadElement()
        {
            Salary_ID = int.Parse(ComboSalaryId.Text);
            Setting_ID = int.Parse(ComboSetting.Text);
            Emp_ID = int.Parse(ComboBoxEmp.Text);
            Present = int.Parse(texPresent.Text);
            Absent = int.Parse(txtAbsent.Text);
            OT_Hours = int.Parse(txtOT.Text);
            Allowances = int.Parse(txtAllow.Text);
            Net_Salary = float.Parse(txtNetSalary.Text);


        }

        //Calulation

        float gross_pay = 0;

        private void kryptonButton7_Click(object sender, EventArgs e)
        {
            int m_c_id = 0;
            int absent_days = 0, ot_hours = 0, allowances = 0;
            float ot_rate = 0, gov_tax = 0, basesalary = 0;
            float no_pay, base_pay, gross_pay;

            try
            {
                if (int.TryParse(ComboBoxEmp.SelectedValue?.ToString(), out int empId))
                {
                    using (SqlConnection Con = new SqlConnection(DbConfig.ConnectionString))
                    {
                        Con.Open();

                        // Get monthly salary from employee table
                        using (SqlCommand cmd = Con.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "SELECT * FROM Employee_Tbl WHERE Emp_ID = @Emp_ID";
                            cmd.Parameters.Add(new SqlParameter("@Emp_ID", SqlDbType.Int)).Value = empId;

                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                DataTable dt = new DataTable();
                                da.Fill(dt);

                                if (dt.Rows.Count > 0)
                                {
                                    basesalary = Convert.ToSingle(dt.Rows[0]["Monthly_Salary"]);
                                }
                                else
                                {
                                    MessageBox.Show("No records found for the selected employee ID.");
                                    return;
                                }
                            }
                        }

                        // Get salary details from Salary table
                        using (SqlCommand cmd = Con.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "SELECT * FROM Salary_Tbl WHERE Emp_ID = @Emp_ID";
                            cmd.Parameters.Add(new SqlParameter("@Emp_ID", SqlDbType.Int)).Value = empId;

                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                DataTable dt = new DataTable();
                                da.Fill(dt);

                                if (dt.Rows.Count > 0)
                                {
                                    ot_hours = Convert.ToInt32(dt.Rows[0]["OT_Hours"]);
                                    absent_days = Convert.ToInt32(dt.Rows[0]["Absent"]);
                                    allowances = Convert.ToInt32(dt.Rows[0]["Allowances"]);
                                    m_c_id = Convert.ToInt32(dt.Rows[0]["Setting_ID"]);
                                }
                                else
                                {
                                    MessageBox.Show("No records found in the salary table for the selected employee ID.");
                                    return;
                                }
                            }
                        }

                        // Get settings based on Setting_ID
                        using (SqlCommand cmd = Con.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "SELECT * FROM Setting_Tbl WHERE Setting_ID = @Setting_ID";
                            cmd.Parameters.Add(new SqlParameter("@Setting_ID", SqlDbType.Int)).Value = m_c_id;

                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                DataTable dt = new DataTable();
                                da.Fill(dt);

                                if (dt.Rows.Count > 0)
                                {
                                    ot_rate = Convert.ToSingle(dt.Rows[0]["Ot_rate"]);
                                    gov_tax = Convert.ToSingle(dt.Rows[0]["Tax_rate"]);
                                }
                                else
                                {
                                    MessageBox.Show("No settings found for the selected Setting ID.");
                                    return;
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a valid employee ID.");
                    return;
                }

                // Use UI field values for calculation (override DB values with screen values)
                if (int.TryParse(txtAbsent.Text, out int uiAbsent)) absent_days = uiAbsent;
                if (int.TryParse(txtOT.Text, out int uiOT)) ot_hours = uiOT;
                if (float.TryParse(txtAllow.Text, out float uiAllow)) allowances = (int)uiAllow;

                // Net Salary Formula:
                // Daily Rate = Basic Salary / 30
                // No Pay = Daily Rate * Absent Days
                // OT Pay = OT Hours * OT Rate
                // Gross = Basic + Allowances + OT Pay
                // Net = Gross - No Pay - (Gross * Tax%)
                float daily_rate = basesalary / 30;
                no_pay = daily_rate * absent_days;
                float ot_pay = ot_hours * ot_rate;
                base_pay = basesalary + allowances + ot_pay;
                gross_pay = base_pay - no_pay - (base_pay * gov_tax / 100);

                txtNetSalary.Text = gross_pay.ToString("F2");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        //counting the days
        private void texPresent_TextChanged(object sender, EventArgs e)
        {
            int preValue;
            if (int.TryParse(texPresent.Text, out preValue))
            {
                if (preValue <= 30)
                {
                    int result = 30 - preValue;
                    txtAbsent.Text = result.ToString();
                }
                else
                {
                    MessageBox.Show("Please enter a number less than 30.");
                }
            }
            else
            {
                
            }
        }

        //Clear
        private void Clear()
        {
            ComboSalaryId.ResetText();
            ComboSetting.ResetText();
            ComboBoxEmp.ResetText();
            texPresent.Clear();
            txtAbsent.Clear();
            txtOT.Clear();
            txtAllow.Clear();
            txtOT.Clear();
            txtNetSalary.Clear();
            ComboSalaryId.Focus();


        }

        //Search
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow row = DataGridView1.Rows[e.RowIndex];

                ComboSalaryId.Text = row.Cells["Salary_ID"].Value.ToString();
                ComboSetting.Text = row.Cells["Setting_ID"].Value.ToString();
                ComboBoxEmp.Text = row.Cells["Emp_ID"].Value.ToString();
                texPresent.Text = row.Cells["Present"].Value.ToString();
                txtAbsent.Text = row.Cells["Absent"].Value.ToString();
                txtOT.Text = row.Cells["OT_Hours"].Value.ToString();
                txtAllow.Text = row.Cells["Allowances"].Value.ToString();
            }
        }

        //Update
        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(texPresent.Text) && !string.IsNullOrEmpty(txtAbsent.Text) && !string.IsNullOrEmpty(txtOT.Text) && !string.IsNullOrEmpty(txtAllow.Text))
            {
                try
                {
                    using (SqlConnection Con = new SqlConnection(DbConfig.ConnectionString))
                    {
                        Con.Open();

                        using (SqlCommand cmd = Con.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;

                            cmd.CommandText = "UPDATE [Salary_Tbl] SET " +
                                              "Setting_ID = @Setting_ID, " +
                                              "Emp_ID = @Emp_ID, " +
                                              "Present = @Present, " +
                                              "Absent = @Absent, " +
                                              "OT_Hours = @OT_Hours, " +
                                              "Allowances = @Allowances " +
                                              "WHERE Salary_ID = @Salary_ID";

                            

                            cmd.Parameters.AddWithValue("@Setting_ID", ComboSetting.Text);
                            cmd.Parameters.AddWithValue("@Emp_ID", ComboBoxEmp.Text);
                            cmd.Parameters.AddWithValue("@Present", texPresent.Text);
                            cmd.Parameters.AddWithValue("@Absent", txtAbsent.Text);
                            cmd.Parameters.AddWithValue("@OT_Hours", txtOT.Text);
                            cmd.Parameters.AddWithValue("@Allowances", txtAllow.Text);
                            cmd.Parameters.AddWithValue("@Salary_ID", ComboSalaryId.Text);

                            cmd.ExecuteNonQuery();
                            LoadGridView();
                            Clear();
                        }
                    }
                    MessageBox.Show("Salary Details Updated Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show($"An SQL error occurred: {sqlEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Fill in all the fields that can't be empty.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
           
            
                DialogResult result = MessageBox.Show("Do you want to delete this?", "Warning message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    int Salary_ID;
                    if (!int.TryParse(ComboSalaryId.Text, out Salary_ID))
                    {
                        MessageBox.Show("Please enter a valid Employee ID.");
                        return;
                    }

                    using (SqlConnection con = new SqlConnection(DbConfig.ConnectionString))
                    {
                        con.Open();

                        SqlTransaction transaction = con.BeginTransaction();

                        try
                        {

                            string deleteAttendance = "DELETE FROM Salary_Tbl WHERE Salary_ID = @Salary_ID";
                            using (SqlCommand cmd = new SqlCommand(deleteAttendance, con, transaction))
                            {
                                cmd.Parameters.AddWithValue("@Salary_ID", Salary_ID);
                                cmd.ExecuteNonQuery();
                            }


                            string deleteEmployee = "DELETE FROM Salary_Tbl WHERE Salary_ID = @Salary_ID";
                            using (SqlCommand cmd = new SqlCommand(deleteEmployee, con, transaction))
                            {
                                cmd.Parameters.AddWithValue("@Salary_ID", Salary_ID);
                                int rowsAffected = cmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Deleted successfully!");
                                }
                                else
                                {
                                    MessageBox.Show("Deleted successfully.");
                                }
                            }

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("An error occurred: " + ex.Message);
                        }
                        finally
                        {
                            con.Close();
                        }

                        LoadGridView();
                        Clear();
                    }
                }
            
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {
            Dashboardcs H = new Dashboardcs();
            this.Hide();
            H.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Employee E = new Employee();
            this.Hide();
            E.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Setting S = new Setting();
            this.Hide();
            S.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Branch B = new Branch();
            this.Hide();
            B.Show();
        }





        //GetEmployeesID
        private void GetEmployeesID()
        {
         
            try
            {
                using (SqlConnection Con = new SqlConnection(DbConfig.ConnectionString))
                {
                    SqlCommand cmdc = new SqlCommand("SELECT * FROM Employee_Tbl", Con);
                    SqlDataAdapter dac = new SqlDataAdapter();
                    dac.SelectCommand = cmdc;
                    DataTable table1 = new DataTable();
                    dac.Fill(table1);

                    ComboBoxEmp.DataSource = table1;
                    ComboBoxEmp.DisplayMember = "Emp_ID";
                    ComboBoxEmp.ValueMember = "Emp_ID";
                    Con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        //GetSalary
        private void GetSalaryID()
        {

            try
            {
                using (SqlConnection Con = new SqlConnection(DbConfig.ConnectionString))
                {
                    SqlCommand cmdc = new SqlCommand("SELECT * FROM Salary_Tbl", Con);
                    SqlDataAdapter dac = new SqlDataAdapter();
                    dac.SelectCommand = cmdc;
                    DataTable table1 = new DataTable();
                    dac.Fill(table1);

                    ComboSalaryId.DataSource = table1;
                    ComboSalaryId.DisplayMember = "Salary_ID";
                    ComboSalaryId.ValueMember = "Salary_ID";
                    Con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        //GetSettingID
        private void GetSettingID()
        {

            try
            {
                using (SqlConnection Con = new SqlConnection(DbConfig.ConnectionString))
                {
                    SqlCommand cmdc = new SqlCommand("SELECT * FROM Setting_Tbl", Con);
                    SqlDataAdapter dac = new SqlDataAdapter();
                    dac.SelectCommand = cmdc;
                    DataTable table1 = new DataTable();
                    dac.Fill(table1);

                    ComboSetting.DataSource = table1;
                    ComboSetting.DisplayMember = "Setting_ID";
                    ComboSetting.ValueMember = "Setting_ID";
                    Con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        //LoadGridView
        private void LoadGridView()
        {
            try
            {
                string connectionString = DbConfig.ConnectionString;
                string query = "SELECT * FROM Salary_Tbl";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    DataGridView1.DataSource = ds.Tables[0].DefaultView;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("SQL Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Issue_Salary_Load(object sender, EventArgs e)
        {
            DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            GetEmployeesID();
            GetSettingID();
            LoadGridView();
            GetSalaryID();
        }


        //Save
        private void kryptonButton6_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(texPresent.Text) && !string.IsNullOrEmpty(txtAbsent.Text) && !string.IsNullOrEmpty(txtOT.Text) && !string.IsNullOrEmpty(txtAllow.Text))
            {
                try
                {
                    using (SqlConnection Con = new SqlConnection(DbConfig.ConnectionString))
                    {
                        Con.Open();
                        
                        using (SqlCommand cmd = Con.CreateCommand())
                        {
                            cmd.CommandType = CommandType.Text;

                            
                            cmd.CommandText = "INSERT INTO [Salary_Tbl] (Setting_ID, Emp_ID, Present, Absent, OT_Hours, Allowances) VALUES (@Setting_ID, @Emp_ID, @Present, @Absent, @OT_Hours, @Allowances)";

                            
                            cmd.Parameters.AddWithValue("@Setting_ID", ComboSetting.Text);
                            cmd.Parameters.AddWithValue("@Emp_ID", ComboBoxEmp.Text);
                            cmd.Parameters.AddWithValue("@Present", texPresent.Text);
                            cmd.Parameters.AddWithValue("@Absent", txtAbsent.Text);
                            cmd.Parameters.AddWithValue("@OT_Hours", txtOT.Text);
                            cmd.Parameters.AddWithValue("@Allowances", txtAllow.Text);
                            

                            cmd.ExecuteNonQuery();
                            LoadGridView();
                            Clear();
                        }
                    }
                    MessageBox.Show("Salary Details Added Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);


                }
                catch (SqlException sqlEx)
                {
                    
                    MessageBox.Show($"An SQL error occurred: {sqlEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
               
                MessageBox.Show("Fill in all the fields that can't be empty.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}
