using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace test
{
    public partial class khachDatSan : Form
    {
        public khachDatSan()
        {
            InitializeComponent();
        }
        SqlConnection con;
        private void khachDatSan_Load(object sender, EventArgs e)
        {
            // Hiển thị danh sách khách đã thuê sân
            con = new SqlConnection(@"Data Source=LAPTOP-DSA2JMNK\SQLEXPRESS;Initial Catalog=test;Integrated Security=True");
            con.Open();

            String query = "select id,hoTen from khachHang where hoTen IN(Select hoTen from khachHang, chiTietKhachHang where khachHang.id = chiTietKhachHang.idKhachHang)";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);

            dataGridView1.DataSource = dt;

        }

        //tìm kiếm khách đã thuê sân bằng id
        private void button1_Click(object sender, EventArgs e)
        {
            string sqlTimKiem = "select id,hoTen from khachHang,chiTietKhachHang where khachHang.id=chiTietKhachHang.idKhachHang  and chiTietKhachHang.idKhachHang=@idKhach";
            SqlCommand cmd = new SqlCommand(sqlTimKiem, con);
            cmd.Parameters.AddWithValue("idKhach", textBox1.Text);
            //cmd.Parameters.AddWithValue("Name", txtName.Text);
            cmd.ExecuteNonQuery();
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            trangChu form = new trangChu();
            form.Show();
        }
    }
}
