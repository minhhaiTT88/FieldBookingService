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
    [ApiAuthorizeFunctionConfig(AuthenFunctionId.FIELDBOOKING)]
    public class FieldBookingController : MasterDataBaseController<FieldBookingInfo>
    {

        public FieldBookingController(IBusinessBase<FieldBookingInfo> businessBase) : base(businessBase)
        {

        }

        // Update
        //[Authorize]
        //[ApiAuthorize(Action = AuthenAction.UPDATE)]
        [HttpPut("UpdateStatus")]
        public virtual async Task<MasterDataBaseBusinessResponse> UpdateStatus([FromQuery] decimal id, [FromQuery] string status, [FromQuery] string? reason)
        {
            var requestId = Utils.GenGuidStringN();
            var requestTime = DateTime.Now;
            var clientInfo = Request.GetClientInfo();

            //
            MasterDataBaseBusinessResponse response = new();

            try
            {
                var result = await Task.Run(() =>
                {
                    var createResult = new FieldBookingDA().UpdateStatus(requestId, id, status, reason);
                    return new Tuple<decimal>(createResult);
                });

                //
                response.code = result.Item1;
                response.message = "Cật nhật trạng thái thành công";

                if (response.code <= 0)
                {
                    response.message = "Cật nhật trạng thái thất bại.";

                    if (response.code == -3)
                    {
                        response.message += " Đã quá hạn duyệt đơn.";
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.log.Error(ex, $"[{requestId}] {ex.Message}");

                response.code = ErrorCodes.Err_Exception;
                response.message = ex.Message;

                Response.StatusCode = StatusCodes.Status500InternalServerError;
            }

            Logger.log.Info(JsonHelper.Serialize(new
            {
                requestId,
                requestTime,
                responseTime = DateTime.Now,
                processTime = ConstLog.GetProcessingMilliseconds(requestTime),
                clientInfo,
                request = id,
                response
            }));

            return response;
        }

        [HttpGet("GetTimeSlotByDate")]
        public virtual async Task<List<TimeSlotInfo>?> GetTimeSlotByDate([FromQuery] decimal fieldId, [FromQuery] DateTime bookingDate)
        {
            var requestId = Utils.GenGuidStringN();
            var requestTime = DateTime.Now;
            var clientInfo = Request.GetClientInfo();
            //
            List<TimeSlotInfo>? response = null;

            try
            {
                response = await Task.Run(() => new FieldBookingDA().GetTimeSlotByDate(requestId, fieldId, bookingDate));
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
