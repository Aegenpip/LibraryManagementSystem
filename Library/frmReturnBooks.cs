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
    public partial class frmReturnBooks : MetroFramework.Forms.MetroForm
    {
        MySqlConnection con = new MySqlConnection($"SERVER=localhost;DATABASE=library;UID=root;PWD=2001e-U1995");

        public frmReturnBooks()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            pnlDGW.Visible = true;
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType= CommandType.Text;
            cmd.CommandText = "select * from issuesinfo where enrollmentNumber='"+ txtEnterEnNum.Text +"' AND returnDate=''";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource= dt;
        }

        private void frmReturnBooks_Load(object sender, EventArgs e)
        {
            if(con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            pnlReturn.Visible= true;
            int i;
            i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from issuesinfo where issueID='"+ i +"'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);

            foreach(DataRow dr in dt.Rows)
            {
                lblBookName.Text = dr["bookName"].ToString();
                lblIssueDate.Text = dr["issueDate"].ToString();
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            int i;
            i = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update issuebooks set returnDate='"+ dateTimePicker1.Text +"' where issueID="+ i +"";
            cmd.ExecuteNonQuery();

            MySqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "update books set availableQuantity=availableQuantity+1 where bookName='"+ lblBookName.Text +"'";
            cmd1.ExecuteNonQuery();

            MessageBox.Show("books return succesfully");


            pnlDGW.Visible = true;
            MySqlCommand cmd2 = con.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "select * from issuesinfo where enrollmentNumber='" + txtEnterEnNum.Text + "' AND returnDate=''";
            cmd2.ExecuteNonQuery();
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd2);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
