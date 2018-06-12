using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using DTO;
using System.Data;

namespace BUS
{
  public  class BACSI_BUS
    {
        private static BACSI_BUS instance;

        private BACSI_BUS() { }

        public static BACSI_BUS Instance
        {
            get
            {
                if (instance == null)
                    instance = new BACSI_BUS();
                return instance;
            }

            private set
            {
                instance = value;
            }
        }

       public bool LoginBUS(string TenDangNhap, string MatKhau)
        {

            //return DAO.TaiKhoanDAO.Instance.DangNhap(TenDangNhap,MatKhau);
            return BACSI_DAO.Instance.DangNhap(TenDangNhap, MatKhau);

        }

        public static List<BACSI> LoadBacSi()
         {
            return BACSI_DAO.Instance.LoadBacSi();
         }

        public static List<BACSI> LoadBacSiThamGiaKhamBenh()
        {
            return BACSI_DAO.Instance.LoadBacSiThamGiaKhamBenh();
        }
        
        public static List<BACSI> LoadBacSiCanTim(int Ma)
        {
            return BACSI_DAO.Instance.LoadBacSiCanTim(Ma);
        }
        public BACSI LayThongTinBacSy(int MaBS)
        { 
            return BACSI_DAO.Instance.LayThongTinBacSy(MaBS);

        }
        public BACSI GetAccountByUserName(string userName)
        {
            return BACSI_DAO.Instance.GetAccountByUserName(userName);

        }

        public bool CapNhatThongTin(int ma, string ten, string tendangnhap, string matkhau, string matkhaumoi)
        {

            return BACSI_DAO.Instance.CapNhatThongTin(ma, ten, tendangnhap, matkhau, matkhaumoi);
        }

    }
}
