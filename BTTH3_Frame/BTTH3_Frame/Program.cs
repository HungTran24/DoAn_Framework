using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTTH3_Frame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var connString = @"server=localhost;user=root;database=Customers;port=3306;password=*";

            var conn = new SqlConnection(connString);
            try
            {
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    Console.WriteLine("Connection opened successfully");
                }
            }
            catch (Exception e)
            {
                if (conn.State != ConnectionState.Open)
                {
                    Console.WriteLine("Connection failed");
                    Console.WriteLine(e.ToString());
                }
            }

        }
    }
}

