using CommonLib;
using Dapper;
using ObjectInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Management
{
    public class TimeSlotDA : DataAccessBase<TimeSlotInfo>
    {
        public override string ProfileKeyField => CommonLib.ProfileKeyField.TIMESLOT;
        public override string DbTable => CommonLib.DbTable.TIMESLOT;
        public override string InsertSqlText => $@"
                INSERT INTO {DbTable} (
                    TimeSlotId, TimeFrom, TimeTo, Position, FieldId, Enable, Price
                ) VALUES (
                    @TimeSlotId, @TimeFrom, @TimeTo, @Position, @FieldId, @Enable, @Price
                )";
        public override string UpdateSqlText => $@"
                UPDATE {DbTable} SET 
                    Enable = @Enable,
                    Price = @Price
                WHERE {ProfileKeyField} = @TimeSlotId ";

        public List<TimeSlotInfo> GetByParentId(string requestId, decimal parentId)
        {
            using var connection = SQLHelper.GetConnection(Config_Info.gConnectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();

            return GetByParentId(requestId, transaction, parentId);
        }

        public List<TimeSlotInfo> GetByParentId(string requestId, IDbTransaction transaction, decimal parentId)
        {
            var requestTime = DateTime.Now;
            Logger.log.Info($"[{requestId}] Start. parentId=[{parentId}]]");

            string sqlText = $"SELECT * FROM {DbTable} WHERE FieldId = @parentId";
            var result = transaction.Connection.Query<TimeSlotInfo>(sqlText, new { parentId }, transaction).ToList();


            //
            Logger.log.Info($"[{requestId}] End. Tong thoi gian {ConstLog.GetProcessingMilliseconds(requestTime)} (ms)");
            return result;
        }

        public long DeleteByParent(string requestId, IDbTransaction transaction, decimal parentId)
        {
            var requestTime = DateTime.Now;
            Logger.log.Info($"[{requestId}] Start. parentId=[{JsonHelper.Serialize(parentId)}]");


            long result = ErrorCodes.Err_Unknown;

            string sqlText = $"DELETE FROM {DbTable} WHERE FieldId = @parentId";
            //
            var deleteCount = transaction.Connection.Execute(sqlText, new { parentId }, transaction);

            result = deleteCount > 0 ? ErrorCodes.Success : ErrorCodes.Err_InvalidData;

        //
        endFunc:
            Logger.log.Info($"[{requestId}] End. Tong thoi gian {ConstLog.GetProcessingMilliseconds(requestTime)} (ms)");
            return result;
        }
    }
}
