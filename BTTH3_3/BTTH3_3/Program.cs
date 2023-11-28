using System;
using System.Data.SqlClient;

namespace BTTH3_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=DESKTOP-LTA3PDR;Initial Catalog=QLBH_TH;Integrated Security=True";

            using (var connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE SANPHAM SET GIA = GIA * 1.05 WHERE NUOCSX = 'Thai Lan'";
                string query2 = "UPDATE SANPHAM m SET GIA = GIA * 1.05 WHERE NUOCSX = 'Trung Quoc' AND GIA <= 10000";
                string query3 = "SELECT MASP, TENSP FROM SANPHAM WHERE NUOCSX = 'Trung Quoc'";
                string query4 = "SELECT MASP, TENSP FROM SANPHAM WHERE DVT = 'cay' OR DVT = 'quyen'"; 
                string query5 = "SELECT MASP, TENSP FROM SANPHAM WHERE MASP LIKE 'B%01'";
                string query6 = "SELECT MASP, TENSP FROM SANPHAM WHERE NUOCSX = 'Trung Quoc' AND GIA BETWEEN 30000 AND 40000";


                //using (var command = new SqlCommand(query, connection))
                //{
                //    try
                //    {
                //        connection.Open();
                //        int rowsAffected = command.ExecuteNonQuery();
                //        if (rowsAffected > 0) {
                //            Console.WriteLine("UPDATE SUCCESSFULLY!");
                //        }

                //    }
                //    catch (Exception ex)
                //    {
                //        Console.WriteLine("Lỗi: " + ex.Message);
                //    }
                //}
                using (var command = new SqlCommand(query6, connection))
                {
                    try
                    {
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("MASP: {0}, TENSP: {1}", reader["MASP"], reader["TENSP"]);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Lỗi: " + ex.Message);
                    }
                }
            }
        }
    }
}
