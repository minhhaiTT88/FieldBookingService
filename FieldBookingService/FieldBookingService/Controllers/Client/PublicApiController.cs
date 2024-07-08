using BOShare.Controllers;
using BusinessLayer.Base;
using CommonLib;
using DataAccess.Management;
using FieldBookingService.Helper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ObjectInfo;

namespace FieldBookingService.Controllers.Management
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicApiController : Controller
    {
        public readonly IBusinessBase<FieldBookingInfo> _fieldBookingBL;

        public PublicApiController(IBusinessBase<FieldBookingInfo> fieldBookingBL)
        {
            _fieldBookingBL = fieldBookingBL;
        }

        [HttpGet("GetFieldsActive")]
        public async Task<List<FieldInfo>?> GetFieldsActive()
        {
            var requestId = Utils.GenGuidStringN();
            var requestTime = DateTime.Now;
            var clientInfo = Request.GetClientInfo();
            //
            List<FieldInfo>? response = null;

            try
            {
                response = await Task.Run(() => new FieldDA().GetFieldsActive(requestId));
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


        [HttpGet("GetTimeSlotByDate")]
        public async Task<List<TimeSlotInfo>?> GetTimeSlotByDate([FromQuery] decimal fieldId, [FromQuery] string bookingDate)
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

        [HttpPost("ConfirmBooking")]
        public async Task<MasterDataBaseBusinessResponse> ConfirmBooking([FromBody] FieldBookingInfo data)
        {
            var requestId = Utils.GenGuidStringN();
            var requestTime = DateTime.Now;
            var clientInfo = Request.GetClientInfo();

            MasterDataBaseBusinessResponse response = new();

            try
            {
                var result = await Task.Run(() =>
                {
                    var createResult = _fieldBookingBL.Insert(requestId, data, clientInfo);
                    return new Tuple<decimal>(createResult);
                });

                

                //
                response.code = result.Item1;

                if (response.code > 0)
                {
                    response.message = "Đặt sân thành công";


                    //gửi thông báo thành công đi bằng web socket
                    Socket_Message_Info _msg = new Socket_Message_Info();
                    _msg.type = "BOOKING_SUCCESS";
                    _msg.content = data;
                    WebsocketHandler.BroardCastMsg(JsonConvert.SerializeObject(_msg));
                }

                

                if (response.code <= 0)
                {
                    response.message = "Đặt sân thành công";
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
                request = data,
                response
            }));

            return response;
        }
    }
}
