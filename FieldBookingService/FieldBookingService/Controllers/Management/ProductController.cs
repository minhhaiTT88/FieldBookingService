using BOShare.Controllers;
using BusinessLayer.Base;
using CommonLib;
using FieldBookingService.Helper;
using Microsoft.AspNetCore.Mvc;
using ObjectInfo;

namespace FieldBookingService.Controllers.Management
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiAuthorizeFunctionConfig(AuthenFunctionId.PRODUCT)]
    public class ProductController : MasterDataBaseController<ProductInfo>
    {

        public ProductController(IBusinessBase<ProductInfo> businessBase) : base(businessBase)
        {

        }

        // Create
        //[Authorize]
        //[ApiAuthorize(Action = AuthenAction.INSERT)]
        [HttpPost("insert")]
        public override async Task<MasterDataBaseBusinessResponse> Create([FromForm] ProductInfo data)
        {
            var requestId = Utils.GenGuidStringN();
            var requestTime = DateTime.Now;
            var clientInfo = Request.GetClientInfo();

            MasterDataBaseBusinessResponse response = new();

            try
            {
                var result = await Task.Run(() =>
                {
                    string urlFile = UploadSingleFile();
                    if(urlFile.Length > 0)
                    {
                        data.ImageUrl = urlFile;
                    }

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
        public override async Task<MasterDataBaseBusinessResponse> Update([FromForm] ProductInfo data)
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
                    string urlFile = UploadSingleFile();
                    if (urlFile.Length > 0)
                    {
                        data.ImageUrl = urlFile;
                    }

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

        private string UploadSingleFile()
        {
            string url = "";
            var formFiles = this.HttpContext.Request.Form.Files;
            if (formFiles != null && formFiles.Count > 0)
            {
                IFormFile file = formFiles[0];
                if (file != null)
                {
                    string fileName = $"{DateTime.Now.ToString("yyyyMMddHHmmssFFF")}_pr{Path.GetExtension(file.FileName)}";
                    
                    url = UploadHelpers.upload_file(file.OpenReadStream(), fileName);
                }
            }
            return url;

        }

    }
}
