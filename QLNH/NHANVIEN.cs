using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNH
{
    class NHANVIEN
    {
        private Program.status status;
        private String manv;
        private String hoten;
        private String sodt;
        private String dc;
        private String phai;
        private String macn;

        

        public NHANVIEN()
        {

        }

        public NHANVIEN(String manv , String hoten, String sodt, String dc, String phai, String macn, Program.status status )
        {
            this.manv = manv;
            this.hoten = hoten;
            this.sodt = sodt;
            this.dc = dc;
            this.phai = phai;
            this.macn = macn;
            this.Status = status;
        }

        public String sqlQuery()
        {
            switch (this.status)
            {
                case Program.status.insert:
                    return "delete from NHANVIEN WHERE MANV='" + this.manv + "'";


                case Program.status.delete:
                    return "insert into NHANVIEN ([MANV],[HOTEN],[SODT],[DIACHI],[PHAI],[MACN]) VALUES('" + this.manv + "',N'" + this.hoten + "','" + this.sodt + "',N'" + this.dc + "',N'" + this.phai + "','" + this.macn + "')";

                case Program.status.update:
                    
                    return "UPDATE NHANVIEN SET [HOTEN] = N'" + this.hoten + "',[DIACHI] = N'" + this.dc +  "',[PHAI]=N'" + this.phai + "',[SODT]='" + this.sodt + "',[MACN]='" + this.macn + "' WHERE MANV ='"+this.manv+"'"  ;


            }
            return "";
        }

        public string Manv
        {
            get
            {
                return manv;
            }

            set
            {
                manv = value;
            }
        }

        public string Hoten
        {
            get
            {
                return hoten;

            }
            set
            {
                hoten = value;
            }
        }

        public string Sodt
        {
            get
            {
                return sodt;
            }

            set
            {
                sodt = value;
            }
        }

        public string Dc
        {
            get
            {
                return dc;
            }

            set
            {
                dc = value;
            }
        }

        public string Phai
        {
            get
            {
                return phai;
            }

            set
            {
                phai = value;
            }
        }

        public string Macn
        {
            get
            {
                return macn;
            }

            set
            {
                macn = value;
            }
        }

        internal Program.status Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
            }
        }
    }
}
