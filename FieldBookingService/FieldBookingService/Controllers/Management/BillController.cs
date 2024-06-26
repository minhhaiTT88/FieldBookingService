using BOShare.Controllers;
using BusinessLayer.Base;
using CommonLib;
using DataAccess.Management;
using FieldBookingService.Helper;
using FieldBookingService.Memory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ObjectInfo;

namespace FieldBookingService.Controllers.Management
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiAuthorizeFunctionConfig(AuthenFunctionId.BILL)]
    public class BillController : MasterDataBaseController<BillInfo>
    {

        public BillController(IBusinessBase<BillInfo> businessBase) : base(businessBase)
        {

        }

        // Update
        //[Authorize]
        //[ApiAuthorize(Action = AuthenAction.UPDATE)]
        [HttpPost("PaymentField")]
        public virtual async Task<MasterDataBaseBusinessResponse> PaymentField([FromBody] BillInfo data)
        {
            var requestId = Utils.GenGuidStringN();
            var requestTime = DateTime.Now;
            var clientInfo = Request.GetClientInfo();

            //
            MasterDataBaseBusinessResponse response = new();

            try
            {
                data.CreatedBy = clientInfo?.UserName ?? string.Empty;
                data.CreatedDate = clientInfo?.ActionTime ?? DateTime.Now;

                var result = await Task.Run(() =>
                {
                    var createResult = new BillDA().PaymentField(requestId, data);
                    return new Tuple<decimal>(createResult);
                });

                //
                response.code = result.Item1;
                response.message = "Thanh toán thành công";

                if (response.code <= 0)
                {
                    response.message = "Thanh toán thất bại.";
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
