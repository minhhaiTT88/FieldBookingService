using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SQLHelper
    {
        public static IDbConnection GetConnection(string connectionString)
        {
            IDbConnection connection = new SqlConnection(connectionString);
            
            //connection.Open();
            return connection;
        }
    }
}
