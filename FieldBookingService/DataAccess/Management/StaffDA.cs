using CommonLib;
using ObjectInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Management
{
    public class StaffDA : DataAccessBase<StaffInfo>
    {
        public override string ProfileKeyField => CommonLib.ProfileKeyField.STAFF;
        public override string DbTable => CommonLib.DbTable.STAFF;
        public override string InsertSqlText => $@"
                INSERT INTO {DbTable} (
                    StaffId, StaffName, PhoneNumber, IdentityNumber, BirthOfDate, StaffPosition, Sex, Salary,
                    CreatedBy, CreatedDate
                ) VALUES (
                    @StaffId, @StaffName, @PhoneNumber, @IdentityNumber, @BirthOfDate, @StaffPosition, @Sex, @Salary,
                    @CreatedBy, @CreatedDate
                )";
        public override string UpdateSqlText => $@"
                UPDATE {DbTable} SET 
                    StaffName = @StaffName, 
                    StaffPosition = @StaffPosition, 
                    PhoneNumber = @PhoneNumber, 
                    IdentityNumber = @IdentityNumber, 
                    BirthOfDate = @BirthOfDate, 
                    Sex = @Sex, 
                    Salary = @Salary, 
                    
                    ModifiedBy = @ModifiedBy, 
                    ModifiedDate = @ModifiedDate
                WHERE {ProfileKeyField} = @StaffId ";
        public override string MakeCondition(string[] listKeySearch)
        {
            string sqlCondition = "";

            if (listKeySearch != null)
            {
                //StaffName
                if (listKeySearch.Length >= 1 && listKeySearch[0] != "")
                {
                    sqlCondition += $" AND UPPER(StaffName) like N'%{listKeySearch[0].ToUpper()}%' ";
                }

                //PhoneNumber
                if (listKeySearch.Length >= 2 && listKeySearch[1] != "")
                {
                    sqlCondition += $" AND PhoneNumber like '%{listKeySearch[1]}%' ";
                }
            }
            return sqlCondition;
        }
    }
}
