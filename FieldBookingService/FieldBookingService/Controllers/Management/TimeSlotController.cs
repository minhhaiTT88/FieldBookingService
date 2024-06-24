using BOShare.Controllers;
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
    [ApiAuthorizeFunctionConfig(AuthenFunctionId.TIMESLOT)]
    public class TimeSlotController : MasterDataBaseController<TimeSlotInfo>
    {

        public TimeSlotController(IBusinessBase<TimeSlotInfo> businessBase) : base(businessBase)
        {

        }

        [HttpGet("getByFieldId")]
        public async Task<List<TimeSlotInfo>?> GetByFieldId([FromQuery] decimal value)
        {
            var requestId = Utils.GenGuidStringN();
            var requestTime = DateTime.Now;
            var clientInfo = Request.GetClientInfo();
            //
            List<TimeSlotInfo>? response = null;

            try
            {
                response = await Task.Run(() => new TimeSlotDA().GetByParentId(requestId, value));
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
                request = new { },
                response
            }));

            return response;
        }
    }
}
