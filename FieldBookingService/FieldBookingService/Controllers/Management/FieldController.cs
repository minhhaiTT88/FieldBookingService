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
    [ApiAuthorizeFunctionConfig(AuthenFunctionId.FIELD)]
    public class FieldController : MasterDataBaseController<FieldInfo>
    {

        public FieldController(IBusinessBase<FieldInfo> businessBase) : base(businessBase)
        {

        }
    }
}
