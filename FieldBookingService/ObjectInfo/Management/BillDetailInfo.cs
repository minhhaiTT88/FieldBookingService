using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectInfo
{
    public class BillDetailInfo : MasterDataBase
    {
        public decimal BillDetailId { get; set; }
        public decimal BillId { get; set; }
        public string Type { get; set; } = string.Empty;
        public decimal ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal Count { get; set; }
        public decimal Total { get; set; }
    }
}
