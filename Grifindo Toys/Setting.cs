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

    public partial class Setting : KryptonForm
    {
        int Setting_ID, Ot_rate;
        DateTime Start_date, End_date;
        float Tax_rate;
        SqlConnection con = new SqlConnection(DbConfig.ConnectionString);
        public Setting()
        {
            InitializeComponent();
            DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            LoadGridView();
        }


        //LoadGridVie
        private void LoadGridView()
        {
            try
            {
                string connectionString = DbConfig.ConnectionString;
                string query = "SELECT * FROM Setting_Tbl";

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

        private void dateTimeTime_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Value = dateTimeTime.Value.AddDays(30);
        }

        //Save
        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            try
            {
                LoadElement();

                
                string insertQuery = "INSERT INTO Setting_Tbl (Setting_ID, Start_date, End_date, Ot_rate, Tax_rate) " + "VALUES (@Setting_ID, @Start_date, @End_date, @Ot_rate, @Tax_rate)";

                using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                {
                    cmd.Parameters.AddWithValue("@Setting_ID", Setting_ID);
                    cmd.Parameters.AddWithValue("@Start_date", Start_date);
                    cmd.Parameters.AddWithValue("@End_date", End_date);
                    cmd.Parameters.AddWithValue("@Ot_rate", Ot_rate);
                    cmd.Parameters.AddWithValue("@Tax_rate", Tax_rate);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Saved successfully!");
                }

                Clear();
                LoadGridView();
            }
            catch (SqlException )
            {
                MessageBox.Show("Alredy have Enter Id please try agen", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message);
            }
            finally
            {
                
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
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

                TxtSet_ID.Text = row.Cells["Setting_ID"].Value.ToString();
                dateTimeTime.Text = row.Cells["Start_date"].Value.ToString();
                dateTimePicker1.Text = row.Cells["End_date"].Value.ToString();
                TxtOt.Text = row.Cells["Ot_rate"].Value.ToString();
                Txttax.Text = row.Cells["Tax_rate"].Value.ToString();
            }
        }

        //Update 
        private void kryptonButton2_Click(object sender, EventArgs e)
        {
                try
                {
                    LoadElement();

                    // Build the SQL update query string with parameters
                    string updateQuery = "UPDATE Setting_Tbl " +
                                         "SET Start_date = @Start_date, End_date = @End_date, Ot_rate = @Ot_rate, Tax_rate = @Tax_rate " +
                                         "WHERE Setting_ID = @Setting_ID";

                    // Using block to ensure proper resource management
                    using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                    {
                        // Add parameters to the command
                        cmd.Parameters.AddWithValue("@Setting_ID", Setting_ID);
                        cmd.Parameters.AddWithValue("@Start_date", Start_date);
                        cmd.Parameters.AddWithValue("@End_date", End_date);
                        cmd.Parameters.AddWithValue("@Ot_rate", Ot_rate);
                        cmd.Parameters.AddWithValue("@Tax_rate", Tax_rate);

                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Updated successfully!");
                        }
                        else
                        {
                            MessageBox.Show("No record found with the provided Setting_ID.");
                        }
                    }

                    Clear();
                    LoadGridView();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("An error occurred while updating the data: " + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An unexpected error occurred: " + ex.Message);
                }
                finally
                {
                    // Ensure the connection is closed in the finally block
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            

        }

        // Clear
        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void label1_Click(object sender, EventArgs e)
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

        
        //Delete
        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to delete this?", "Warning message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (!int.TryParse(TxtSet_ID.Text, out int Setting_ID))
                {
                    MessageBox.Show("Please enter a valid Setting ID.");
                    return;
                }

                string connectionString = DbConfig.ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction();

                    try
                    {
                        // Deleting from Setting_Tbl
                        string deleteSetting = "DELETE FROM Setting_Tbl WHERE Setting_ID = @Setting_ID";
                        using (SqlCommand cmdSetting = new SqlCommand(deleteSetting, con, transaction))
                        {
                            cmdSetting.Parameters.AddWithValue("@Setting_ID", Setting_ID);
                            cmdSetting.ExecuteNonQuery();
                        }

                        // Deleting from Employee_Tbl
                        string deleteEmployee = "DELETE FROM Employee_Tbl WHERE Emp_ID = @Emp_ID";
                        using (SqlCommand cmdEmployee = new SqlCommand(deleteEmployee, con, transaction))
                        {
                            cmdEmployee.Parameters.AddWithValue("@Emp_ID", Setting_ID);
                            int rowsAffected = cmdEmployee.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Deleted successfully!");
                            }
                            else
                            {
                                MessageBox.Show("No records found for deletion.");
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

       
        private void LoadElement()
        {
            Setting_ID = int.Parse(TxtSet_ID.Text);
            Ot_rate = int.Parse(TxtOt.Text);
            Start_date = dateTimeTime.Value;
            End_date = dateTimePicker1.Value;
            Tax_rate = float.Parse(Txttax.Text);
            
        }

        //Clear
        private void Clear()
        {
            TxtSet_ID.Clear();
            TxtOt.Clear();
            dateTimeTime.ResetText();
            dateTimePicker1.ResetText();
            Txttax.ResetText();
            TxtSet_ID.Focus();


        }
    }
}
