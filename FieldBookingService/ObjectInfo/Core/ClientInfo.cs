using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectInfo.Core
{
    public sealed class ClientInfo
    {
        public string AppName { get; set; } = string.Empty;
        public string IpAddress { get; set; } = string.Empty;
        public string WsName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public DateTime ActionTime { get; set; } = DateTime.Now;
        public string ClientLanguage { get; set; } = string.Empty;
        public string BranchId { get; set; }
        public string UserId { get; set; }
        public string RemoteIpAddress { get; set; } = string.Empty;
        public LoggedUser LoggedUser { get; set; }
    }
}
