using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;



namespace Grifindo_Toys
{
    public partial class Employee : KryptonForm
    {
        int Emp_ID, Monthly_Salary;
        string Name, Address, Gender, Position, DOB, Contact, Join_Date;
        
        SqlConnection con = new SqlConnection(DbConfig.ConnectionString);


        //Update
        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtEmp_ID.Text) || string.IsNullOrWhiteSpace(TxtName.Text) || string.IsNullOrWhiteSpace(TxtAddress.Text) ||
                string.IsNullOrWhiteSpace(ComGender.Text) || string.IsNullOrWhiteSpace(TxtDOB.Text) || string.IsNullOrWhiteSpace(ComPosition.Text))
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                LoadElement();

                string updateQuery = "UPDATE Employee_Tbl SET " +"Name = @Name, " +"Address = @Address, " +"Gender = @Gender, " +"Join_Date = @Join_Date, " +"DOB = @DOB, " +"Contact = @Contact, " +"Position = @Position, " +"Monthly_Salary = @Monthly_Salary " +"WHERE Emp_ID = @Emp_ID";

                try
                {
                    using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                    {
                        con.Open();

                        cmd.Parameters.AddWithValue("@Emp_ID", Emp_ID);
                        cmd.Parameters.AddWithValue("@Name", Name);
                        cmd.Parameters.AddWithValue("@Address", Address);
                        cmd.Parameters.AddWithValue("@Gender", Gender);
                        cmd.Parameters.AddWithValue("@Join_Date", Join_Date);
                        cmd.Parameters.AddWithValue("@DOB", DOB);
                        cmd.Parameters.AddWithValue("@Contact", Contact);
                        cmd.Parameters.AddWithValue("@Position", Position);
                        cmd.Parameters.AddWithValue("@Monthly_Salary", Monthly_Salary);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Updated successfully!");
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error occurred while updating: " + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An unexpected error occurred: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }

                Clear();
                LoadGridView();
            }
        }




        //Delete
        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to delete this?", "Warning message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                int Emp_ID;
                if (!int.TryParse(TxtEmp_ID.Text, out Emp_ID))
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
                    
                        string deleteAttendance = "DELETE FROM Employee_Tbl WHERE Emp_ID = @Emp_ID";
                        using (SqlCommand cmd = new SqlCommand(deleteAttendance, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Emp_ID", Emp_ID);
                            cmd.ExecuteNonQuery();
                        }

                      
                        string deleteEmployee = "DELETE FROM Employee_Tbl WHERE Emp_ID = @Emp_ID";
                        using (SqlCommand cmd = new SqlCommand(deleteEmployee, con, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Emp_ID", Emp_ID);
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("No records found for deletion");
                            }
                            else
                            {
                                MessageBox.Show("Deleted successfully.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void label1_Click(object sender, EventArgs e)
        {
            Dashboardcs H = new Dashboardcs();
            this.Hide();
            H.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Setting S = new Setting();
            this.Hide();
            S.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Issue_Salary I = new Issue_Salary();
            this.Hide();
            I.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Branch B = new Branch();
            this.Hide();
            B.Show();
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //Search
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow row = DataGridView1.Rows[e.RowIndex];

                TxtEmp_ID.Text = row.Cells["Emp_ID"].Value.ToString();
                TxtName.Text = row.Cells["Name"].Value.ToString();
                TxtAddress.Text = row.Cells["Address"].Value.ToString();
                ComGender.Text = row.Cells["Gender"].Value.ToString();
                TxtJ_date.Text = row.Cells["Join_Date"].Value.ToString();
                TxtDOB.Text = row.Cells["DOB"].Value.ToString();
                txtM.Text = row.Cells["Contact"].Value.ToString();
                ComPosition.Text = row.Cells["Position"].Value.ToString();
                txtMsalary.Text = row.Cells["Monthly_Salary"].Value.ToString();
            }
        }

        private void kryptonButton5_Click(object sender, EventArgs e)
        {
            Clear();
        }


        public Employee()
        {
            InitializeComponent();
            DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            LoadGridView();
        }

        private void LoadElement()
        {
            Emp_ID = int.Parse(TxtEmp_ID.Text);
            Name = TxtName.Text;
            Address = TxtAddress.Text;
            Gender = ComGender.Text;
            Join_Date = TxtJ_date.Text;
            DOB = TxtDOB.Text;
            Contact = txtM.Text;
            Position = ComPosition.Text;
            Monthly_Salary = int.Parse(txtMsalary.Text);

        }

        private void Clear()
        {
            TxtEmp_ID.Clear();
            TxtName.Clear();
            TxtAddress.Clear();
            ComGender.ResetText();
            ComPosition.ResetText();
            TxtJ_date.Clear();
            txtM.Clear(); 
            TxtDOB.Clear(); 
            txtMsalary.Clear();
            TxtEmp_ID.Focus();


        }
        //LoadGridView
        private void LoadGridView()
        {
            try
            {
                string connectionString = DbConfig.ConnectionString;
                string query = "SELECT * FROM Employee_Tbl";

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

        //Save
        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtEmp_ID.Text) || string.IsNullOrWhiteSpace(TxtName.Text) || string.IsNullOrWhiteSpace(TxtAddress.Text) ||
                string.IsNullOrWhiteSpace(ComGender.Text) || string.IsNullOrWhiteSpace(TxtDOB.Text) || string.IsNullOrWhiteSpace(ComPosition.Text))
            {
                MessageBox.Show("Missing Information", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    LoadElement();

                    // Define the SQL query with parameters
                    string insertQuery = "INSERT INTO Employee_Tbl (Emp_ID, Name, Address, Gender, Join_Date, DOB, Contact, Position, Monthly_Salary) " + "VALUES (@Emp_ID, @Name, @Address, @Gender, @Join_Date, @DOB, @Contact, @Position, @Monthly_Salary)";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                    {
                        con.Open();

                        cmd.Parameters.AddWithValue("@Emp_ID", Emp_ID);
                        cmd.Parameters.AddWithValue("@Name", Name);
                        cmd.Parameters.AddWithValue("@Address", Address);
                        cmd.Parameters.AddWithValue("@Gender", Gender);
                        cmd.Parameters.AddWithValue("@Join_Date", Join_Date);
                        cmd.Parameters.AddWithValue("@DOB", DOB);
                        cmd.Parameters.AddWithValue("@Contact", Contact);
                        cmd.Parameters.AddWithValue("@Position", Position);
                        cmd.Parameters.AddWithValue("@Monthly_Salary", Monthly_Salary);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Saved successfully!");
                    }

                    Clear();
                    LoadGridView();
                }
                catch (SqlException)
                {
                    MessageBox.Show("Already have Enter ID, please try again", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
        }


    }
}
