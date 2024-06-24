using BusinessLayer.Base;
using DataAccess;
using DataAccess.Base;
using DataAccess.Management;
using ObjectInfo;
using System.Linq;

namespace Business
{
    public class FieldInfoBL : BusinessBase<FieldInfo>
    {
        public FieldInfoBL(IDataAccessBase<FieldInfo> dataAccessBase) : base(dataAccessBase)
        {

        }

        public override string ProfileKeyField => CommonLib.ProfileKeyField.FIELD;
    }
}
