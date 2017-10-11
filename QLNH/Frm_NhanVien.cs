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

namespace QLNH
{

    public partial class Frm_NhanVien : Form
    {
        NHANVIEN nv = new NHANVIEN();
        Stack<NHANVIEN> Undo = new Stack<NHANVIEN>();

        int vt = 0;
        public Frm_NhanVien()
        {
            InitializeComponent();
        }

        private void nHANVIENBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bdsNV.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dS);

        }

        private void Frm_NhanVien_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dS.NHANVIEN' table. You can move, or remove it, as needed.
            this.nHANVIENTableAdapter.Connection.ConnectionString = Program.connstr;
            this.nHANVIENTableAdapter.Fill(this.dS.NHANVIEN);

            btnGhi.Enabled = false;

            if (bdsNV.Count == 0)
            {
                btnXoa.Enabled = false;
            }
            if (Undo.Count == 0)
            {
                btnPhuchoi.Enabled = false;
                return;
            }



        }

        private void btnThem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {



            vt = bdsNV.Position;
            groupBox1.Enabled = true;
            gcNV.Enabled = false;
            gcNV.Enabled = false;

            SqlDataReader myReader;
            String strLenh = "exec SP_CHECKMANV ";
            myReader = Program.ExecSqlDataReader(strLenh);
            if (myReader == null) return;
            myReader.Read();//Doc 1 dong

            

            bdsNV.AddNew();
            cbPHAI.SelectedIndex = 1;
            cbPHAI.SelectedIndex = 0;
            txtMANV.Text = myReader.GetString(0);
            txtMANV.Enabled = false;

            if (Program.servername.Equals(@"MCUONG-PC\SERVER3"))
            {
                txtMACN.Text = "CN2";
                txtMACN.Enabled = false;
                
            }
            else if (Program.servername.Equals(@"MCUONG-PC\SERVER2"))
            {
                txtMACN.Text = "CN1";
                txtMACN.Enabled = false;

            }

            


            txtHOTEN.Focus();
            
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnDS.Enabled = true;
            btnRefresh.Enabled = true;
            btnGhi.Enabled = true;
            btnPhuchoi.Enabled = true;
            if (Undo.Count == 0)
            {
                btnPhuchoi.Enabled = false;
                return;
            }
            nv.Status = Program.status.insert;


        }

        private void btnGhi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtHOTEN.Text.Trim() == "")
            {
                MessageBox.Show("Họ tên không đươc thiếu", "Báo lỗi nhập liệu", MessageBoxButtons.OK);
                txtHOTEN.Focus();
            }

            if (txtMANV.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập Mã Nhân Viên");
                txtMANV.Focus();
            }
            if (txtDC.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập địa chỉ của bạn");
                txtDC.Focus();
            }
            if (txtSODT.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập số điên thoại");
                txtSODT.Focus();
            }

            

            MessageBox.Show(txtMACN.Text.Trim());

            try
            {
                bdsNV.EndEdit();
                bdsNV.ResetCurrentItem();
                if (dS.HasChanges())
                {
                    this.nHANVIENTableAdapter.Update(this.dS.NHANVIEN);
                    gvNV.UpdateCurrentRow();

                    
                    NHANVIEN nv1 = new NHANVIEN(txtMANV.Text, txtHOTEN.Text, txtSODT.Text, txtDC.Text, cbPHAI.Text, txtMACN.Text, nv.Status);
                    Undo.Push(nv1);

                    if(nv.Status == Program.status.update)
                    {
                        NHANVIEN nv3 = new NHANVIEN(nv.Manv, nv.Hoten, nv.Sodt, nv.Dc, nv.Phai, nv.Macn, nv.Status);
                       Undo.Push(nv3);
                    }



                }
                


            }

            catch (Exception ex)
            {
                if (ex.Message.Contains("MANV"))
                {
                    MessageBox.Show("Mã nhân viên bị trùng");
                }
                else
                {
                    MessageBox.Show("lỗi thêm nhân viên.", "Error", MessageBoxButtons.OK);
                }

            }
            

            btnGhi.Enabled = true;
            btnRefresh.Enabled = true;
            btnThem.Enabled = true;
            btnDS.Enabled = true;
            btnXoa.Enabled = true;
            btnThoat.Enabled = true;
            btnPhuchoi.Enabled = true;
            if (Undo.Count == 0)
            {
                btnPhuchoi.Enabled = false;
                return;
            }
            btnSua.Enabled = true;

        }

        private void btnXoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if ((MessageBox.Show("Bạn muốn xóa nhân viên này", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) == DialogResult.Yes)
            {
                nv.Status = Program.status.delete;
                NHANVIEN nv2 = new NHANVIEN(txtMANV.Text, txtHOTEN.Text, txtSODT.Text, txtDC.Text, cbPHAI.Text, txtMACN.Text,nv.Status);
                Undo.Push(nv2);
                bdsNV.RemoveCurrent();
                this.nHANVIENTableAdapter.Update(this.dS.NHANVIEN);

                btnGhi.Enabled = false;
                btnRefresh.Enabled = true;
                btnThem.Enabled = true;
                btnDS.Enabled = true;
                btnXoa.Enabled = true;
                btnThoat.Enabled = true;
                btnPhuchoi.Enabled = true;
                if (Undo.Count == 0)
                {
                    btnPhuchoi.Enabled = false;
                    return;
                }
                btnSua.Enabled = true;
            }

        }

        private void btnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.nHANVIENTableAdapter.Fill(this.dS.NHANVIEN);
            gcNV.Enabled = true;
            btnGhi.Enabled = false;
            btnRefresh.Enabled = true;
            btnThem.Enabled = true;
            btnDS.Enabled = true;
            btnXoa.Enabled = true;
            btnThoat.Enabled = true;
            btnPhuchoi.Enabled = true;
            if (Undo.Count == 0)
            {
                btnPhuchoi.Enabled = false;
                return;
            }
            btnSua.Enabled = true;


        }

        private void btnSua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gcNV.Enabled = false;
            txtMANV.Enabled = false;
            vt = bdsNV.Position;

            nv.Status = Program.status.update;
            
            nv = new NHANVIEN(txtMANV.Text, txtHOTEN.Text, txtSODT.Text, txtDC.Text, cbPHAI.Text, txtMACN.Text, nv.Status);

            btnGhi.Enabled = true;
            btnRefresh.Enabled = true;
            btnThem.Enabled = false;
            btnDS.Enabled = false;
            btnXoa.Enabled = false;
            btnThoat.Enabled = true;
            btnPhuchoi.Enabled = true;
            if (Undo.Count == 0)
            {
                btnPhuchoi.Enabled = false;
                return;
            }
            btnSua.Enabled = false;

            

        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void btnDS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            this.nHANVIENTableAdapter.Fill(this.dS.NHANVIEN);
            gcNV.Enabled = true;

            btnGhi.Enabled = false;
            btnRefresh.Enabled = true;
            btnThem.Enabled = true;
            btnDS.Enabled = true;
            btnXoa.Enabled = true;
            btnThoat.Enabled = true;
            btnPhuchoi.Enabled = true;
            if (Undo.Count == 0)
            {
                btnPhuchoi.Enabled = false;
                return;
            }
            btnSua.Enabled = true;

        }

        private void btnPhuchoi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(Undo.Count == 0)
            {
                btnPhuchoi.Enabled = false;
                return;
            }

            NHANVIEN nv = new NHANVIEN();
            nv = Undo.Pop();
            
            Program.ExecSqlDataTable(nv.sqlQuery());
            //MessageBox.Show(nv.sqlQuery());

            this.nHANVIENTableAdapter.Fill(this.dS.NHANVIEN);


        }
    }
}
