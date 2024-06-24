using BusinessLayer.Base;
using DataAccess;
using DataAccess.Base;
using DataAccess.Management;
using ObjectInfo;
using System.Linq;

namespace Business
{
    public class FieldBookingBL : BusinessBase<FieldBookingInfo>
    {
        public FieldBookingBL(IDataAccessBase<FieldBookingInfo> dataAccessBase) : base(dataAccessBase)
        {

        }

        public override string ProfileKeyField => CommonLib.ProfileKeyField.FIELDBOOKING;
    }
}
