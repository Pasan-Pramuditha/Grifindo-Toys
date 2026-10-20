using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using ComponentFactory.Krypton.Toolkit;

namespace Grifindo_Toys
{
    public partial class Bonus : KryptonForm
    {
        int Emp_ID, Amount;
        string Name;
        SqlConnection con = new SqlConnection(DbConfig.ConnectionString);
        public Bonus()
        {
            InitializeComponent();
            DataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            LoadGridView();
            GetEmployeesID();
        }

        private void LoadElement()
        {
            Emp_ID = int.Parse(txtEmp_IDb.Text);
            Name = txtNameb.Text;
            Amount = int.Parse(txtAmount.Text);

        }

        private void Clear()
        {
            txtEmp_IDb.ResetText();
            txtNameb.Clear();
            txtAmount.Clear();
            txtEmp_IDb.Focus();
        }

        //LoadGridView
        private void LoadGridView()
        {
            try
            {
                string query = "SELECT * FROM Bonus_Tbl";
                {
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    DataGridView2.DataSource = ds.Tables[0].DefaultView;
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

        private void GetEmployeesID()
        {

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            using (SqlCommand cmd = new SqlCommand("SELECT Emp_ID FROM Employee_Tbl", con))
            {
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Emp_ID", typeof(int));
                    dt.Load(rdr);

                    txtEmp_IDb.ValueMember = "Emp_ID";
                    txtEmp_IDb.DataSource = dt;
                }
            }

            con.Close();
        }

        private void txtEmp_IDb_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetEmployeesName();
        }



        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            try
            {
                LoadElement();
                {
                    con.Open();
                    string query = "INSERT INTO Bonus_Tbl (Emp_ID, Name, Amount) VALUES (@Emp_ID, @Name, @Amount)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        
                        cmd.Parameters.AddWithValue("@Emp_ID", Emp_ID);
                        cmd.Parameters.AddWithValue("@Name", Name);
                        cmd.Parameters.AddWithValue("@Amount", Amount);

                        cmd.ExecuteNonQuery();

                        
                        MessageBox.Show("Saved successfully!");

                        con.Close();
                        LoadGridView();
                        Clear();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("A SQL error occurred: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            try
            {
                LoadElement();
                {
                    con.Open();
                    string query = "UPDATE Bonus_Tbl SET Name = @Name, Amount = @Amount WHERE Emp_ID = @Emp_ID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Emp_ID", Emp_ID);
                        cmd.Parameters.AddWithValue("@Name", Name);
                        cmd.Parameters.AddWithValue("@Amount", Amount);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Updated successfully!");

                        con.Close();
                        LoadGridView();
                        Clear();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("A SQL error occurred: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            {
                DialogResult result = MessageBox.Show("Do you want to delete this?", "Warning message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    int Emp_ID;
                    if (!int.TryParse(txtEmp_IDb.Text, out Emp_ID))
                    {
                        MessageBox.Show("Please enter a valid Employee ID.");
                        return;
                    }

                    using (SqlConnection con = new SqlConnection(DbConfig.ConnectionString))

                    {
                        string Delete = $"DELETE FROM Bonus_Tbl WHERE Emp_ID = '{Emp_ID}'";


                        try
                        {
                            con.Open();

                            using (SqlCommand command = new SqlCommand(Delete, con))
                            {
                                command.Parameters.AddWithValue("@EmployeeID", Emp_ID);
                                int rowsAffected = command.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Deleted successfully!");
                                }
                                else
                                {
                                    MessageBox.Show("No records found for deletion.");
                                }
                            }
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show("An error occurred: " + ex.Message);
                        }
                        LoadGridView();
                        Clear();
                    }
                }
            }

        }   

        private void GetEmployeesName()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            string query = "SELECT Name FROM Employee_Tbl WHERE Emp_Id = @Emp_Id";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Emp_Id", txtEmp_IDb.SelectedValue.ToString());

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        txtNameb.Text = dr["Name"].ToString();
                    }
                }
            }

            con.Close();
        }
    }
}
