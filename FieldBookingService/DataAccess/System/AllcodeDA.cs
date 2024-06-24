using CommonLib;
using ObjectInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.System
{
    public class AllCodeDA : DataAccessBase<AllCodeInfo>
    {
        public override string DbTable => CommonLib.DbTable.ALLCODE;
    }
}
