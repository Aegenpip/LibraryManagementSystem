using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    public partial class frmViewStudents : MetroFramework.Forms.MetroForm
    {
        MySqlConnection con = new MySqlConnection($"SERVER=localhost;DATABASE=library;UID=root;PWD=2001e-U1995");

        public frmViewStudents()
        {
            InitializeComponent();
        }

        private void frmViewStudents_Load(object sender, EventArgs e)
        {
            con.Open();

            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from students";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            dgwViewStudents.DataSource = dt;

            con.Close();
        }

        private void txtName_KeyUp(object sender, KeyEventArgs e)
        {
            con.Open();

            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from students where studentName like ('%" + txtSearch.Text + "%')";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            dgwViewStudents.DataSource = dt;

            con.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int i;
            i = Convert.ToInt32(dgwViewStudents.SelectedCells[0].Value.ToString());

            con.Open();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update students set studentName='" + txtName.Text + "',enrollmentNumber='" + txtEnNum.Text + "',department='" + txtDepartment.Text + "'," +
                                "studentContact='" + txtContact.Text + "',studentMail='" + txtMail.Text + "' where studentID=" + i + "";
            cmd.ExecuteNonQuery();

            con.Close();

            MessageBox.Show("Book Updated Succesfully");
        }

        private void dgwViewStudents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            pnlUpdate.Visible = true;

            int i;
            i = Convert.ToInt32(dgwViewStudents.SelectedCells[0].Value.ToString());

            con.Open();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from students where studentID=" + i + "";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                txtName.Text = dr["studentName"].ToString();
                txtEnNum.Text = dr["enrollmentNumber"].ToString();
                txtDepartment.Text = dr["department"].ToString();
                txtContact.Text =dr["studentContact"].ToString();
                txtMail.Text = dr["studentMail"].ToString();
            }

            con.Close();
        }
       
    }
}
