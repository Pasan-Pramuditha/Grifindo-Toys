using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using ComponentFactory.Krypton.Toolkit;


namespace Grifindo_Toys
{
    public partial class Branch : KryptonForm
    {
        int Branch_ID;
        String Branch_Name, Location, Address, Contact_No;

        SqlConnection con = new SqlConnection(DbConfig.ConnectionString);

        public Branch()
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
                string query = "SELECT * FROM [Branch_Tbl]";

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


        private void label4_Click(object sender, EventArgs e)
        {
            Issue_Salary IS = new Issue_Salary();
            this.Hide();
            IS.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Setting S = new Setting();
            this.Hide();
            S.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Employee E = new Employee();
            this.Hide();
            E.Show();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Dashboardcs E = new Dashboardcs();
            this.Hide();
            E.Show();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            Clear();
        }

        //Update 
        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            try
            {
                LoadElement();

                string updateQuery = "UPDATE [Branch_Tbl] " +"SET Branch_Name = @Branch_Name, Location = @Location, Address = @Address, Contact_No = @Contact_No " + "WHERE Branch_ID = @Branch_ID";

                using (SqlConnection con = new SqlConnection(DbConfig.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@Branch_ID", Branch_ID);
                        cmd.Parameters.AddWithValue("@Branch_Name", Branch_Name);
                        cmd.Parameters.AddWithValue("@Location", Location);
                        cmd.Parameters.AddWithValue("@Address", Address);
                        cmd.Parameters.AddWithValue("@Contact_No", Contact_No);

                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Updated successfully!");
                        }
                        else
                        {
                            MessageBox.Show("Update failed. Please check if the Branch_ID exists.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }

                Clear();
                LoadGridView();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("A database error occurre: " + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception )
            {
                MessageBox.Show("Note that there is no Branch ID, please enter the ID ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        //Delete 
        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to delete this?", "Warning message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                int BranchID;
                if (!int.TryParse(txt_ID.Text, out BranchID))
                {
                    MessageBox.Show("Please enter a valid  Branch ID.");
                    return;
                }

                using (SqlConnection con = new SqlConnection(DbConfig.ConnectionString))

                {
                    string deleteQuery = "DELETE FROM [Branch_Tbl] WHERE Branch_ID = @BranchID";

                    try
                    {
                        con.Open();

                        using (SqlCommand command = new SqlCommand(deleteQuery, con))
                        {
                            command.Parameters.AddWithValue("@BranchID", BranchID);
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

        //Search
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow row = DataGridView1.Rows[e.RowIndex];

                txt_ID.Text = row.Cells["Branch_ID"].Value.ToString();
                txt_Name.Text = row.Cells["Branch_Name"].Value.ToString();
                Text_Location.Text = row.Cells["Location"].Value.ToString();
                tex_Addres.Text = row.Cells["Address"].Value.ToString();
                txt_Contact.Text = row.Cells["Contact_No"].Value.ToString();
                
                
            }
        }

        private void LoadElement()
        {
            Branch_ID = int.Parse(txt_ID.Text);
            Branch_Name = txt_Name.Text;
            Location = Text_Location.Text;
            Address = tex_Addres.Text;
            Contact_No = txt_Contact.Text;

        }

        //Clear
        private void Clear()
        {
            txt_ID.Clear();
            txt_Name.Clear();
            Text_Location.ResetText();
            tex_Addres.Clear();
            txt_Contact.ResetText();
            txt_ID.Focus();


        }

        //Save

        private void kryptonButton6_Click(object sender, EventArgs e)
        {
            try
            {
                LoadElement();


                string insertQuery = "INSERT INTO [Branch_Tbl] (Branch_ID, Branch_Name, Location, Address, Contact_No) " + "VALUES (@Branch_ID, @Branch_Name, @Location, @Address, @Contact_No)";

                using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                {
                    cmd.Parameters.AddWithValue("@Branch_ID", Branch_ID);
                    cmd.Parameters.AddWithValue("@Branch_Name", Branch_Name);
                    cmd.Parameters.AddWithValue("@Location", Location);
                    cmd.Parameters.AddWithValue("@Address", Address);
                    cmd.Parameters.AddWithValue("@Contact_No", Contact_No);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Saved successfully!");
                }

                Clear();
                LoadGridView();
            }
            catch (SqlException)
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
    }
}
