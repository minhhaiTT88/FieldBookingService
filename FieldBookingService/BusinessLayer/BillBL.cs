using BusinessLayer.Base;
using DataAccess;
using DataAccess.Base;
using DataAccess.Management;
using ObjectInfo;
using System.Linq;

namespace Business
{
    public class BillBL : BusinessBase<BillInfo>
    {
        public BillBL(IDataAccessBase<BillInfo> dataAccessBase) : base(dataAccessBase)
        {

        }

        public override string ProfileKeyField => CommonLib.ProfileKeyField.BILL;
    }
}
