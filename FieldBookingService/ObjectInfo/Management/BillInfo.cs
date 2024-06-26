using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectInfo
{
    public class BillInfo : MasterDataBase
    {
        public decimal BillId { get; set; }
        public string Code { get; set; } = string.Empty;
        public DateTime DateCheckout { get; set; }
        public decimal CustomerId { get; set; }
        public decimal FieldId { get; set; }
        public string FieldName { get; set; } = string.Empty;
        public decimal FieldBookingId { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string PaymentMethodText { get; set; } = string.Empty;
        public List<BillDetailInfo> BillDetails { get; set; } = new List<BillDetailInfo>();
        public string CustomerName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public decimal Fee { get; set; }
        public decimal Total { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalBeforeDiscount { get; set; }
    }
}
