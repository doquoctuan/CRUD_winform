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

namespace DoQuocTuan_5951071118
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-E7SCDHU\SQLEXPRESS;Initial Catalog=quanli;Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
            GetStudentRecord();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadBinding();
        }

        private void GetStudentRecord()
        {
            
            SqlCommand command = new SqlCommand("select * from tblStudent",con);
            DataTable dataTable = new DataTable();
            con.Open();
            SqlDataReader sqlDataReader = command.ExecuteReader();
            dataTable.Load(sqlDataReader);
            con.Close();
            dgvThongTin.DataSource = dataTable;
        }



        private bool IsValidData()
        {
            if (txtHoTen.Text == string.Empty
                || txtDiaChi.Text == string.Empty
                || string.IsNullOrEmpty(txtSBD.Text)
                || string.IsNullOrEmpty(txtSDT.Text))
            {
                MessageBox.Show("Có chỗ chưa nhập liệu.", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {
                string query = "insert into tblStudent values " + 
                    "(@name, @fathername, @sbd, @diachi, @sdt)";
                SqlCommand command = new SqlCommand(query, con);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@name", txtHoTen.Text);
                command.Parameters.AddWithValue("@fathername", txtTen.Text);
                command.Parameters.AddWithValue("@sbd", txtSBD.Text);
                command.Parameters.AddWithValue("@diachi", txtDiaChi.Text);
                command.Parameters.AddWithValue("@sdt", txtSDT.Text);
                con.Open();
                command.ExecuteNonQuery();
                con.Close();
                GetStudentRecord();
            }
        }

        private void dgvThongTin_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgvThongTin_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        void loadBinding()
        {
            txtHoTen.DataBindings.Add(new Binding("Text", dgvThongTin.DataSource, "name", true, DataSourceUpdateMode.Never));
            txtTen.DataBindings.Add(new Binding("Text", dgvThongTin.DataSource, "tenSV", true, DataSourceUpdateMode.Never));
            txtSDT.DataBindings.Add(new Binding("Text", dgvThongTin.DataSource, "sdt", true, DataSourceUpdateMode.Never));
            txtDiaChi.DataBindings.Add(new Binding("Text", dgvThongTin.DataSource, "address", true, DataSourceUpdateMode.Never));
            txtSBD.DataBindings.Add(new Binding("Text", dgvThongTin.DataSource, "sbd", true, DataSourceUpdateMode.Never));
        }

        private void dgvThongTin_SelectionChanged(object sender, EventArgs e)
        {
            
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (dgvThongTin.SelectedCells.Count > 0)
            {
                string query = String.Format("update tblStudent set name = N'{0}', tenSV = N'{1}', sdt = {2}, address = '{3}' where sbd = '{4}'", txtHoTen.Text, txtTen.Text, txtSDT.Text, txtDiaChi.Text, txtSBD.Text);
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudentRecord();
            } 
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvThongTin.SelectedCells.Count > 0)
            {
                string query = String.Format("delete from tblStudent where sbd = '{0}'", txtSBD.Text);
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                GetStudentRecord();
            }
        }
    }
}
