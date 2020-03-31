using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test
{
    public partial class trangChu : Form
    {
        public trangChu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            login a = new login();
            a.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoaiSan a = new LoaiSan();
            a.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            San a = new San();
            a.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            khachHang a = new khachHang();
            a.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            datSan a = new datSan();
            a.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            traSan a = new traSan();
            a.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            khachDatSan a = new khachDatSan();
            a.Show();
        }

        private void trangChu_Load(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            hoaDon a = new hoaDon();
            a.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
