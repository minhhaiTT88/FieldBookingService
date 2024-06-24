using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectInfo
{
    public class FieldBookingInfo : MasterDataBase
    {

        public decimal FieldBookingId { get; set; }
        public string Code { get; set; } = string.Empty;
        public decimal FieldId { get; set; }
        public string FieldName { get; set; } = string.Empty;
        public decimal TimeSlotId { get; set; }
        public string TimeSlotText { get; set; } = string.Empty;

        //khác hàng
        public decimal CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        //ngày đặt sân
        public DateTime BookingDate { get; set; }
        public DateTime TimeFrom { get; set; }
        public DateTime TimeTo { get; set; }
        public decimal Deposit { get; set; }
        public decimal FieldPrice { get; set; }


        // trạng thái
        public string Status { get; set; } = string.Empty;
        public string StatusText { get; set; } = string.Empty;

        public string RejectReason { get; set; } = string.Empty;

    }
}
