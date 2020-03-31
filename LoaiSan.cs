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
    public partial class LoaiSan : Form
    {
        public LoaiSan()
        {
            InitializeComponent();
        }
        SqlConnection conn;
        private void LoaiSan_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(@"Data Source=LAPTOP-DSA2JMNK\SQLEXPRESS;Initial Catalog=test;Integrated Security=True");
            conn.Open();
            HienThi();
        }

        // Xuất dữ liệu thể loại sân từ database vào dataGrid
        public void HienThi()
        {
            string sqlSelect = "SELECT * FROM loaiSan";
            SqlCommand cmd = new SqlCommand(sqlSelect, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
        }

        // thêm thể loại sân mới
        private void button1_Click(object sender, EventArgs e)
        {
            string sqlInsert = "INSERT INTO loaiSan VALUES (@id,@tenLoaiSan,@gia)";
            SqlCommand cmd = new SqlCommand(sqlInsert, conn);
            cmd.Parameters.AddWithValue("id", textBox1.Text);
            cmd.Parameters.AddWithValue("tenLoaiSan", textBox2.Text);
            cmd.Parameters.AddWithValue("gia", textBox3.Text);
           
            cmd.ExecuteNonQuery();
            HienThi();
        }

        // sửa thể loại sân
        private void button2_Click(object sender, EventArgs e)
        {
            string sqlEdit = "UPDATE loaiSan SET tenLoaiSan=@tenLoaiSan,gia=@gia where id=@id";
            SqlCommand cmd = new SqlCommand(sqlEdit, conn);
            cmd.Parameters.AddWithValue("id", textBox1.Text);
            cmd.Parameters.AddWithValue("tenLoaiSan", textBox2.Text);
            cmd.Parameters.AddWithValue("gia", textBox3.Text);

            cmd.ExecuteNonQuery();
            HienThi();
        }

        // xóa thể loại sân
        private void button3_Click(object sender, EventArgs e)
        {
            string sqlDelete = "DELETE FROM loaiSan where id=@id";
            SqlCommand cmd = new SqlCommand(sqlDelete, conn);
            
            cmd.Parameters.AddWithValue("id", textBox1.Text);
            cmd.Parameters.AddWithValue("tenLoaiSan", textBox2.Text);
            cmd.Parameters.AddWithValue("gia", textBox3.Text);

            cmd.ExecuteNonQuery();
            HienThi();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            trangChu form = new trangChu();
            form.Show();
        }
    }
}
