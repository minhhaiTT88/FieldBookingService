using ObjectInfo.Core;
using ObjectInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Base;

namespace BusinessLayer.Base
{
    public interface IBusinessBase<T> where T : MasterDataBase, new()
    {
        string ProfileKeyField { get; }
        IDataAccessBase<T> DataAccessBase { get; }

        List<T> Search(string requestId, string p_keysearch, string p_orderBy, ref decimal p_total_record, decimal p_from = 0, decimal p_to = 0);
        List<T> GetAll(string requestId);
        T GetById(string requestId, decimal id);
        void BeforeCreate(string requestId, T data);
        decimal Insert(string requestId, T data, ClientInfo clientInfo);
        void BeforeUpdate(string requestId, T oldData, T newData);
        decimal Update(string requestId, T data, ClientInfo clientInfo);
        decimal Delete(string requestId, decimal id, ClientInfo clientInfo);
    }
}
