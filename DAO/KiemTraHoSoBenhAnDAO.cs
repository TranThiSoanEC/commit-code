using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
    public class KiemTraHoSoBenhAnDAO
    {
        private string connectionString;

        public KiemTraHoSoBenhAnDAO()
        {
            connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        }

        public string ConnectionString
        {
            get
            {
                return connectionString;
            }

            set
            {
                connectionString = value;
            }
        }

        public bool selectByMaBN(List<KiemTraHoSoBenhAnDTO> LichSuKB, KiemTraHoSoBenhAnDTO k)
        {

            string query = "";
            query += @"select [dbo].[PHIEUKHAM].[MaPK]";
            query += @"      , [dbo].[PHIEUKHAM].[NgayKham]";
            query += @" from [dbo].[PHIEUKHAM] ";
            query += @"where [dbo].[PHIEUKHAM].[MaBN]= @MaBN order by [NgayKham] desc";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {

                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = query;
                    command.Parameters.AddWithValue("@MaBN", k.Mabn);
                    try
                    {
                        connection.Open();
                        LichSuKB.Clear();

                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    KiemTraHoSoBenhAnDTO kt = new KiemTraHoSoBenhAnDTO();
                                    kt.Mapk = int.Parse(dr["MaPK"].ToString());
                                    kt.Ngaykham = DateTime.Parse(dr["NgayKham"].ToString());
                                    LichSuKB.Add(kt);

                                }
                            }
                        }
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        connection.Close();
                        return false;
                    }
                }
            }
            return true;
        }
        public KiemTraHoSoBenhAnDTO selectCTPK(KiemTraHoSoBenhAnDTO k)
        {
            KiemTraHoSoBenhAnDTO CTPK = new KiemTraHoSoBenhAnDTO();
            string query = "";
            //query += @"select [dbo].[BENHNHAN].[TenBN]";
            query += @"select[dbo].[PHIEUKHAM].[NgayKham]";
            query += @", [dbo].[BENHNHAN].[TenBN]";
            query += @", [dbo].[BENHNHAN].[NgaySinh]";
            query += @", [dbo].[BENHNHAN].[GioiTinh]";
            query += @", [dbo].[BACSI].[TenBS]";
            query += @", [dbo].[PHIEUKHAM].[TrieuChung]";
            query += @", [dbo].[PHIEUKHAM].[KetQua]";
            query += @"from[dbo].[BENHNHAN]";
            query += @", [dbo].[PHIEUKHAM]";
            query += @", [dbo].[BACSI]";
            query += @"where[dbo].[PHIEUKHAM].[MaBN] = [dbo].[BENHNHAN].[MaBN] and[dbo].[PHIEUKHAM].[MaBS] = [dbo].[BACSI].[MaBS] and[MaPK] = @MaPK";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {

                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = query;
                    command.Parameters.AddWithValue("@MaPK", k.Mapk);
                    try
                    {
                        connection.Open();
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                if (dr.Read())
                                {
                                    CTPK.Ngaykham = DateTime.Parse(dr["NgayKham"].ToString());
                                    CTPK.Tenbn = dr["TenBN"].ToString();
                                    CTPK.Ngaysinh = DateTime.Parse(dr["NgaySinh"].ToString());
                                    CTPK.Gioitinh = dr["GioiTinh"].ToString();
                                    CTPK.Bacsi = dr["TenBS"].ToString();
                                    CTPK.Trieuchung = dr["TrieuChung"].ToString();
                                    CTPK.Chuandoan = dr["KetQua"].ToString();
                                }
                            }
                        }


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        connection.Close();
                        return null;
                    }
                }
            }
            return CTPK;
        }
        public bool selectCTTT(List<KiemTraHoSoBenhAnDTO> CTTT, KiemTraHoSoBenhAnDTO k)
        {

            string query = "";
            query += @"select[dbo].[CTTT].[TenThuoc]";
            query += @", [dbo].[CTTT].[SoLuong]";
            query += @", [dbo].[THUOC].[DonViTinh]";
            query += @", [dbo].[CTTT].[DonGia]";
            query += @", [dbo].[CTTT].[CachDung]";
            query += @"from[dbo].[CTTT]";
            query += @", [dbo].[PHIEUKHAM]";
            query += @", [dbo].[THUOC]";
            query += @"where[dbo].[CTTT].[MaPK] = [dbo].[PHIEUKHAM].[MaPK] and[dbo].[THUOC].[TenThuoc] = [dbo].[CTTT].[TenThuoc] and[dbo].[PHIEUKHAM].[MaPK] = @MaPK";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {

                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = query;
                    command.Parameters.AddWithValue("@MaPK", k.Mapk);
                    try
                    {
                        connection.Open();
                        CTTT.Clear();
                        //command.ExecuteNonQuery();
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    KiemTraHoSoBenhAnDTO kt = new KiemTraHoSoBenhAnDTO();
                                    kt.Tenthuoc = dr["TenThuoc"].ToString();
                                    kt.Soluong = Int32.Parse(dr["SoLuong"].ToString());
                                    kt.Donvitinh = dr["DonViTinh"].ToString();
                                    kt.Cachdung = dr["CachDung"].ToString();
                                    kt.Dongia = dr["DonGia"].ToString();
                                    CTTT.Add(kt);

                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        connection.Close();
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
