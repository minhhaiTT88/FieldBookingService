using CommonLib;
using Dapper;
using Microsoft.SqlServer.Server;
using ObjectInfo;
using System.Collections.Generic;
using System.Data;
using System.Globalization;


namespace DataAccess.Management
{
    public class FieldDA : DataAccessBase<FieldInfo>
    {
        private TimeSlotDA _timeSlotDA;
        public FieldDA()
        {
            _timeSlotDA = new TimeSlotDA();
        }
        public override string ProfileKeyField => CommonLib.ProfileKeyField.FIELD;
        public override string DbTable => CommonLib.DbTable.FIELD;
        public override string InsertSqlText => $@"
                INSERT INTO {DbTable} (
                    FieldId, FieldName, Position, Status, 
                    CreatedBy, CreatedDate
                ) VALUES (
                    @FieldId, @FieldName, @Position, @Status,
                    @CreatedBy, @CreatedDate
                )";
        public override string UpdateSqlText => $@"
                UPDATE {DbTable} SET 
                    FieldName = @FieldName, 
                    Position = @Position, 
                    Status = @Status,
                    Description = @Description,
                    ModifiedBy = @ModifiedBy, 
                    ModifiedDate = @ModifiedDate
                WHERE {ProfileKeyField} = @FieldId ";

        public override string MakeCondition(string[] listKeySearch)
        {
            string sqlCondition = "";

            if (listKeySearch != null)
            {
                //FieldName
                if (listKeySearch.Length >= 1 && listKeySearch[0] != "")
                {
                    sqlCondition += $" AND FieldName like '%{listKeySearch[0]}%' ";
                }

                //Status
                if (listKeySearch.Length >= 2 && listKeySearch[1] != "")
                {
                    sqlCondition += $" AND Status = '{listKeySearch[1]}' ";
                }
            }
            return sqlCondition;
        }

        public List<FieldInfo> GetFieldsActive(string requestId)
        {
            var requestTime = DateTime.Now;
            Logger.log.Info($"[{requestId}] Start.");

            List<FieldInfo> result = null;

            using var connection = SQLHelper.GetConnection(Config_Info.gConnectionString);

            List<FieldInfo> lstAll = connection.Query<FieldInfo>($"SELECT * FROM {DbTable}").ToList();

            result = lstAll.Where((x)=>x.Status.Equals(FieldStatus.active)).ToList();
            //
            Logger.log.Info($"[{requestId}] End. Tong thoi gian {ConstLog.GetProcessingMilliseconds(requestTime)} (ms)");
            return result;
        }
        public override decimal InsertChildData(string requestId, IDbTransaction transaction, FieldInfo data)
        {
            decimal result = ErrorCodes.Success;
            if (data != null && data.TimeSlots?.Count > 0)
            {
                data.TimeSlots.ForEach(x => { x.FieldId = data.FieldId; });
                result = _timeSlotDA.Insert_List(requestId, transaction, data.TimeSlots, out var rescode);
            }
            return result;
        }

        public override decimal UpdateChildData(string requestId, IDbTransaction transaction, FieldInfo data)
        {
            decimal result = ErrorCodes.Success;
            if (data != null && data.TimeSlots?.Count > 0)
            {
                //người dùng đã ghi đè một danh sách khác
                if (data.TimeSlots[0].TimeSlotId <= 0)
                {
                    //xóa và thêm mới lại
                    _timeSlotDA.DeleteByParent(requestId, transaction, data.FieldId);

                    data.TimeSlots.ForEach(x =>
                    {
                        x.FieldId = data.FieldId;
                        // Phân tích chuỗi giờ từ string
                        DateTime _timeFrom;
                        bool success = DateTime.TryParseExact(x.TimeFromStr, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _timeFrom);
                        if (success) x.TimeFrom = _timeFrom;

                        DateTime _timeTo;
                        success = DateTime.TryParseExact(x.TimeToStr, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _timeTo);
                        if (success) x.TimeTo = _timeTo;

                    });
                    result = _timeSlotDA.Insert_List(requestId, transaction, data.TimeSlots, out var rescode);
                }
                else
                {
                    result = _timeSlotDA.Update_List(requestId, transaction, data.TimeSlots, out var rescode);
                }
            }
            return result;
        }

        public override FieldInfo GetById(string requestId, IDbTransaction transaction, decimal id)
        {
            var data = base.GetById(requestId, transaction, id);
            if (data != null)
            {
                data.TimeSlots = _timeSlotDA.GetByParentId(requestId, transaction, id);
            }
            return data;
        }
    }
}
