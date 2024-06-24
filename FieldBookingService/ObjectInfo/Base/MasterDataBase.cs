using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectInfo
{
    public class MasterDataBase
    {
        public string Description { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; } = string.Empty;
        public DateTime? ModifiedDate { get; set; }
    }

    public class MasterDataBaseBusinessResponse
    {
        public decimal code { get; set; }
        public string message { get; set; } = string.Empty;
        public string jsonData { get; set; } = string.Empty;
        public string PropertyName { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
    }
}
