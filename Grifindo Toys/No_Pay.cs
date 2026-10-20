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
    public partial class No_Pay : KryptonForm
    {
        SqlConnection con = new SqlConnection(DbConfig.ConnectionString);
        public No_Pay()
        {
            InitializeComponent();
            GetEmployeesID();
            //GetAttendance();
        }


        //GetEmployeesName
        private void GetEmployeesName()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            string query = "SELECT Name FROM Employee_Tbl WHERE Emp_Id = @Emp_Id";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Emp_Id", txtEmp_IDno.SelectedValue.ToString());

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        txtNameno.Text = dr["Name"].ToString();
                    }
                }
            }

            con.Close();
        }

        


        //GetEmployeesID
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

                    txtEmp_IDno.ValueMember = "Emp_ID";
                    txtEmp_IDno.DataSource = dt;
                }
            }
        } 

     
        private void txtEmp_IDno_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetEmployeesName();
            //GetAttendance();


        }

        private void GetAbsent()
        {

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            using (SqlCommand cmd = new SqlCommand("SELECT Att_ID FROM Attendance_Tbl", con))
            {
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Absent", typeof(int));
                    dt.Load(rdr);

                    //txtAbsentno.ValueMember = "Absentno";
                    //txtAbsentno.DataSource = dt;
                }
            }
        }


        private void kryptonButton1_Click(object sender, EventArgs e)
        {

        }

        private void No_Pay_Load(object sender, EventArgs e)
        {

        }
    }
}
