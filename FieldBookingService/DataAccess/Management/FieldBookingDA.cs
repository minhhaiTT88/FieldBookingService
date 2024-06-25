using CommonLib;
using Dapper;
using ObjectInfo;
using System.Data;


namespace DataAccess.Management
{
    public class FieldBookingDA : DataAccessBase<FieldBookingInfo>
    {
        private CustomerDA _customerDA;
        public FieldBookingDA()
        {
            _customerDA = new CustomerDA();
        }
        public override string ProfileKeyField => CommonLib.ProfileKeyField.FIELDBOOKING;
        public override string DbTable => CommonLib.DbTable.FIELDBOOKING;
        public override string InsertSqlText => $@"
                INSERT INTO {DbTable} (
                    FieldBookingId, Code, FieldId, TimeSlotId, CustomerId, BookingDate, TimeFrom, TimeTo, FieldPrice, Deposit, Status,
                    CreatedBy, CreatedDate
                ) VALUES (
                    @FieldBookingId, @Code, @FieldId, @TimeSlotId, @CustomerId, @BookingDate, @TimeFrom, @TimeTo, @FieldPrice, @Deposit, @Status,
                    @CreatedBy, @CreatedDate
                )";
        public override string UpdateSqlText => $@"
                UPDATE {DbTable} SET 
                    TimeSlotsId = @TimeSlotsId, 
                    CustomerId = @CustomerId, 
                    Date = @Date,  
                    Status = @Status,
                    
                    ModifiedBy = @ModifiedBy, 
                    ModifiedDate = @ModifiedDate
                WHERE {ProfileKeyField} = @FieldBookingId ";

        public override string MakeCondition(string[] listKeySearch)
        {
            string sqlCondition = "";

            if (listKeySearch != null)
            {
                //FieldId
                if (listKeySearch.Length >= 1 && listKeySearch[0] != "")
                {
                    sqlCondition += $" AND FieldId = {listKeySearch[0]} ";
                }

                //CustomerName
                if (listKeySearch.Length >= 2 && listKeySearch[1] != "")
                {
                    sqlCondition += $" AND CustomerName like '%{listKeySearch[1]}%' ";
                }

                //PhoneNumber
                if (listKeySearch.Length >= 3 && listKeySearch[2] != "")
                {
                    sqlCondition += $" AND PhoneNumber like '%{listKeySearch[2]}%' ";
                }

                //Status
                if (listKeySearch.Length >= 4 && listKeySearch[3] != "")
                {
                    sqlCondition += $" AND Status = {listKeySearch[3]} ";
                }

                //Booking date from
                if (listKeySearch.Length >= 5 && listKeySearch[4] != "")
                {
                    sqlCondition += $" AND CAST(BookingDate as DATE) >= '{listKeySearch[4]}' ";
                }

                //Booking date from
                if (listKeySearch.Length >= 6 && listKeySearch[5] != "")
                {
                    sqlCondition += $" AND CAST(BookingDate as DATE) <= '{listKeySearch[5]}' ";
                }

            }
            return sqlCondition;
        }

        //xử lý đặt sân
        public override decimal Insert(string requestId, IDbTransaction transaction, FieldBookingInfo data)
        {
            if (ConflictScheduleBooking(transaction, data) > 0)
            {
                //đã bị chùng lịch
                return -2;
            }

            //khi thêm mới yêu cầu thì mặc định trạng thái đơn sẽ là chờ duyệt
            data.Status = FieldBookingStatus.pending;

            //tự sinh mã đơn
            data.Code = "DS" + DateTime.Now.ToString("ddMMyyyyHHmmssfffftt");

            //kiểm tra để xử lý thông tin khách hàng
            if (data != null)
            {
                //tìm thông tin khách hàng theo sdt
                CustomerInfo customerExisted = _customerDA.GetByPhoneNumber(requestId, data.PhoneNumber);

                //nếu chưa có thông tin khách hàng thì thêm mới, có thông tin trong hệ thống rồi thì cập nhật lại
                if (customerExisted != null)
                {
                    data.CustomerId = customerExisted.CustomerId;
                }
                else
                {
                    CustomerInfo customerNew = new CustomerInfo();
                    customerNew.CustomerName = data.CustomerName;
                    customerNew.PhoneNumber = data.PhoneNumber;
                    customerNew.CreatedBy = data.CreatedBy;
                    customerNew.CreatedDate = data.CreatedDate;

                    data.CustomerId = _customerDA.Insert(requestId, transaction, customerNew);
                }
            }

            return base.Insert(requestId, transaction, data);
        }

        public decimal ConflictScheduleBooking(IDbTransaction transaction, FieldBookingInfo data)
        {
            decimal result = 0;

            string sqlText = $"SELECT count(*) FROM {DbTable} " +
                $"WHERE FieldId = @FieldId AND BookingDate = @BookingDate " +
                $"AND TimeSlotId = @TimeSlotId AND Status = '{FieldBookingStatus.approved}'";

            var param = new { BookingDate = data.BookingDate.ToString("yyyy-MM-dd"), TimeSlotId = data.TimeSlotId, FieldId = data.FieldId };

            result = transaction.Connection.QueryFirstOrDefault<decimal>(sqlText, param, transaction);

            return result;
        }

        public List<TimeSlotInfo> GetTimeSlotByDate(string requestId, decimal fieldId, string bookingDate)
        {
            var requestTime = DateTime.Now;
            Logger.log.Info($"[{requestId}] Start. data=[{JsonHelper.Serialize(new { fieldId, bookingDate })}]");

            using var connection = SQLHelper.GetConnection(Config_Info.gConnectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();

            string sqlText = $"select b.*, a.Valid, FORMAT(b.TimeFrom,'HH\\hmm') + ' - ' + FORMAT(b.TimeTo,'HH\\hmm') AS TimeFormatted from (" +
                $"select TimeSlotId, 0 as Valid  from {CommonLib.DbTable.FIELDBOOKING} a where FieldId = 4 AND BookingDate = @bookingDate AND status != '{FieldBookingStatus.reject}' " +
                $"UNION " +
                $"select TimeSlotId, 1 as Valid from {CommonLib.DbTable.TIMESLOT} b where FieldId = @fieldId) a " +
                $"JOIN {CommonLib.DbTable.TIMESLOT} b ON a.TimeSlotId = b.TimeSlotId " +
                $"ORDER BY b.Position";

            var param = new { bookingDate, fieldId };

            List<TimeSlotInfo> result = transaction.Connection.Query<TimeSlotInfo>(sqlText, param, transaction).ToList();

            return result;
        }


        public virtual decimal UpdateStatus(string requestId, decimal id, string status, string reason)
        {
            using var connection = SQLHelper.GetConnection(Config_Info.gConnectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();


            return UpdateStatus(requestId, transaction, id, status, reason);
        }
        public virtual decimal UpdateStatus(string requestId, IDbTransaction transaction, decimal id, string status, string reason)
        {
            var requestTime = DateTime.Now;
            Logger.log.Info($"[{requestId}] Start. data=[{JsonHelper.Serialize(new { id, status, reason })}]");

            decimal result = ErrorCodes.Err_Unknown;

            if (id == 0)
            {
                result = ErrorCodes.Err_DataNull;
                goto endFunc;
            }

            string sqlText = $@"
                UPDATE {DbTable} SET 
                    Status = @status,
                    RejectReason = @reason
                WHERE {ProfileKeyField} = @id ";

            result = transaction.Connection.Execute(sqlText, new { id, status, reason }, transaction);


            if (result > 0)
            {
                transaction.Commit();
            }
            else
            {
                transaction.Rollback();
            }
        //
        endFunc:
            Logger.log.Info($"[{requestId}] End. Tong thoi gian {ConstLog.GetProcessingMilliseconds(requestTime)} (ms)");
            return result;
        }


    }
}
