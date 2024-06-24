using BOShare.Controllers;
using Business;
using BusinessLayer.Base;
using CommonLib;
using DataAccess.Management;
using FieldBookingService.Helper;
using Microsoft.AspNetCore.Mvc;
using ObjectInfo;

namespace FieldBookingService.Controllers.Management
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiAuthorizeFunctionConfig(AuthenFunctionId.CUSTOMER)]
    public class CustomerController : MasterDataBaseController<CustomerInfo>
    {
        public CustomerController(IBusinessBase<CustomerInfo> businessBase) : base(businessBase)
        {

        }

        [HttpGet("GetPhoneNumber")]
        public virtual async Task<CustomerInfo?> GetPhoneNumber([FromQuery] string value)
        {
            var requestId = Utils.GenGuidStringN();
            var requestTime = DateTime.Now;
            var clientInfo = Request.GetClientInfo();
            //
            CustomerInfo? response = null;

            try
            {
                response = await Task.Run(() => new CustomerDA().GetByPhoneNumber(requestId, value));

                if (response == null)
                {
                    Response.StatusCode = StatusCodes.Status204NoContent;
                }
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex, $"[{requestId}] {ex.Message}");

                Response.StatusCode = StatusCodes.Status500InternalServerError;
            }

            Logger.log.Info(JsonHelper.Serialize(new
            {
                requestId,
                requestTime,
                responseTime = DateTime.Now,
                processTime = ConstLog.GetProcessingMilliseconds(requestTime),
                clientInfo,
                request = new { value },
                response
            }));

            return response;
        }
    }
}
