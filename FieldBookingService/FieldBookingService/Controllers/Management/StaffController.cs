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
    [ApiAuthorizeFunctionConfig(AuthenFunctionId.STAFF)]
    public class StaffController : MasterDataBaseController<StaffInfo>
    {

        public StaffController(IBusinessBase<StaffInfo> businessBase) : base(businessBase)
        {

        }
    }
}
