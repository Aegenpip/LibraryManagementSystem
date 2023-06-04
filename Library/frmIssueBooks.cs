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
    public partial class frmIssueBooks : MetroFramework.Forms.MetroForm
    {
        MySqlConnection con = new MySqlConnection($"SERVER=localhost;DATABASE=library;UID=root;PWD=2001e-U1995");
        String studentID;
        String bookID;

        public frmIssueBooks()
        {
            InitializeComponent();
        }

        private void frmIssueBooks_Load(object sender, EventArgs e)
        {
            if(con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
        }

        private void btnSearchStudent_Click(object sender, EventArgs e)
        {
            int i = 0;

            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from students where enrollmentNumber='"+ txtEnrollment.Text +"'";
            cmd.ExecuteNonQuery();  
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            i = Convert.ToInt32(dt.Rows.Count.ToString());

            if(i ==0)
            {
                MessageBox.Show("this enrollment number not found");
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    studentID = dr["studentID"].ToString();
                    txtStudentName.Text = dr["studentName"].ToString();
                    txtDepartment.Text = dr["department"].ToString();
                    txtContact.Text = dr["studentContact"].ToString();
                    txtMail.Text = dr["studentMail"].ToString();
                }
            }
        }

        private void txtBookName_KeyUp(object sender, KeyEventArgs e)
        {
            int count = 0;

            lBoxBookName.Items.Clear();

            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from books where bookName like ('%" + txtBookName.Text + "%')";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            count = Convert.ToInt32(dt.Rows.Count.ToString());

            if (count > 0)
            {
                lBoxBookName.Visible = true;
                foreach (DataRow dr in dt.Rows)
                {
                    lBoxBookName.Items.Add(dr["bookName"].ToString());
                }
            }
        }

        private void txtBookName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode== Keys.Down)
            {
                lBoxBookName.Focus();
                lBoxBookName.SelectedIndex= 0;
            }
        }

        private void lBoxBookName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBookName.Text = lBoxBookName.SelectedItem.ToString();
                lBoxBookName.Visible= false;
            }
        }

        private void lBoxBookName_MouseClick(object sender, MouseEventArgs e)
        {
            txtBookName.Text = lBoxBookName.SelectedItem.ToString() ;
            lBoxBookName.Visible= false;
        }

        private void btnIssueBook_Click(object sender, EventArgs e)
        {
            int bookQuantity=0;

            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType= CommandType.Text;
            cmd.CommandText = "select * from books where bookName='"+ txtBookName.Text +"'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                bookQuantity = Convert.ToInt32(dr["availableQuantity"].ToString());
                bookID = dr["bookID"].ToString();
            }

            if(bookQuantity > 0 )
            {
                MySqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "insert into issuebooks (studentID,bookID,issueDate) " +
                                "VALUES ('" + studentID + "','" + bookID + "','" + dtpIssueDate.Text + "','')";
                cmd1.ExecuteNonQuery();

                MySqlCommand cmd2 = con.CreateCommand();
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = "update books set availableQuantity=availableQuantity-1 where bookName='"+ txtBookName.Text +"'";
                cmd2.ExecuteNonQuery();

                MessageBox.Show("books issue successfully");
            }
            else
            {
                MessageBox.Show("books not available");
            }

            
        }
    }
}
