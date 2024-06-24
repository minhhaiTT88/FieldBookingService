using CommonLib;
using Dapper;
using ObjectInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Management
{
    public class CustomerDA : DataAccessBase<CustomerInfo>
    {
        public override string ProfileKeyField => CommonLib.ProfileKeyField.CUSTOMER;
        public override string DbTable => CommonLib.DbTable.CUSTOMER;
        public override string InsertSqlText => $@"
                INSERT INTO {DbTable} (
                    CustomerId, CustomerName, PhoneNumber, IdentityNumber, 
                    CreatedBy, CreatedDate
                ) VALUES (
                    @CustomerId, @CustomerName, @PhoneNumber, @IdentityNumber, 
                    @CreatedBy, @CreatedDate
                )";
        public override string UpdateSqlText => $@"
                UPDATE {DbTable} SET 
                    CustomerName = @CustomerName, 
                    PhoneNumber = @PhoneNumber, 
                    IdentityNumber = @IdentityNumber,  
                    
                    ModifiedBy = @ModifiedBy, 
                    ModifiedDate = @ModifiedDate
                WHERE {ProfileKeyField} = @CustomerId ";

        public override string MakeCondition(string[] listKeySearch)
        {
            string sqlCondition = "";

            if (listKeySearch != null)
            {
                //CustomerName
                if (listKeySearch.Length >= 1 && listKeySearch[0] != "")
                {
                    sqlCondition += $" AND UPPER(CustomerName) like N'%{listKeySearch[0].ToUpper()}%' ";
                }

                //PhoneNumber
                if (listKeySearch.Length >= 2 && listKeySearch[1] != "")
                {
                    sqlCondition += $" AND PhoneNumber like '%{listKeySearch[1]}%' ";
                }
            }
            return sqlCondition;
        }

        public CustomerInfo GetByPhoneNumber(string requestId, string phoneNumber)
        {
            using var connection = SQLHelper.GetConnection(Config_Info.gConnectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();

            var requestTime = DateTime.Now;
            Logger.log.Info($"[{requestId}] Start. phoneNumber=[{phoneNumber}]]");

            string sqlText = $"SELECT * FROM {DbTable} WHERE PhoneNumber = @phoneNumber";
            var result = transaction.Connection.QueryFirstOrDefault<CustomerInfo>(sqlText, new { phoneNumber }, transaction);

            //
            Logger.log.Info($"[{requestId}] End. Tong thoi gian {ConstLog.GetProcessingMilliseconds(requestTime)} (ms)");
            return result;
        }
    }
}
