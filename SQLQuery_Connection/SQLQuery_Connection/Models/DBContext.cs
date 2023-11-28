using SQLQuery_Connection.Models;

namespace SQLQuery_Connection.Models
{
    public class DBContext
    {
    }
}
using System.Data.Entity;

public class YourDbContext : DBContext
{
    public YourDbContext() : base("Server=DESKTOP-LTA3PDR;Database=ConnectionDB;Integrated Security=true;")
    {
    }

    // Định nghĩa các DbSet để ánh xạ với các bảng trong cơ sở dữ liệu
}
