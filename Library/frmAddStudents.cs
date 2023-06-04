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
    public partial class frmAddStudents : MetroFramework.Forms.MetroForm
    {
        MySqlConnection con = new MySqlConnection($"SERVER=localhost;DATABASE=library;UID=root;PWD=2001e-U1995");

        public frmAddStudents()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            con.Open();

            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType= CommandType.Text;
            cmd.CommandText = "Call addStudent('"+ txtName.Text +"','"+ txtEnNum.Text +"','"+ txtDepartment.Text +"','"+ txtContact.Text +"','"+ txtMail.Text +"')";
            cmd.ExecuteNonQuery();

            con.Close();


            MessageBox.Show("Student Added Succesfully");
        }
    }
}
