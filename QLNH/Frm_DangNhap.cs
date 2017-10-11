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


namespace QLNH
{
    public partial class Frm_DangNhap : Form
    {
        public Frm_DangNhap()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Frm_DangNhap_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'qL_NHDataSet.v_DS_PHANMANH' table. You can move, or remove it, as needed.
            this.v_DS_PHANMANHTableAdapter.Fill(this.qL_NHDataSet.v_DS_PHANMANH);

            tENCNComboBox.SelectedIndex = 1;
            tENCNComboBox.SelectedIndex = 0;


        }

        private void tENCNComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                Program.servername = tENCNComboBox.SelectedValue.ToString();
            }
            catch { }

        }

        private void btnDN_Click(object sender, EventArgs e)
        {
            if (txtLogin.Text.Trim() == "")
            {
                MessageBox.Show(" Vui lòng nhập user name.", "Lỗi đăng nhập", MessageBoxButtons.OK);
                txtLogin.Focus();
                return;


            }

            if (txtLogin.Text.Trim() == "")
            {
                MessageBox.Show("Tài khoản đăng nhập không được bỏ trống!", " Báo lỗi đăng nhập", MessageBoxButtons.OK);
                txtLogin.Focus();
                return;
            }
            Program.mlogin = txtLogin.Text;
            Program.password = txtPassword.Text;

            if (Program.KetNoi() == 0) return;
            //MessageBox.Show("Đăng nhập thành công." , " ", MessageBoxButtons.OK);

            SqlDataReader myReader;

            String strLenh = "exec SP_DANGNHAP '" + Program.mlogin + "'";
            myReader = Program.ExecSqlDataReader(strLenh);
            if (myReader == null) return;
            myReader.Read();//Doc 1 dong


            Program.username = myReader.GetString(0);     // Lay user name
            if (Convert.IsDBNull(Program.username))
            {
                MessageBox.Show("Login bạn nhập không có quyền truy cập dữ liệu\n Bạn xem lại username, password", "", MessageBoxButtons.OK);
                return;
            }
            Program.mHoten = myReader.GetString(1);
            Program.mGroup = myReader.GetString(2);
            myReader.Close();
            Program.conn.Close();

            Program.frmChinh.MANV.Text = "Mã nhân viên : " + Program.username;
            Program.frmChinh.HOTEN.Text = "Họ tên : " + Program.mHoten;
            Program.frmChinh.NHOM.Text = "Nhóm : " + Program.mGroup;
            this.Close();



        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
