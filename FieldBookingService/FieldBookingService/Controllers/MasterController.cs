using CommonLib;
using Microsoft.AspNetCore.Mvc; 
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using ObjectInfo;
using FieldBookingService.Helper;
using FieldBookingService.Memory;
using Microsoft.AspNetCore.Authorization;
using BusinessLayer.Base;  // chứa lớp logic nghiệp vụ 
using Newtonsoft.Json;
using ObjectInfo.Core;

namespace BOShare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterDataBaseController<T> : ControllerBase where T : MasterDataBase, new()
    {
        public string FunctionId => GetApiAuthorizeFunctionConfig()?.FunctionId ?? string.Empty;

        public readonly IBusinessBase<T> BusinessBase;


        public MasterDataBaseController(IBusinessBase<T> businessBase)
        {
            BusinessBase = businessBase;
        }

        [HttpPost("search")]
        public virtual async Task<ResponseSearchInfo?> Search(string? keysearch, decimal from = 0, decimal to = 0, string orderBy = "")
        {
            var requestId = Utils.GenGuidStringN();
            var requestTime = DateTime.Now;
            var clientInfo = Request.GetClientInfo();
            //
            ResponseSearchInfo responseSearchInfo = new ResponseSearchInfo();

            List<T>? list = null;
            decimal p_total_record = 0;

            try
            {
                list = await Task.Run(() => BusinessBase.Search(requestId, keysearch, orderBy, ref p_total_record, from, to));
                responseSearchInfo.totalrows = p_total_record;
                responseSearchInfo.jsondata = JsonConvert.SerializeObject(list);
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
                responseSearchInfo
            }));

            return responseSearchInfo;
        }

        [HttpGet("GetAll")]
        public virtual async Task<List<T>?> GetAll()
        {
            var requestId = Utils.GenGuidStringN();
            var requestTime = DateTime.Now;
            var clientInfo = Request.GetClientInfo();
            //
            List<T>? response = null;

            try
            {
                response = await Task.Run(() => BusinessBase.GetAll(requestId));
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


        //
        //[Authorize]
        //[ApiAuthorize(Action = AuthenAction.DETAIL)]
        [HttpGet("getdetailbyid")]
        public virtual async Task<T?> GetDetailById([FromQuery] decimal value)
        {
            var requestId = Utils.GenGuidStringN();
            var requestTime = DateTime.Now;
            var clientInfo = Request.GetClientInfo();
            //
            T? response = null;

            try
            {
                response = await Task.Run(() => BusinessBase.GetById(requestId, value));

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

        // Create
        //[Authorize]
        //[ApiAuthorize(Action = AuthenAction.INSERT)]
        [HttpPost("insert")]
        public virtual async Task<MasterDataBaseBusinessResponse> Create([FromBody] T data)
        {
            var requestId = Utils.GenGuidStringN();
            var requestTime = DateTime.Now;
            var clientInfo = Request.GetClientInfo();

            MasterDataBaseBusinessResponse response = new();

            try
            {
                var result = await Task.Run(() =>
                {
                    var createResult = BusinessBase.Insert(requestId, data, clientInfo);
                    return new Tuple<decimal>(createResult);
                });

                //
                response.code = result.Item1;
                response.message = "Thêm mới thành công";

                if (response.code <= 0)
                {
                    response.message = "Thêm mới thất bại";
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

        // Update
        //[Authorize]
        //[ApiAuthorize(Action = AuthenAction.UPDATE)]
        [HttpPut("update")]
        public virtual async Task<MasterDataBaseBusinessResponse> Update([FromBody] T data)
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
                    var createResult = BusinessBase.Update(requestId, data, clientInfo);
                    return new Tuple<decimal>(createResult);
                });

                //
                response.code = result.Item1;
                response.message = "Cập nhật thành công";

                if (response.code <= 0)
                {
                    response.message = "Cập nhật thất bại";
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

        // Delete
        //[Authorize]
        //[ApiAuthorize(Action = AuthenAction.DELETE)]
        [HttpDelete("delete")]
        public virtual async Task<MasterDataBaseBusinessResponse> Delete([FromQuery] decimal value)
        {
            var requestId = Utils.GenGuidStringN();
            var requestTime = DateTime.Now;
            var clientInfo = Request.GetClientInfo();

            MasterDataBaseBusinessResponse response = new();

            try
            {
                var result = await Task.Run(() => BusinessBase.Delete(requestId, value, clientInfo));

                response.code = result;
                response.message = "Xóa thành công";

                if (response.code <= 0)
                {
                    response.message = "Xóa thất bại";
                }

            }
            catch (Exception ex)
            {
                Logger.log.Error(ex, $"[{requestId}] {ex.Message}");

                response.code = ErrorCodes.Err_Exception;
                response.message = DefErrorMem.GetErrorDesc(ErrorCodes.Err_Exception);

                Response.StatusCode = StatusCodes.Status500InternalServerError;
            }

            Logger.log.Info(JsonHelper.Serialize(new
            {
                requestId,
                requestTime,
                responseTime = DateTime.Now,
                processTime = ConstLog.GetProcessingMilliseconds(requestTime),
                clientInfo,
                request = value,
                response
            }));

            return response;
        }

        // Private Funcs
        #region Private Funcs

        private ApiAuthorizeFunctionConfigAttribute? GetApiAuthorizeFunctionConfig()
        {
            var attributes = this.GetType().GetCustomAttributes(typeof(ApiAuthorizeFunctionConfigAttribute), true);

            if (attributes != null && attributes.Length > 0)
            {
                return (ApiAuthorizeFunctionConfigAttribute)attributes[0];
            }

            return null;
        }

        #endregion
    }
}
