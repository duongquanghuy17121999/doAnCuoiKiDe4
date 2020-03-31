using CrystalDecisions.CrystalReports.Engine;
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
    public partial class InHoaDon : Form
    {
        public InHoaDon()
        {
            InitializeComponent();
        }

        private void InHoaDon_Load(object sender, EventArgs e)
        {
            ReportDocument objrep = new rpHoaDon();
            SqlConnection mycon = new SqlConnection(@"Data Source=LAPTOP-DSA2JMNK\SQLEXPRESS;Initial Catalog=test;Integrated Security=True");
            mycon.Open();
            SqlDataAdapter data = new SqlDataAdapter(Program.sql,
            mycon);
            DataSet ds = new DataSet();
            data.Fill(ds, Program.sql);
            objrep.SetDataSource(ds.Tables[0]);
            crystalReportViewer1.ReportSource = objrep;
            crystalReportViewer1.Refresh();
        }
    }
}
