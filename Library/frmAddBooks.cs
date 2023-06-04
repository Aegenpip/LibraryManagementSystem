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
    public partial class frmAddBooks : MetroFramework.Forms.MetroForm
    {
        MySqlConnection con = new MySqlConnection($"SERVER=localhost;DATABASE=library;UID=root;PWD=2001e-U1995");


        public frmAddBooks()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            con.Open(); 
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "Call addBook('"+ txtBookName.Text +"','"+ txtAuthor.Text +"','"+ txtPublisher.Text +"','"+ dtpPrchsDate.Text +"',"+ txtPrice.Text +","+ txtQuantity.Text +","+ txtQuantity.Text +")";
            cmd.ExecuteNonQuery();
            con.Close();


            txtBookName.Text = "";
            txtAuthor.Text = "";
            txtPublisher.Text = "";
            txtPrice.Text = "";
            txtQuantity.Text = "";

            MessageBox.Show("Book Added Succesfully");

        }
    }
}
