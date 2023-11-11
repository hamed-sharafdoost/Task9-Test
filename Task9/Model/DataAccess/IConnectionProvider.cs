using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task9.Model.DataAccess
{
    public interface IConnectionProvider
    {
        SqlConnection GetConnection();
        IDbConnection ConnectToDatabase();
    }
}
