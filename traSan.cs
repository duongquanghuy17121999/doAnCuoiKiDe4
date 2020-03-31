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
    public partial class traSan : Form
    {
        public traSan()
        {
            InitializeComponent();
        }

        private void traSan_Load(object sender, EventArgs e)
        {
            Load_San();
        }

        //Load danh sách sân đang được cho thuê vào comboBox1
        private void Load_San()
        {
            String connectionStr = @"Data Source=LAPTOP-DSA2JMNK\SQLEXPRESS;Initial Catalog=test;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionStr);
            connection.Open();

            String query = "Select id from San where trangThaiSan=N'full'";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable data = new DataTable();
            adapter.Fill(data);
            comboBox1.DisplayMember = "id";
            comboBox1.DataSource = data;
            connection.Close();
        }

        //Load thông tin khách sau khi chọn sân
        private void Load_Khach()
        {
            String connectionStr = @"Data Source=LAPTOP-DSA2JMNK\SQLEXPRESS;Initial Catalog=test;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionStr);
            connection.Open();

            String query = "Select San.id[Mã sân], khachHang.id[Mã khách hàng], khachHang.hoTen from San, khachHang, chiTietKhachHang where San.id = chiTietKhachHang.idSan and chiTietKhachHang.idKhachHang = khachHang.id and San.id = '" + comboBox1.Text + "'";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable data = new DataTable();
            adapter.Fill(data);
            dataGridView1.DataSource = data;
            connection.Close();
        }


        // Khi nhấn nút trả sân thì xóa thông tin khách thuê tương ứng
        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Thông tin thuê phòng này sẽ bị xóa?", "Thông Báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                MessageBox.Show("Thao đã tác bị hủy!");
            }
            else
            {
                String connectionStr = @"Data Source=LAPTOP-DSA2JMNK\SQLEXPRESS;Initial Catalog=test;Integrated Security=True";

                SqlConnection connection = new SqlConnection(connectionStr);
                connection.Open();
                //Xóa Thông tin trong chi tiết khách thuê trước
                String query = "Delete from chiTietKhachHang where idSan='" + comboBox1.Text + "'";
                SqlCommand command = new SqlCommand(query, connection);
                int ret = command.ExecuteNonQuery();

                if (ret > 0)
                {
                    MessageBox.Show("Xóa thông tin thuê sân thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Update lại trạng thái sân
                    query = "Update San set trangThaiSan=N'Empty' where id='" + comboBox1.Text + "'";
                    command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                    Load_Khach();
                    Load_San();
                }
                else
                {
                    MessageBox.Show("Xóa thông tin thuê sân thất bại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Load_Khach();
                    Load_San();
                    connection.Close();
                }

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_Khach();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            trangChu form = new trangChu();
            form.Show();
        }
    }
}
