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
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;
using ComponentFactory.Krypton.Toolkit;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Grifindo_Toys
{
    public partial class Registration : KryptonForm
    {
        int Admin_ID;
        string User_Name, Password, ComPassword, F_Name, L_Name, Email, Contect;
        DateTime DOB;

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            this.Hide();
            f1.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                TexPassword.UseSystemPasswordChar = false;
            }
            else
            {

                TexPassword.UseSystemPasswordChar = true;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                TexComUsername.UseSystemPasswordChar = false;
            }
            else
            {

                TexComUsername.UseSystemPasswordChar = true;
            }
        }

        private void TexPassword_TextChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                TexPassword.UseSystemPasswordChar = false;
            }
            else
            {

                TexPassword.UseSystemPasswordChar = true;
            }
        }

        private void TexComUsername_TextChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                TexComUsername.UseSystemPasswordChar = false;
            }
            else
            {

                TexComUsername.UseSystemPasswordChar = true;
            }
        }

        SqlConnection con = new SqlConnection(DbConfig.ConnectionString);


        //Delete 
        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            {
                DialogResult result = MessageBox.Show("Do you want to delete this?", "Warning message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    int AdminID;
                    if (!int.TryParse(TexAdminID.Text, out AdminID))
                    {
                        MessageBox.Show("Please enter a valid Admin ID.");
                        return;
                    }

                    using (SqlConnection con = new SqlConnection(DbConfig.ConnectionString))

                    {
                        string deleteQuery = "DELETE FROM Login_Tbl WHERE Admin_ID = @AdminID";

                        try
                        {
                            con.Open();

                            using (SqlCommand command = new SqlCommand(deleteQuery, con))
                            {
                                command.Parameters.AddWithValue("@AdminID", AdminID);
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
                        
                        Clear();
                    }
                }
            }
        }

        //INSERT

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            try
            {
                LoadElement();
                con.Open();
                string insert = "insert into Login_Tbl (User_Name, Password, ComPassword,Admin_ID, F_Name, L_Name,Email, DOB, Contect) " +
                    "values ('" + User_Name + "','" + Password + "','" + ComPassword + "','" + Admin_ID + "','" + F_Name + "','" + L_Name +
                    "','" + Email + "','" + DOB + "','" + Contect + "')";
                SqlCommand cmd = new SqlCommand(insert, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Saved successfully!");
                Clear();
            }
            catch (SqlException)
            {
                MessageBox.Show("Alredy have Enter Id please try agen");
            }
            catch (Exception)
            {
                MessageBox.Show("No data entered try agen");
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        

        public Registration()
        {
            InitializeComponent();
        }

        private void LoadElement()
        {
            Admin_ID = int.Parse(TexAdminID.Text);
            Email = TexEmail.Text;
            F_Name = TexF_name.Text;
            L_Name = TexL_name.Text;
            DOB = DateTimePicker1.Value;
            Contect = TexCON.Text;
            User_Name = TexUsername.Text;
            Password = TexPassword.Text;
            ComPassword = TexComUsername.Text;


        }
        private void Clear()
        {
            TexAdminID.Clear();
            TexEmail.Clear();
            TexF_name.Clear();
            TexL_name.Clear();
            DateTimePicker1.ResetText();
            TexCON.Clear();
            TexUsername.Clear();
            TexPassword.Clear();
            TexComUsername.Clear();
            TexAdminID.Focus();

        }
    }

}

