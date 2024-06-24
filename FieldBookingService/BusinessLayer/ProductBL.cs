using BusinessLayer.Base;
using DataAccess;
using DataAccess.Base;
using DataAccess.Management;
using ObjectInfo;
using System.Linq;

namespace Business
{
    public class ProductBL : BusinessBase<ProductInfo>
    {
        public ProductBL(IDataAccessBase<ProductInfo> dataAccessBase) : base(dataAccessBase)
        {

        }

        public override string ProfileKeyField => CommonLib.ProfileKeyField.PRODUCT;
    }
}
