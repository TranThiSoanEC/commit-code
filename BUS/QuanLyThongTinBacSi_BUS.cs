using DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class QuanLyThongTinBacSi_BUS
    {
        public static List<BACSI> LoadBacSi()
        {
            return QuanLyThongTinBacSi_DAO.Instance.LoadBacSi();
        }

        ////////////////////////
        // THEM - SUA - XOA
        public static DataTable BangKiemTraTonTai(BACSI bnDTO)
        {
            return QuanLyThongTinBacSi_DAO.BangKiemTraTonTai(bnDTO);
        }
        public static bool KiemTraTonTai(BACSI bnDTO)
        {
            return QuanLyThongTinBacSi_DAO.Instance.KiemTraTonTai(bnDTO);
        }
        public static bool ThemBacSi(BACSI bnDTO)
        {
            return QuanLyThongTinBacSi_DAO.Instance.ThemBacSi(bnDTO);
        }
        public static bool SuaThongTinBacSi(BACSI bnDTO)
        {
            return QuanLyThongTinBacSi_DAO.Instance.SuaBacSi(bnDTO);
        }

        public static bool XoaBacSi(BACSI bnDTO)
        {

            return QuanLyThongTinBacSi_DAO.Instance.XoaBacSi(bnDTO);
        }

    }
}
