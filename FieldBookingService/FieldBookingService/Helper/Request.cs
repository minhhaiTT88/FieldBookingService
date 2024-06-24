﻿using FieldBookingService.Memory;
using ObjectInfo;
using System.Security.Claims;

namespace FieldBookingService
{
    public static class Request
    {
        public static SAUser GetUserInfo(this HttpRequest httpRequest)
        {
            if (httpRequest.HttpContext.User.Identity == null || !httpRequest.HttpContext.User.Identity.IsAuthenticated)
                return new();
            var claims = ((ClaimsIdentity)httpRequest.HttpContext.User.Identity).Claims;
            var userPid = claims.FirstOrDefault(x => x.Type == CustomClaimTypes.UserId)?.Value;

            long.TryParse(userPid ?? string.Empty, out var userPidValue);
            var user = LoginMem.GetUser(userPidValue);

            if (user == null)
            {
                return null;
            }
            return new SAUser
            {
                UserId = user.UserId,
                Full_Name = user.Full_Name,
                User_Name = user.User_Name,
            };
        }


    }
}
