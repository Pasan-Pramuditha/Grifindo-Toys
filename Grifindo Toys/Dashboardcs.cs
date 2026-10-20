using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using Grifindo_Toys;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace Grifindo_Toys
{
    public partial class Dashboardcs : KryptonForm
    {
        SqlConnection con = new SqlConnection(DbConfig.ConnectionString);
        public Dashboardcs()
        {
            InitializeComponent();
            CountEmployee();
            CountManager();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            Employee f1 = new Employee();
            this.Hide();
            f1.Show();
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            Issue_Salary f1 = new Issue_Salary();
            this.Hide();
            f1.Show();
        }
        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            Branch f1 = new Branch();
            this.Hide();
            f1.Show();
        }
        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            Setting f1 = new Setting();
            this.Hide();
            f1.Show();
        }

        private void CountEmployee()
        {
            try
            {
                con.Open();
                SqlDataAdapter sad = new SqlDataAdapter("Select Count(*) from Employee_Tbl", con);
                DataTable dt = new DataTable();
                sad.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    label3.Text = dt.Rows[0][0].ToString();
                }
                else
                {
                    label3.Text = "0";
                }
            }
            catch (SqlException ex)
            {
                
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void CountManager()
        {
            try
            {
                String Pos = "Manager";
                con.Open();
                SqlDataAdapter sad = new SqlDataAdapter("Select Count(*) from Employee_Tbl where Position = '" + Pos + "'", con);
                DataTable dt = new DataTable();
                sad.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    label5.Text = dt.Rows[0][0].ToString();
                }
                else
                {
                    label5.Text = "0";
                }
            }
            catch (SqlException ex)
            {
                
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to Logout", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Form1 L = new Form1();
                this.Hide();
                L.Show();
            }
            else
                this.Show();

        }
    }

}
