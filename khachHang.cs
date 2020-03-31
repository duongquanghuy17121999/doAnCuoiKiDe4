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
    public partial class khachHang : Form
    {
        public khachHang()
        {
            InitializeComponent();
        }
        SqlConnection conn;

        private void khachHang_Load(object sender, EventArgs e)
        {

            conn = new SqlConnection(@"Data Source=LAPTOP-DSA2JMNK\SQLEXPRESS;Initial Catalog=test;Integrated Security=True");
            conn.Open();
            HienThi();
        }

        // Load thông tin khách hàng vào DataGridView
        public void HienThi()
        {
            string sqlSelect = "SELECT * FROM khachHang";
            SqlCommand cmd = new SqlCommand(sqlSelect, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
        }

        // Thêm khách mới vào danh sách
        private void button1_Click(object sender, EventArgs e)
        {
            // thực hiện câu truy vấn thêm khách hàng
            string sqlInsert = "INSERT INTO khachHang VALUES (@id,@hoTen,@sdt,@cmnd)";
            SqlCommand cmd = new SqlCommand(sqlInsert, conn);
            cmd.Parameters.AddWithValue("id", textBox1.Text);
            cmd.Parameters.AddWithValue("hoTen", textBox2.Text);
            cmd.Parameters.AddWithValue("sdt", textBox3.Text);
            cmd.Parameters.AddWithValue("cmnd", textBox4.Text);
            cmd.ExecuteNonQuery();
            HienThi();
        }

        // cập nhật thông tin khách hàng
        private void button2_Click(object sender, EventArgs e)
        {
            string sqlEdit = "UPDATE khachHang SET hoTen=@hoTen,sdt=@sdt,cmnd=@cmnd where id=@id";
            SqlCommand cmd = new SqlCommand(sqlEdit, conn);
            cmd.Parameters.AddWithValue("id", textBox1.Text);
            cmd.Parameters.AddWithValue("hoTen", textBox2.Text);
            cmd.Parameters.AddWithValue("sdt", textBox3.Text);
            cmd.Parameters.AddWithValue("cmnd", textBox4.Text);

            cmd.ExecuteNonQuery();
            HienThi();
        }

        //xóa thông tin khách khỏi danh sách
        private void button3_Click(object sender, EventArgs e)
        {
            string sqlDelete = "DELETE FROM khachHang where id=@id";
            SqlCommand cmd = new SqlCommand(sqlDelete, conn);

            cmd.Parameters.AddWithValue("id", textBox1.Text);
            cmd.Parameters.AddWithValue("hoTen", textBox2.Text);
            cmd.Parameters.AddWithValue("sdt", textBox3.Text);
            cmd.Parameters.AddWithValue("cmnd", textBox4.Text);

            cmd.ExecuteNonQuery();
            HienThi();
        }

        // tìm kiếm thông tin khách dựa vào id
        private void button4_Click(object sender, EventArgs e)
        {
            String sqlTimKiem = "select * from khachHang where id=@id";
            SqlCommand cmd = new SqlCommand(sqlTimKiem, conn);
            cmd.Parameters.AddWithValue("id", textBox5.Text);
           // cmd.Parameters.AddWithValue("hoTen", textBox5.Text);
            cmd.ExecuteNonQuery();
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            trangChu form = new trangChu();
            form.Show();
        }
    }
}
