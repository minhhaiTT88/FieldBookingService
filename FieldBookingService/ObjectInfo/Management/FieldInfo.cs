using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectInfo
{
    public class FieldInfo : MasterDataBase
    {
        public decimal FieldId { get; set; }
        public string FieldName { get; set; } = string.Empty;
        public int Position { get; set; }

        // trạng thái
        public string Status { get; set; } = string.Empty;
        public string StatusText { get; set; } = string.Empty;
        public List<TimeSlotInfo>? TimeSlots { get; set; }
    }
}
