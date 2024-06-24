using Business.Interfaces;
using BusinessLayer.Base;
using DataAccess;
using DataAccess.Base;
using DataAccess.Management;
using ObjectInfo;
using System.Linq;

namespace Business
{
    public class CustomerBL : BusinessBase<CustomerInfo>
    {
        public CustomerBL(IDataAccessBase<CustomerInfo> dataAccessBase) : base(dataAccessBase)
        {

        }


        public override string ProfileKeyField => CommonLib.ProfileKeyField.CUSTOMER;
    }
}
