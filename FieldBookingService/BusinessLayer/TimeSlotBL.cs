using BusinessLayer.Base;
using DataAccess;
using DataAccess.Base;
using DataAccess.Management;
using ObjectInfo;
using System.Linq;

namespace Business
{
    public class TimeSlotBL : BusinessBase<TimeSlotInfo>
    {
        public TimeSlotBL(IDataAccessBase<TimeSlotInfo> dataAccessBase) : base(dataAccessBase)
        {

        }

        public override string ProfileKeyField => CommonLib.ProfileKeyField.TIMESLOT;
    }
}
