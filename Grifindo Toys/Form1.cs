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
using ComponentFactory.Krypton.Toolkit;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Grifindo_Toys
{
    public partial class Form1 : KryptonForm
    {
        SqlConnection con = new SqlConnection(DbConfig.ConnectionString);

        public Form1()
        {
            InitializeComponent();
            
        }

        private void Clear()
        {

            texPassword.Clear();
            texUsername.Clear();
            texUsername.Focus();

        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            
                string username = texUsername.Text;
                string password = texPassword.Text;

                string query = "SELECT COUNT(*) FROM Login_Tbl WHERE User_Name = @username AND Password = @password";


                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        int result = (int)cmd.ExecuteScalar();

                        if (result > 0)
                        {
                            MessageBox.Show("Login successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Dashboardcs log = new Dashboardcs();
                            this.Hide();
                            log.Show();
                        }
                        else
                        {
                            MessageBox.Show("Login failed. Please check your username and password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        con.Close();
                        Clear();
                    }
                }
            }

        

        private void kryptonButton2_Click_2(object sender, EventArgs e)
        {
            Registration f1 = new Registration();
            this.Hide();
            f1.Show();
        }

       

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked==true)
            {
                texPassword.UseSystemPasswordChar = false;
            }
            else
            {

                texPassword.UseSystemPasswordChar = true;
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void texPassword_TextChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                texPassword.UseSystemPasswordChar = false;
            }
            else
            {

                texPassword.UseSystemPasswordChar = true;
            }
        }
    }
 }

