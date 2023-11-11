using Microsoft.Data.SqlClient;
using System.Data;

namespace Task9.Model.DataAccess
{
    public class ConnectionProvider : IConnectionProvider
    {
        private string connectionString = "Data Source=localhost;Integrated Security=True;Encrypt=False";
        private static readonly string databaseConnectionString = "Server =.; Database=Store;Trusted_Connection=True;Encrypt=False";
        public IDbConnection ConnectToDatabase()
        {
            return new SqlConnection(databaseConnectionString);
        }
        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
