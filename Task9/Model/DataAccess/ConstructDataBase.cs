using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.Data.SqlClient;
using System;
using System.IO;

namespace Task9.Model.DataAccess
{
    public class ConstructDataBase
    {
        private string scriptLocation;
        private string triggerLocation;
        private ConnectionProvider connection;
        public ConstructDataBase()
        {
            scriptLocation = Path.Combine(Environment.CurrentDirectory, "script.sql");
            triggerLocation = Path.Combine(Environment.CurrentDirectory, "DeliveryTrigger.sql");
            connection = new ConnectionProvider();
        }
        public void CreateDataBase()
        {
            Server server = new Server(new ServerConnection(connection.GetConnection()));
            if (server.Databases["Store"] == null)
            {
                string sql = File.ReadAllText(scriptLocation);
                string triggerSql = File.ReadAllText(triggerLocation);
                server.ConnectionContext.ExecuteNonQuery(sql);
                server.ConnectionContext.ExecuteNonQuery(triggerSql);
            }
        }
    }
}
