using CommonLib;
using Dapper;
using ObjectInfo;
using System.Data;

namespace DataAccess.Management
{
    public class BillDA : DataAccessBase<BillInfo>
    {
        private BillDetailDA _billDetailDA;
        private CustomerDA _customerDA;
        private FieldBookingDA _fieldBookingDA;
        public BillDA()
        {
            _billDetailDA = new BillDetailDA();
            _customerDA = new CustomerDA();
            _fieldBookingDA = new FieldBookingDA();
        }
        public override string ProfileKeyField => CommonLib.ProfileKeyField.BILL;
        public override string DbTable => CommonLib.DbTable.BILL;
        public override string InsertSqlText => $@"
                INSERT INTO {DbTable} (
                    BillId, DateCheckout, CustomerId, FieldId, PaymentMethod, Fee, 
                    TotalBeforeDiscount, Discount, Description, Total,
                    CreatedBy, CreatedDate
                ) VALUES (
                    @BillId, @DateCheckout, @CustomerId, @FieldId, @PaymentMethod, @Fee, 
                    @TotalBeforeDiscount, @Discount, @Description, @Total,
                    @CreatedBy, @CreatedDate
                )";

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

                //CustomerName
                if (listKeySearch.Length >= 2 && listKeySearch[1] != "")
                {
                    sqlCondition += $" AND CustomerName like '%{listKeySearch[1]}%' ";
                }
            }
            return sqlCondition;
        }


        //xử lý đặt sân
        public decimal PaymentField(string requestId, BillInfo data)
        {
            using var connection = SQLHelper.GetConnection(Config_Info.gConnectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();

            decimal result = Insert(requestId, transaction, data);
            if (result > 0)
            {
                //cập nhật trạng thái đơn đặt sân
                result = _fieldBookingDA.UpdateStatus(requestId, transaction, data.FieldBookingId, FieldBookingStatus.done, "");
            }
            return result;
        }

        public override decimal Insert(string requestId, IDbTransaction transaction, BillInfo data)
        {
            data.DateCheckout = DateTime.Now;
            decimal result = 1;
            
            //không có mã khác hàng tức là chưa có thông tin khách hàng này trong hệ thông
            // => cần thêm mới thông tin khách hàng
            if (data != null && data?.CustomerId == 0)
            {
                CustomerInfo customerInfo = new CustomerInfo();
                customerInfo.CustomerName = data.CustomerName;
                customerInfo.PhoneNumber = data.PhoneNumber;
                customerInfo.CreatedBy = data.CreatedBy;
                customerInfo.CreatedDate = data.CreatedDate;

                result = _customerDA.Insert(requestId, transaction, customerInfo);
                data.CustomerId = result;
            }

            //kiểm tra để xử lý thông tin khách hàng
            if (result > 0)
            {
               
                //gọi lại hàm insert chung của class cha để insert bill
                result = base.Insert(requestId, transaction, data);
            }

            return result;
        }

        public override decimal InsertChildData(string requestId, IDbTransaction transaction, BillInfo data)
        {
            decimal result = ErrorCodes.Success;
            if (data != null && data.BillDetails?.Count > 0)
            {
                data.BillDetails.ForEach(x => { x.BillId = data.BillId; });
                result = _billDetailDA.Insert_List(requestId, transaction, data.BillDetails, out var rescode);
            }
            return result;
        }

        public override BillInfo GetById(string requestId, decimal id)
        {
            var data = base.GetById(requestId, id);
            if (data != null)
            {
                data.BillDetails = _billDetailDA.GetByParentId(requestId, id);
            }
            return data;
        }
    }
}
