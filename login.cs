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

namespace test
{
    public partial class login : Form
    {
        string strConnection = @"Data Source=LAPTOP-DSA2JMNK\SQLEXPRESS;Initial Catalog=test;Integrated Security=True";
        SqlConnection conn;
        SqlCommand command;

        public login()
        {
            InitializeComponent();
        }

        // Kiểm tra đăng nhập
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection(strConnection);
                conn.Open();
                string sql = "Select COUNT(*)  FROM [test].[dbo].[admin] where userName=@userName and passWord=@pass ";
                command = new SqlCommand(sql, conn);
                command.Parameters.Add(new SqlParameter("@userName", textBox1.Text));
                command.Parameters.Add(new SqlParameter("@pass", textBox2.Text));
                int x = (int)command.ExecuteScalar();
                if (x == 1) //Kiểm tra userName và passWord có trong database chưa
                {
                    //MessageBox.Show("login suzess", "thong bao");
                    this.Hide();
                    trangChu form = new trangChu();
                    form.Show();
                }
                else
                {
                    MessageBox.Show("login failt");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();

        }

        private void login_Load(object sender, EventArgs e)
        {

        }
    }
}
