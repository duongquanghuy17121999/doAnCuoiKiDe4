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
    public partial class San : Form
    {
        public San()
        {
            InitializeComponent();
        }
        SqlConnection conn;
        private void San_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(@"Data Source=LAPTOP-DSA2JMNK\SQLEXPRESS;Initial Catalog=test;Integrated Security=True");
            conn.Open();
            HienThi();
            Load_San();
        }

        // Load dữ liêu Sân vào dataGridView
        public void HienThi()
        {
            string sqlSelect = "SELECT * FROM San";
            SqlCommand cmd = new SqlCommand(sqlSelect, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
            Program.sql = sqlSelect;
        }

        //Thêm Sân mới vào danh sách
        private void button1_Click(object sender, EventArgs e)
        {
            // Thực hiện câu truy vấn thêm sân
            string sqlInsert = "INSERT INTO San VALUES (@id,@tenSan,@trangThaiSan,@idLoaiSan)";
            SqlCommand cmd = new SqlCommand(sqlInsert, conn);
            cmd.Parameters.AddWithValue("id", textBox1.Text);
            cmd.Parameters.AddWithValue("tenSan", textBox2.Text);
            cmd.Parameters.AddWithValue("trangThaiSan", textBox3.Text);
            cmd.Parameters.AddWithValue("idLoaiSan", comboBox1.Text);
            cmd.ExecuteNonQuery();
            HienThi();
        }

        // Cập nhật thông tin sân
        private void button2_Click(object sender, EventArgs e)
        {
            string sqlEdit = "UPDATE San SET tenSan=@tenSan,trangThaiSan=@trangThaiSan,idLoaiSan=@idLoaiSan where id=@id";
            SqlCommand cmd = new SqlCommand(sqlEdit, conn);
            cmd.Parameters.AddWithValue("id", textBox1.Text);
            cmd.Parameters.AddWithValue("tenSan", textBox2.Text);
            cmd.Parameters.AddWithValue("trangThaiSan", textBox3.Text);
            cmd.Parameters.AddWithValue("idLoaiSan", comboBox1.Text);

            cmd.ExecuteNonQuery();
            HienThi();
        }

        //Xóa sân khỏi danh sách
        private void button3_Click(object sender, EventArgs e)
        {
            string sqlDelete = "DELETE FROM San where id=@id";
            SqlCommand cmd = new SqlCommand(sqlDelete, conn);

            cmd.Parameters.AddWithValue("id", textBox1.Text);
            cmd.Parameters.AddWithValue("tenSan", textBox2.Text);
            cmd.Parameters.AddWithValue("trangThaiSan", textBox3.Text);
            cmd.Parameters.AddWithValue("idLoaiSan", comboBox1.Text);

            cmd.ExecuteNonQuery();
            HienThi();
        }

        // tìm kiếm sân dựa vào id Sân
        private void button4_Click(object sender, EventArgs e)
        {
            string sqlTimKiem = "select * from San where id=@id";
            SqlCommand cmd = new SqlCommand(sqlTimKiem, conn);
            cmd.Parameters.AddWithValue("id", textBox5.Text);
            cmd.Parameters.AddWithValue("tenSan", textBox2.Text);
            cmd.ExecuteNonQuery();
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
        }

        // Load Id loại sân vào comboBox1
        private void Load_San()
        {
            String connectionStr = @"Data Source=LAPTOP-DSA2JMNK\SQLEXPRESS;Initial Catalog=test;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionStr);
            connection.Open();

            String query = "Select id from loaiSan";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable data = new DataTable();
            adapter.Fill(data);
            comboBox1.DisplayMember = "id";
            comboBox1.DataSource = data;
            connection.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            trangChu form = new trangChu();
            form.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            inSan f = new inSan();
            f.Show();
        }
    }
}
