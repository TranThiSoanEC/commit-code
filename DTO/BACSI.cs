using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BACSI
    {
        private int MaBS;
        private string TenBS;
        private string TenDangNhap;
        private string matkhau;
        private DataRow item;
        int Loai;
        int TinhTrangTonTai;

        public int MaBS1
        {
            get
            {
                return MaBS;
            }

            set
            {
                MaBS = value;
            }
        }

        
        public string TenBS1
        {
            get
            {
                return TenBS;
            }

            set
            {
                TenBS = value;
            }
        }

       
        public string TenDangNhap1
        {
            get
            {
                return TenDangNhap;
            }

            set
            {
                TenDangNhap = value;
            }

        }


        public string Matkhau
        {
            get
            {
                return matkhau;
            }

            set
            {
                matkhau = value;
            }
        }

        public int Loai1
        {
            get
            {
                return Loai;
            }

            set
            {
                Loai = value;
            }
        }

        public int TinhTrangTonTai1
        {
            get
            {
                return TinhTrangTonTai;
            }

            set
            {
                TinhTrangTonTai = value;
            }
        }

        public BACSI( string tenbs, string tendangnhap, string matkhau, int loai, int tinhtrangtontai)
        {
           
            this.TenBS1= tenbs;
            this.TenDangNhap1= tendangnhap;
            this.Matkhau = matkhau;
            this.Loai1 = loai;
            this.TinhTrangTonTai1 = tinhtrangtontai;
        }

       
        public BACSI(int mabs,string tenbs, string tendangnhap, string matkhau)
        {
            this.MaBS1 = mabs;
            this.TenBS1 = tenbs;
            this.TenDangNhap1 = tendangnhap;
            this.Matkhau = matkhau;
        }

        public BACSI(int mabs, string tenbs, string tendangnhap)
        {
            this.MaBS1 = mabs;
            this.TenBS1 = tenbs;
            this.TenDangNhap1 = tendangnhap;
        }
        public BACSI( string tendangnhap, string matkhau)
        {
            this.TenDangNhap1 = tendangnhap;
            this.Matkhau = matkhau;
        }

        public BACSI()
        {
        }

        public BACSI(DataRow item)
        {
            this.item = item;
        }

    }

}
