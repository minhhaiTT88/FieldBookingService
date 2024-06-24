using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectInfo
{
    public class TimeSlotInfo : MasterDataBase
    {
        public decimal TimeSlotId { get; set; }
        public DateTime TimeFrom { get; set; }
        public string TimeFromStr { get; set; } = string.Empty;
        public DateTime TimeTo { get; set; }
        public string TimeToStr { get; set; } = string.Empty;
        public decimal Valid { get; set; }
        public decimal Price { get; set; }
        public bool Enable { get; set; }
        public int Position { get; set; }
        public decimal FieldId { get; set; }
    }
}
