using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ObjectInfo.Core;
using CommonLib;
using System.Net.Http;
using System.Text.Json;

namespace FieldBookingService.Helper
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class ApiAuthorizeAttribute : ActionFilterAttribute
    {
        public bool CheckFunction { get; init; } = true;
        public string Action { get; init; } = string.Empty;

        //
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var functionConfig = GetApiAuthorizeFunctionConfig(context);

            Tuple<HttpStatusCode, LoggedUser> verifyResult = await ValidateFunction(functionConfig?.FunctionId ?? string.Empty, Action, CheckFunction, context);

            if (verifyResult == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (verifyResult.Item1 != HttpStatusCode.OK)
            {
                switch (verifyResult.Item1)
                {
                    case HttpStatusCode.Unauthorized:
                        context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        break;
                    case HttpStatusCode.Forbidden:
                        context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                        break;
                    default:
                        context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                        break;
                }

                return;
            }

            context.HttpContext.Items["User"] = verifyResult.Item2;

            await next();
        }

        private ApiAuthorizeFunctionConfigAttribute? GetApiAuthorizeFunctionConfig(ActionExecutingContext context)
        {
            var attributes = context.Controller.GetType().GetCustomAttributes(typeof(ApiAuthorizeFunctionConfigAttribute), true);

            if (attributes != null && attributes.Length > 0)
            {
                return (ApiAuthorizeFunctionConfigAttribute)attributes[0];
            }

            return null;
        }

        public async Task<Tuple<HttpStatusCode, LoggedUser>> ValidateFunction(string functionId, string action, bool checkFunction, ActionExecutingContext context, string checkMode = "")
        {
            if (context.HttpContext == null)
            {
                return default;
            }


            var response = HttpStatusCode.OK;

            //
            LoggedUser user = null;
            //lấy user từ db
            if (user == null || user.UserName == "")
            {
                response = HttpStatusCode.BadRequest;
            }
            else
            {
                //check quyền
                //không phải tài khoản admin mới cần check quyền
                if (user.UserType != User_Type.admin)
                {
                    //check quyền trong bảng user_function
                    //nếu không tồn tại quyền thì chặn
                    var havePremission = false;
                    if (havePremission == false)
                    {
                        response = HttpStatusCode.Forbidden;
                    }
                }
            }
            //
            return new Tuple<HttpStatusCode, LoggedUser>(response, user);
        }

    }
}
