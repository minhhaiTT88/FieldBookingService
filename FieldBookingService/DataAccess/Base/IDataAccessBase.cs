using ObjectInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Base
{
    public interface IDataAccessBase<T> where T : MasterDataBase, new()
    {
        string DbPackage { get; }
        string ProfileKeyField { get; }
        string DbTable { get; }
        string ViewDBTable { get; }
        string SeqName { get; }
        string InsertSqlText { get; }
        string UpdateSqlText { get; }
        string SearchSqlText { get; }
        List<T> Search(string requestId, string p_keysearch, string p_orderBy, ref decimal p_total_record, decimal p_from = 0, decimal p_to = 0);
        string MakeCondition(string[] litsKeySearch);
        List<T> GetAll(string requestId);
        T GetById(string requestId, decimal id);
        T GetById(string requestId, IDbTransaction transaction, decimal id);
        decimal Insert(string requestId, T data);
        decimal Insert(string requestId, IDbTransaction transaction, T data);
        decimal Insert_List(string requestId, List<T> datas, out List<decimal> resCodes);
        decimal Insert_List(string requestId, IDbTransaction transaction, List<T> datas, out List<decimal> resCodes);
        decimal Update(string requestId, T data);
        decimal Update(string requestId, IDbTransaction transaction, T data);
        decimal Update_List(string requestId, List<T> datas, out List<decimal> resCodes);
        decimal Update_List(string requestId, IDbTransaction transaction, List<T> datas, out List<decimal> resCodes);
        decimal Delete(string requestId, decimal id);
        decimal Delete(string requestId, IDbTransaction transaction, decimal id);
        decimal Delete_List(string requestId, List<decimal> ids, out List<decimal> resCodes);
        decimal Delete_List(string requestId, IDbTransaction transaction, List<decimal> ids, out List<decimal> resCodes);
    }
}
