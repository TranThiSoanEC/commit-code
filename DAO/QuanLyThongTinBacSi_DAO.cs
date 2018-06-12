using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
  public  class QuanLyThongTinBacSi_DAO
    {
        static SqlConnection Con;

        private static QuanLyThongTinBacSi_DAO instance;

        private QuanLyThongTinBacSi_DAO() { }

        public static QuanLyThongTinBacSi_DAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new QuanLyThongTinBacSi_DAO();
                return instance;
            }

            private set
            {
                instance = value;
            }
        }
        public List<BACSI> LoadBacSi()
        {

            // khai bao truy van sql
            string sTruyVan = "select * from BACSI where TinhTrangTonTai = '1'";
            // Mo ket noi
            Con = DataProvider_1.KetNoi();
            // Tien hanh truy van
            DataTable dt = DataProvider_1.LayDataTable(sTruyVan, Con);
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            // tao List PHIEUKHAM_DTO
            List<BACSI> listbs = new List<BACSI>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                BACSI bs = new BACSI();
                bs.MaBS1 = int.Parse(dt.Rows[i]["MaBS"].ToString());
                bs.TenBS1 = dt.Rows[i]["TenBS"].ToString();
                bs.TenDangNhap1 = dt.Rows[i]["TenDangNhap"].ToString();
                bs.Matkhau = dt.Rows[i]["MatKhau"].ToString();
                bs.Loai1 = int.Parse(dt.Rows[i]["Loai"].ToString());
                bs.TinhTrangTonTai1 = int.Parse(dt.Rows[i]["TinhTrangTonTai"].ToString());

                listbs.Add(bs);
            }
            DataProvider_1.DongKetNoi(Con);
            return listbs;
        }

        // them - sua - xoa table BACSI

        public static DataTable BangKiemTraTonTai(BACSI bnDTO)
        {
            // string sTruyVan = string.Format("SELECT MaBN from BENHNHAN WHERE (TenBN like N'{0}') and (NgaySinh = N'{1}' and (DienThoai = '{2}' and GioiTinh = N'{3}'))", bnDTO.TenBN1, bnDTO.NgaySinh1, bnDTO.DienThoai1, bnDTO.GioiTinh1); // MaBN tu dong tang
            string sTruyVan = string.Format("SELECT MaBS from BACSI WHERE TenDangNhap =N'{0}' AND TinhTrangTonTai = '1'", bnDTO.TenDangNhap1);
            try
            {   
                DataTable dt = DataProvider_1.LayDataTable(sTruyVan, Con);
                DataProvider_1.DongKetNoi(Con);
                if (dt.Rows.Count == 0)
                {
                    return null;
                }
                else
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                DataProvider_1.DongKetNoi(Con);
                return null;
            }

        }
        public bool KiemTraTonTai(BACSI bnDTO)
        {
            string sTruyVan = string.Format("SELECT MaBS from BACSI WHERE TenDangNhap =N'{0}'  AND TinhTrangTonTai = '1'", bnDTO.TenDangNhap1); // MaBN tu dong tang
            Con = DataProvider_1.KetNoi();
            try
            {
                // thuc thi truy van
               bool kq = DataProvider_1.ThucThiTruyVanNonQuery(sTruyVan, Con);
                // dong ket noi
                DataProvider_1.DongKetNoi(Con);

                if (kq == true)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                DataProvider_1.DongKetNoi(Con);
                return false;
            }


        }

        public bool ThemBacSi(BACSI bnDTO)
        {
            // tao cau truy van
            // string sTruyVan = @"insert into BENHNHAN(MaBN,TenBN) value"; -- them mot vai thanh phan vao bang
            /* string sTruyVan = string.Format("insert into BENHNHAN values ('{0}',N'{1}','{2}',N'{3}',{4},N'{5}')", bnDTO.MaBN1, bnDTO.TenBN1, bnDTO.NgaySinh1, bnDTO.DiaChi1, bnDTO.DienThoai1, bnDTO.GioiTinh1);*/// them day du thong tin cua bang
            string sTruyVan = string.Format("insert into BACSI values (N'{0}', N'{1}', N'{2}', '{3}', '1')", bnDTO.TenBS1, bnDTO.TenDangNhap1, bnDTO.Matkhau, bnDTO.Loai1); // MaBN tu dong tang
            Con = DataProvider_1.KetNoi();
            try
            {
                // thuc thi truy van
                bool kq = DataProvider_1.ThucThiTruyVanNonQuery(sTruyVan, Con);
                // dong ket noi
                DataProvider_1.DongKetNoi(Con);

                if (kq == true)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                DataProvider_1.DongKetNoi(Con);
                return false;
            }

        }

        // sua benh nhan

        public bool SuaBacSi(BACSI bnDTO)
        {
            string sTruyVan = string.Format("update BACSI set TenBS = N'{0}', TenDangNhap = N'{1}', MatKhau = N'{2}' , Loai = '{3}'  where MaBS= N'{4}'", bnDTO.TenBS1, bnDTO.TenDangNhap1, bnDTO.Matkhau, bnDTO.Loai1, bnDTO.MaBS1);
            Con = DataProvider_1.KetNoi();
            try
            {
                // thuc thi truy van
               bool KetQua = DataProvider_1.ThucThiTruyVanNonQuery(sTruyVan, Con);
                // dong truy van
                DataProvider_1.DongKetNoi(Con);
                if (KetQua == true)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                DataProvider_1.DongKetNoi(Con);
                return false;
            }
        }

        // xoa benh nhan
        public DataTable TaoBang(BACSI bnDTO)
        {
            

            string query = string.Format("SELECT MaPK from PHIEUKHAM, BACSI WHERE BACSI.MaBS = PHIEUKHAM.MaBS and TenDangNhap = N'{0}'", bnDTO.TenDangNhap1);
            Con = DataProvider_1.KetNoi();

            DataTable dt = DataProvider_1.LayDataTable(query, Con);

            DataProvider_1.DongKetNoi(Con);

            return dt;
        }

        public bool XoaCTTT(int x)
        {
            String sTruyVan = string.Format("update CTTT set TinhTrangTonTai ='0'  where MaPK ='{0}'", x);

            Con = DataProvider_1.KetNoi();
            try
            {
                // thuc thi truy van
               bool KetQua = DataProvider_1.ThucThiTruyVanNonQuery(sTruyVan, Con);

                // dong truy van
                DataProvider_1.DongKetNoi(Con);
                if (KetQua == true)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                DataProvider_1.DongKetNoi(Con);
                return false;
            }
        }

        public bool XoaHoaDon(int x)
        {
            String sTruyVan = string.Format("update HOADON set TinhTrangTonTai ='0' where MaPK ='{0}'", x);

            Con = DataProvider_1.KetNoi();
            try
            {
                // thuc thi truy van
                bool KetQua = DataProvider_1.ThucThiTruyVanNonQuery(sTruyVan, Con);

                // dong truy van
                DataProvider_1.DongKetNoi(Con);
                if (KetQua == true)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.StackTrace);
                DataProvider_1.DongKetNoi(Con);
                return false;
            }
        }



        public bool XoaPhieKham(int x)
        {
            String sTruyVan = string.Format("update PHIEUKHAM set TinhTrangTonTai = '0' where MaPK ='{0}'", x);

            Con = DataProvider_1.KetNoi();
            try
            {
                // thuc thi truy van
               bool KetQua = DataProvider_1.ThucThiTruyVanNonQuery(sTruyVan, Con);

                // dong truy van
                DataProvider_1.DongKetNoi(Con);
                if (KetQua == true)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                DataProvider_1.DongKetNoi(Con);
                return false;
            }
        }
        public void DuyetBang(DataTable dt)
        {

            if (dt.Rows.Count == 0)
                return;
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                int x = int.Parse(dt.Rows[i]["MaPK"].ToString());

                if (XoaCTTT(x) == true)
                {
                    if (XoaHoaDon(x) == true)
                    {
                        if (XoaPhieKham(x) == true)
                        {

                        }
                        else
                            return;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                    return;

            }
        }

        public bool XoaBacSi(BACSI bnDTO)
        {
            DataTable dt = TaoBang(bnDTO);

      

            if (dt != null)
                DuyetBang(dt);

            
            string sTruyVan = string.Format("update BACSI set TinhTrangTonTai = '0'  where MaBS= N'{0}'", bnDTO.MaBS1);

            Con = DataProvider_1.KetNoi();
            try
            {
                // thuc thi truy van
               bool KetQua = DataProvider_1.ThucThiTruyVanNonQuery(sTruyVan, Con);

                // dong truy van
                DataProvider_1.DongKetNoi(Con);
                if (KetQua == true)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                DataProvider_1.DongKetNoi(Con);
                return false;
            }
        }
    }
}
