﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Data.SqlClient;
using System.Data;


namespace DAO
{
  public class THUOC_DAO
    {
        static SqlConnection Con;
        //Load THUOC
        public static List<THUOC> LoadTHUOC()
        {
            // khai bao truy van sql
            string sTruyVan = "select * from THUOC where TinhTrangTonTai = '1'";
            // Mo ket noi
            Con = DataProvider_1.KetNoi();
            // Tien hanh truy van
            DataTable dt = DataProvider_1.LayDataTable(sTruyVan, Con);
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            // tao List THUOC_DTO
            List<THUOC> listbn = new List<THUOC>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                THUOC bn = new THUOC();
                bn.Mathuoc1= int.Parse(dt.Rows[i]["MaThuoc"].ToString());
                bn.TenThuoc1 = dt.Rows[i]["TenThuoc"].ToString();
              
                bn.DonViTinh1= dt.Rows[i]["DonViTinh"].ToString();

                bn.Gia1= float.Parse(dt.Rows[i]["Gia"].ToString());

                bn.TinhTrangTonTai1 = int.Parse(dt.Rows[i]["TinhTrangTonTai"].ToString());

                listbn.Add(bn);
            }
            DataProvider_1.DongKetNoi(Con);
            return listbn;
        }
        // ham kiem tra ton tai
        public static bool KiemTraTonTai(THUOC bnDTO)
        {
            string sTruyVan = string.Format("SELECT MaThuoc from THUOC WHERE TenThuoc =N'{0}'  AND TinhTrangTonTai = '1'", bnDTO.TenThuoc1); // MaBN tu dong tang
            Con = DataProvider_1.KetNoi();
            try
            {
                // thuc thi truy van
                bool kq = DataProvider_1.ThucThiTruyVanNonQuery(sTruyVan, Con);
                // dong ket noi
                DataProvider_1.DongKetNoi(Con);

                if (kq == true)
                {
                    DataTable dt = DataProvider_1.LayDataTable(sTruyVan, Con);
                    DataProvider_1.DongKetNoi(Con);
                    if (dt.Rows.Count == 0)
                    {
                        return false;
                    }
                    else
                        return true;
                }  
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
        // Them THUOC
        public static bool ThemTHUOC(THUOC bnDTO)
        {
            // tao cau truy van
            // string sTruyVan = @"insert into THUOC(MaBN,TenBN) value"; -- them mot vai thanh phan vao bang
            string sTruyVan = string.Format("insert into THUOC values (N'{0}',N'{1}','{2}', '1')",  bnDTO.TenThuoc1 , bnDTO.DonViTinh1 , bnDTO.Gia1, bnDTO.TinhTrangTonTai1 );// them day du thong tin cua bang
            Con = DataProvider_1.KetNoi();
            try
            {
                // thuc thi truy van
                bool KetQua = DataProvider_1.ThucThiTruyVanNonQuery(sTruyVan, Con);
                // dong ket noi
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
            // DataProvider_1.DongKetNoi(Con);


        }

  
        public static bool SuaTHUOC(THUOC bnDTO)
        {
            string sTruyVan = string.Format("update THUOC set TenThuoc= N'{0}', DonViTinh=N'{1}', Gia={2} where MaThuoc='{3}'", bnDTO.TenThuoc1, bnDTO.DonViTinh1, bnDTO.Gia1, bnDTO.Mathuoc1);
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

        // xoa thuoc
        public static bool XoaCTTT(int x)
        {
            String sTruyVan = string.Format("update CTTT set TinhTrangTonTai = '0' where MaThuoc ='{0}'", x);

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

        public static DataTable TaoBang(THUOC thuoc)
        {
            
            string query = string.Format("select CTTT.MaThuoc from CTTT  where  CTTT.MaThuoc =N'{0}'", thuoc.Mathuoc1);

            Con = DataProvider_1.KetNoi();

            DataTable dt = DataProvider_1.LayDataTable(query, Con);

            DataProvider_1.DongKetNoi(Con);

            if (dt.Rows.Count == 0)
                return null;
            else
                return dt;
        }
        public static void DuyetBang(DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
             
                int x = int.Parse(dt.Rows[i]["MaThuoc"].ToString());
         

                if (XoaCTTT(x) == true)
                {
                    
                }
                else
                    return;

            }
        }

        public static bool XoaTHUOC(THUOC bnDTO)
        {
            DataTable dt = TaoBang(bnDTO);

            if (dt != null)
                DuyetBang(dt);

            String sTruyVan = string.Format("update THUOC set TinhTrangTonTai = '0' where MaThuoc ='{0}'", bnDTO.Mathuoc1);
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
