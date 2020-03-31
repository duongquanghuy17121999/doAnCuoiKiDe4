using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace test
{
    public partial class hoaDon : Form
    {
        public hoaDon()
        {
            InitializeComponent();
        }

        private void hoaDon_Load(object sender, EventArgs e)
        {
            Load_San();
            Load_Admin();
            Load_KH();
            Load_HoaDon();
        }

        //Load Mã sân có trạng thái đã cho thuê vào combobox
        private void Load_San()
        {
            String connectionStr = @"Data Source=LAPTOP-DSA2JMNK\SQLEXPRESS;Initial Catalog=test;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionStr);
            connection.Open();

            String query = "Select id from San where trangThaiSan='full'";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable data = new DataTable();

            adapter.Fill(data);
            comboBox1.DisplayMember = "id";
            comboBox1.DataSource = data;

            connection.Close();
        }

        //load danh sách admin
        private void Load_Admin()
        {
            String connectionStr = @"Data Source=LAPTOP-DSA2JMNK\SQLEXPRESS;Initial Catalog=test;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionStr);
            connection.Open();

            String query = "Select id from admin";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable data = new DataTable();

            adapter.Fill(data);
            comboBox2.DisplayMember = "id";
            comboBox2.DataSource = data;

            connection.Close();
        }

        // load danh sách khách đã thuê sân vào combobox
        private void Load_KH()
        {
            String connectionStr = @"Data Source=LAPTOP-DSA2JMNK\SQLEXPRESS;Initial Catalog=test;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionStr);
            connection.Open();

            String query = "Select id from khachHang where id IN(Select id from khachHang, chiTietKhachHang where khachHang.id = chiTietKhachHang.idKhachHang)";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable data = new DataTable();

            adapter.Fill(data);
            comboBox3.DisplayMember = "id";
            comboBox3.DataSource = data;

            connection.Close();
        }

        //load danh sách hóa đơn vào dataGridView
        public void Load_HoaDon()
        {
            String connectionStr = @"Data Source=LAPTOP-DSA2JMNK\SQLEXPRESS;Initial Catalog=test;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionStr);
            connection.Open();

            String query = "Select * from hD";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable data = new DataTable();
            adapter.Fill(data);
            dataGridView1.DataSource = data;
            Program.sql = query;
            //connection.Close();
            //Program.sql = query;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox3.Text))
            {
                //Tính tiền sân
                label9.Text = (int.Parse(textBox2.Text) * int.Parse(textBox3.Text)).ToString();
            }
            else
            {
                label9.Text = null;
            }
        }

        // hàm thêm hóa đơn
        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(comboBox1.Text) || String.IsNullOrEmpty(dateTimePicker1.Value.ToString()) || String.IsNullOrEmpty(comboBox2.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Sân, Thời Gian và mã Người Lập!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                String connectionStr = @"Data Source=LAPTOP-DSA2JMNK\SQLEXPRESS;Initial Catalog=test;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionStr);
                connection.Open();

                String query = "Select Count(*) from hD where idSan='" + comboBox1.Text + "' and Month(ngayLap)='" + dateTimePicker1.Value.Month + "'";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable data = new DataTable();
                adapter.Fill(data);
                if (int.Parse(data.Rows[0][0].ToString()) > 0) //Kiểm tra sự tồn tại của hóa đơn
                {
                    MessageBox.Show("Hóa đơn tháng " + dateTimePicker1.Value.Month + " của san " + comboBox1.Text + " đã tồn tại!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    connection.Close();
                }
                else
                {
                    query = "Insert into hD values('" + textBox1.Text + "','" + comboBox1.Text + "','" + comboBox2.Text + "','" + comboBox3.Text + "','" + dateTimePicker1.Value + "','" + textBox2.Text + "','" + textBox3.Text + "','" + label9.Text + "')";
                    command = new SqlCommand(query, connection);
                    int ret = command.ExecuteNonQuery();
                    if (ret > 0)
                    {
                        MessageBox.Show("Tạo hóa đơn cho sân " + comboBox1.Text + " thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        connection.Close();
                        Load_HoaDon();
                    }
                    else
                    {
                        MessageBox.Show("Tạo hóa đơn cho sân " + comboBox1.Text + " thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        connection.Close();
                    }
                }
                connection.Close();

            }
        }

        //hàm xóa hóa đơn
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Không có hóa đơn để xóa!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                String connectionStr = @"Data Source=LAPTOP-DSA2JMNK\SQLEXPRESS;Initial Catalog=test;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionStr);
                connection.Open();

                String query = "Delete  from hD where idHd ='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";
                SqlCommand command = new SqlCommand(query, connection);
                if (MessageBox.Show("Bạn Thật Sự Muốn Xóa Hóa Đơn Này?", "Thông Báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
                {
                    MessageBox.Show("Xóa hóa đơn thất bại!");
                }
                else
                {
                    int ret = command.ExecuteNonQuery();
                    if (ret > 0)
                    {
                        MessageBox.Show("Xóa hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Load_HoaDon();
                        //Load_CTHD();
                        connection.Close();

                    }
                    else
                    {
                        MessageBox.Show("Xóa hóa đơn thất bại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Load_HoaDon();
                        connection.Close();
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            trangChu form = new trangChu();
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            InHoaDon f = new InHoaDon();
            f.Show();
        }
    }
}
