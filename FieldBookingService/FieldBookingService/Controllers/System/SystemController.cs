using CommonLib;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ObjectInfo;
using FieldBookingService.Memory;
using System.Data;
using System.Net;

namespace FieldBookingService.Controllers.System
{
    [ApiController]
    public class SystemController : Controller
    {
        [Route("api/system/allcode/get-all"), HttpGet]
        public IActionResult Allcode_GetAll()
        {
            try
            {
                List<AllCodeInfo> data = AllCode_Memory.Allcode_GetAll();
                return Ok(new { jsondata = JsonConvert.SerializeObject(data) });
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError, new { jsondata = JsonConvert.SerializeObject(new List<AllCodeInfo>()) });
            }
        }

        [Route("api/system/allcode/get-by-cdname-cdtype"), HttpGet]
        public IActionResult Allcode_GetByCdNameCdType(string cdName, string cdType)
        {
            try
            {
                List<AllCodeInfo> data = AllCode_Memory.Allcode_GetByCdnameCdtype(cdName, cdType);
                return Ok(new { jsondata = JsonConvert.SerializeObject(data) });
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError, new { jsondata = JsonConvert.SerializeObject(new List<AllCodeInfo>()) });
            }
        }
    }
}
