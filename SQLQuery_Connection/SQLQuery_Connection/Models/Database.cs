using System.Data;
using System.Data.SqlClient;

public class DatabaseConnection
{
    private SqlConnection connection;
    private string connectionString = "Server=DESKTOP-LTA3PDR;Database=ConnectionDB;Integrated Security=true;";

    public DatabaseConnection()
    {
        connection = new SqlConnection(connectionString);
    }

    public void OpenConnection()
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }
    }

    public void CloseConnection()
    {
        if (connection.State == ConnectionState.Open)
        {
            connection.Close();
        }
    }

}
