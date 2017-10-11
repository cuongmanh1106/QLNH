using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QLNH
{
    public partial class Frm_Main : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Frm_Main()
        {
            InitializeComponent();
        }

        private Form CheckExists(Type ftype)
        {
            foreach (Form f in this.MdiChildren)
                if (f.GetType() == ftype)
                    return f;
            return null;
        }


        private void btnDangNhap_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(Frm_DangNhap));
            if (frm != null) frm.Activate();
            else
            {
                Frm_DangNhap f = new Frm_DangNhap();
                f.MdiParent = this;
                f.Show();
            }

        }

        private void Frm_Main_Shown(object sender, EventArgs e)
        {
            Form frm = this.CheckExists(typeof(Frm_DangNhap));
            if (frm != null) frm.Activate();
            else
            {
                Frm_DangNhap f = new Frm_DangNhap();
                f.MdiParent = this;
                f.Show();
            }

        }

        private void Frm_Main_Load(object sender, EventArgs e)
        {

        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form frm = this.CheckExists(typeof(Frm_NhanVien));
            if (frm != null) frm.Activate();
            else
            {
                Frm_NhanVien f = new Frm_NhanVien();
                f.MdiParent = this;
                f.Show();
            }
        }
    }
}
