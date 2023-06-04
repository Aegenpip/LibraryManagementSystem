using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlConnector;

namespace Library
{
    public partial class frmViewBooks : MetroFramework.Forms.MetroForm
    {
        MySqlConnection con = new MySqlConnection($"SERVER=localhost;DATABASE=library;UID=root;PWD=2001e-U1995");

        public frmViewBooks()
        {
            InitializeComponent();
        }

        private void frmViewBooks_Load(object sender, EventArgs e)
        {
            con.Open();  

            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType= CommandType.Text;  
            cmd.CommandText = "Select * from books";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill( dt );
            dgwViewBooks.DataSource = dt;

            con.Close();
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            con.Open();

            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from books where bookName like ('%" + txtSearch.Text + "%')";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            dgwViewBooks.DataSource = dt;

            con.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int i;
            i = Convert.ToInt32(dgwViewBooks.SelectedCells[0].Value.ToString());

            con.Open();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update books set bookName='" + txtBookName.Text + "',author='" + txtAuthor.Text + "',publisher='" + txtPublisher.Text + "'," +
                                "purchaseDate='" + dtpPrchsDate.Text + "',price=" + txtPrice.Text + ",quantity=" + txtQuantity.Text + " where bookID="+ i +"";
            cmd.ExecuteNonQuery();

            con.Close();

            MessageBox.Show("Book Updated Succesfully");
        }

        private void dgwViewBooks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            pnlUpdate.Visible = true;  

            int i;
            i = Convert.ToInt32(dgwViewBooks.SelectedCells[0].Value.ToString());

            con.Open();
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from books where bookID="+ i + "";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);    
            da.Fill(dt);

            foreach(DataRow dr in dt.Rows)
            {
                txtBookName.Text = dr["bookName"].ToString();
                txtAuthor.Text = dr["author"].ToString();
                txtPublisher.Text = dr["publisher"].ToString();
                dtpPrchsDate.Value = Convert.ToDateTime(dr["purchaseDate"].ToString());
                txtPrice.Text = dr["price"].ToString();
                txtQuantity.Text = dr["quantity"].ToString();
            }

            con.Close();
        }
    }
}
