using ObjectInfo.Core;

namespace FieldBookingService.Helper
{
    public static class HttpExtensions
    {
        public static ClientInfo GetClientInfo(this HttpRequest request)
        {
            var httpContext = request.HttpContext;

            string ipAddress = string.Empty;
            string wsName = string.Empty;
            string userName = string.Empty;
            string userId = string.Empty;
            DateTime actionTime = DateTime.Now;
            string branchId = string.Empty;
            string remoteIpAddress = httpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? string.Empty;

            //
            ipAddress = httpContext.Request.Headers["X-Forwarded-For"].ToString() ?? string.Empty;
            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = httpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? string.Empty;
            }

            //
            LoggedUser loggedUser = null;
            _ = httpContext.Items.TryGetValue("User", out var getUser);
            if (getUser != null)
            {
                loggedUser = getUser as LoggedUser ?? new();
                userName = loggedUser.UserName;
                userId = loggedUser.UserId;
            }

            //
            return new ClientInfo()
            {
                IpAddress = ipAddress,
                WsName = wsName,
                UserName = userName,
                ActionTime = actionTime.Date != DateTime.MinValue.Date ? actionTime : DateTime.Now,
                BranchId = branchId,
                UserId = userId,
                RemoteIpAddress = remoteIpAddress,
                LoggedUser = loggedUser,
            };
        }

        public static string GetValueIgnoreCase(this IHeaderDictionary headers, string key)
        {
            string value = string.Empty;

            if (headers != null && headers.Count > 0)
            {
                foreach (var entry in headers)
                {
                    if (string.Equals(entry.Key, key, StringComparison.OrdinalIgnoreCase))
                    {
                        value = entry.Value.ToString();
                        break;
                    }
                }
            }

            return value ?? string.Empty;
        }
    }
}
