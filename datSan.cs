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
    public partial class datSan : Form
    {
        public datSan()
        {
            InitializeComponent();
        }
        SqlConnection con;
        private void datSan_Load(object sender, EventArgs e)
        {
            //Load danh sach khách chưa có sân
            con = new SqlConnection(@"Data Source=LAPTOP-DSA2JMNK\SQLEXPRESS;Initial Catalog=test;Integrated Security=True");
            con.Open();

            String query = "select id[Mã Khách hàng],hoTen[Họ tên] from khachHang where hoTen NOT IN(Select hoTen from khachHang, chiTietKhachHang where khachHang.id = chiTietKhachHang.idKhachHang)";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);

            dataGridView1.DataSource = dt;

             HienThi();
        }

        // Load danh sách sân còn trống
        public void HienThi()
        {
            string sqlSelect2 = "Select id[id Sân],tenSan[tên sân] from San where id NOT IN (Select San.id from San, chiTietKhachHang where San.id = chiTietKhachHang.idSan Group by San.id)";
            SqlCommand cmd2 = new SqlCommand(sqlSelect2, con);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            DataTable dt2 = new DataTable();
            dt2.Load(dr2);

            dataGridView2.DataSource = dt2;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // kiểm tra đã nhấn chọn khách và sân chưa
            if (dataGridView1.CurrentRow.Selected && dataGridView2.CurrentRow.Selected)
            {
                con = new SqlConnection(@"Data Source=LAPTOP-DSA2JMNK\SQLEXPRESS;Initial Catalog=test;Integrated Security=True");
                con.Open();

                String query = "Insert into chiTietKhachHang values ('" + dataGridView1.CurrentRow.Cells[0].Value + "','" + dataGridView2.CurrentRow.Cells[0].Value + "','"+dateTimePicker1.Value+"')";
                SqlCommand command = new SqlCommand(query, con);
                int ret = command.ExecuteNonQuery();
                if (ret > 0) //Nếu insert thành công thì sẽ cập nhật lại trạng thái sân
                {
                    MessageBox.Show("Đã thêm khách hàng " + dataGridView1.CurrentRow.Cells[1].Value.ToString() + " vào sân " + dataGridView2.CurrentRow.Cells[0].Value.ToString() + " thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    query = "Update San set trangThaiSan=N'full' where id='" + dataGridView2.CurrentRow.Cells[0].Value + "'";
                    command = new SqlCommand(query, con);
                    command.ExecuteNonQuery();
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Thêm khách hàng " + dataGridView1.CurrentRow.Cells[1].Value.ToString() + " vào sân " + dataGridView2.CurrentRow.Cells[0].Value.ToString() + " thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                con.Close();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            trangChu form = new trangChu();
            form.Show();
        }
    }
}
