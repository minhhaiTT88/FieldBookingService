using CommonLib;
using Dapper;
using ObjectInfo;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class User_DA
    {
        public UserInfo User_Login(string userName, string password)
        {
            using var connection = SQLHelper.GetConnection(Config_Info.gConnectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();

            string sqlText = $"SELECT * FROM {CommonLib.DbTable.UserAccount} WHERE UserName = @userName AND Password = @password";
            var result = transaction.Connection.QueryFirstOrDefault<UserInfo>(sqlText, new { userName, password }, transaction);

            return result;
        }
        
    }
}
