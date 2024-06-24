using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectInfo
{
    public class StaffInfo : MasterDataBase
    {
        public decimal StaffId { get; set; }
        public string StaffName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string IdentityNumber { get; set; } = string.Empty;
        public string StaffPosition { get; set; } = string.Empty;
        public DateTime BirthOfDate { get; set; }
        public string Sex { get; set; } = string.Empty;
        public string SexText { get; set; } = string.Empty;
        public decimal Salary { get; set; }
    }
}
