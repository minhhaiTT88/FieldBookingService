using CommonLib;
using Dapper;
using DataAccess.Base;
using log4net.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using ObjectInfo;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace DataAccess
{
    public class DataAccessBase<T> : IDataAccessBase<T> where T : MasterDataBase, new()
    {
        public virtual string DbPackage { get; } = string.Empty;
        public virtual string ProfileKeyField { get; } = string.Empty;
        public virtual string DbTable => string.Empty;
        public virtual string ViewDBTable => string.Empty;
        public virtual string SeqName => string.Empty;

        public virtual string InsertSqlText => string.Empty;
        public virtual string UpdateSqlText => string.Empty;
        public virtual string SearchSqlText => string.Empty;


        // GET
        #region GET

        public virtual List<T> Search(string requestId, string p_keysearch, string p_orderBy, ref decimal p_total_record, decimal p_from = 0, decimal p_to = 0)
        {
            var requestTime = DateTime.Now;
            Logger.log.Info($"[{requestId}] Start.");

            List<T> result = null;
            using var connection = SQLHelper.GetConnection(Config_Info.gConnectionString);

            if (String.IsNullOrEmpty(p_orderBy))
            {
                p_orderBy = " CreatedDate DESC ";
            }

            //cắt các điều kiện tìm kiếm thanh một ds ví dụ "nhan vien 1|A" => ["nhan vien 1", "A"]
            //map thành cách điều kiện tìm kiếm sql
            string[] listKeySearch = p_keysearch?.Split("|");
            string cond = MakeCondition(listKeySearch);

            //câu lệnh select với các điều kiện tìm kiếm
            string sqlSearch = String.IsNullOrEmpty(SearchSqlText) ? $"SELECT *,  ROW_NUMBER() OVER (ORDER BY {p_orderBy}) AS RowNumber FROM vw_{DbTable} WHERE 1=1 {cond}" : SearchSqlText;

            //câu lệnh select với các điều kiện tìm kiếm và thêm điều kiện phân trang
            string conditionPage = p_from == 0 && p_to == 0 ? "" : $"WHERE RowNumber BETWEEN {p_from} and {p_to}";
            string sqlSearchByPage = $"select a.* FROM ( {sqlSearch} ) a {conditionPage}";
            result = connection.Query<T>(sqlSearchByPage).ToList();

            //lấy tổng bản ghi theo điều kiện tìm kiếm
            string sqlTotal = $"SELECT count(*) FROM ({sqlSearch}) A";
            p_total_record = connection.QueryFirstOrDefault<decimal>(sqlTotal);


            //
            Logger.log.Info($"[{requestId}] End. Tong thoi gian {ConstLog.GetProcessingMilliseconds(requestTime)} (ms)");
            return result;
        }

        public virtual string MakeCondition(string[] listKeySearch)
        {
            return "";
        }

        public virtual List<T> GetAll(string requestId)
        {
            var requestTime = DateTime.Now;
            Logger.log.Info($"[{requestId}] Start.");

            List<T> result = null;

            using var connection = SQLHelper.GetConnection(Config_Info.gConnectionString);

            result = connection.Query<T>($"SELECT * FROM {DbTable}").ToList();

            //
            Logger.log.Info($"[{requestId}] End. Tong thoi gian {ConstLog.GetProcessingMilliseconds(requestTime)} (ms)");
            return result;
        }

        public decimal GetSeqID()
        {
            using var connection = SQLHelper.GetConnection(Config_Info.gConnectionString);

            var nextSequenceValue = connection.QueryFirstOrDefault<decimal>($"SELECT NEXT VALUE FOR seq_{DbTable}");
            //
            return nextSequenceValue;
        }

        public virtual T GetById(string requestId, decimal id)
        {
            using var connection = SQLHelper.GetConnection(Config_Info.gConnectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();

            return GetById(requestId, transaction, id);
        }

        public virtual T GetById(string requestId, IDbTransaction transaction, decimal id)
        {
            var requestTime = DateTime.Now;
            Logger.log.Info($"[{requestId}] Start. id=[{id}]]");

            string sqlText = $"SELECT * FROM vw_{DbTable} WHERE {ProfileKeyField} = @id";
            var result = transaction.Connection.QueryFirstOrDefault<T>(sqlText, new { id }, transaction);

            //
            Logger.log.Info($"[{requestId}] End. Tong thoi gian {ConstLog.GetProcessingMilliseconds(requestTime)} (ms)");
            return result;
        }

        #endregion

        // INSERT
        #region INSERT

        public virtual decimal Insert(string requestId, T data)
        {
            using var connection = SQLHelper.GetConnection(Config_Info.gConnectionString);

            connection.Open();
            using var transaction = connection.BeginTransaction();

            var result = Insert(requestId, transaction, data);
            if (result > 0)
            {
                transaction.Commit();
            }
            else
            {
                transaction.Rollback();
            }

            return result;
        }

        public virtual decimal Insert(string requestId, IDbTransaction transaction, T data)
        {
            var requestTime = DateTime.Now;
            Logger.log.Info($"[{requestId}] Start. data=[{JsonHelper.Serialize(data)}]");

            decimal result = ErrorCodes.Err_Unknown;

            if (data == null)
            {
                result = ErrorCodes.Err_DataNull;
                goto endFunc;
            }

            decimal nextId = GetSeqID();
            //lấy id tự tang từ seq
            CommonFunc.SetProfileKeyField(data, ProfileKeyField, nextId);
            //
            var insertCount = transaction.Connection.Execute(InsertSqlText, data, transaction);
            decimal insertChildCount = -1;
            

            //
            if (insertCount > 0)
            {

                insertChildCount = InsertChildData(requestId, transaction, data);
            }
            result = insertChildCount > 0 ? nextId : ErrorCodes.Err_InvalidData;
        //
        endFunc:
            Logger.log.Info($"[{requestId}] End. Tong thoi gian {ConstLog.GetProcessingMilliseconds(requestTime)} (ms)");
            return result;
        }

        public virtual decimal Insert_List(string requestId, List<T> datas, out List<decimal> resCodes)
        {
            using var connection = SQLHelper.GetConnection(Config_Info.gConnectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();

            var result = Insert_List(requestId, transaction, datas, out resCodes);
            if (result > 0)
            {
                transaction.Commit();
            }
            else
            {
                transaction.Rollback();
            }

            return result;
        }

        public virtual decimal Insert_List(string requestId, IDbTransaction transaction, List<T> datas, out List<decimal> resCodes)
        {
            var requestTime = DateTime.Now;
            Logger.log.Info($"[{requestId}] Start. datas=[{JsonHelper.Serialize(datas)}]");

            resCodes = new List<decimal>();
            decimal result = ErrorCodes.Err_Unknown;

            if (datas == null || datas.Count == 0)
            {
                result = ErrorCodes.Err_DataNull;
                goto endFunc;
            }

            // Duyet insert tung ban ghi
            for (int i = 0; i < datas.Count; i++)
            {
                resCodes.Add(Insert(requestId, transaction, datas[i]));
            }

            //
            if (resCodes.All(x => x > 0))
            {
                result = ErrorCodes.Success;
            }
            else
            {
                result = resCodes.Any(x => x < 0) ? resCodes.FirstOrDefault(x => x < 0) : ErrorCodes.Err_InvalidData;
            }

        //
        endFunc:
            Logger.log.Info($"[{requestId}] End. Tong thoi gian {ConstLog.GetProcessingMilliseconds(requestTime)} (ms)");
            return result;
        }

        public virtual decimal InsertChildData(string requestId, IDbTransaction transaction, T data)
        {
            return ErrorCodes.Success;
        }

        #endregion

        // UPDATE
        #region UPDATE

        public virtual decimal Update(string requestId, T data)
        {
            using var connection = SQLHelper.GetConnection(Config_Info.gConnectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();

            var result = Update(requestId, transaction, data);
            if (result > 0)
            {
                transaction.Commit();
            }
            else
            {
                transaction.Rollback();
            }

            return result;
        }

        public virtual decimal Update(string requestId, IDbTransaction transaction, T data)
        {
            var requestTime = DateTime.Now;
            Logger.log.Info($"[{requestId}] Start. data=[{JsonHelper.Serialize(data)}]");

            decimal result = ErrorCodes.Err_Unknown;

            if (data == null)
            {
                result = ErrorCodes.Err_DataNull;
                goto endFunc;
            }

            //
            var updatedCount = transaction.Connection.Execute(UpdateSqlText, data, transaction);

            result = updatedCount == 1 ? ErrorCodes.Success : ErrorCodes.Err_InvalidData;

            //
            if (result > 0)
            {
                result = UpdateChildData(requestId, transaction, data);
            }

        //
        endFunc:
            Logger.log.Info($"[{requestId}] End. Tong thoi gian {ConstLog.GetProcessingMilliseconds(requestTime)} (ms)");
            return result;
        }

        public virtual decimal Update_List(string requestId, List<T> datas, out List<decimal> resCodes)
        {
            using var connection = SQLHelper.GetConnection(Config_Info.gConnectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();

            var result = Update_List(requestId, transaction, datas, out resCodes);
            if (result > 0)
            {
                transaction.Commit();
            }
            else
            {
                transaction.Rollback();
            }

            return result;
        }

        public virtual decimal Update_List(string requestId, IDbTransaction transaction, List<T> datas, out List<decimal> resCodes)
        {
            var requestTime = DateTime.Now;
            Logger.log.Info($"[{requestId}] Start. datas=[{JsonHelper.Serialize(datas)}]");

            resCodes = new List<decimal>();
            decimal result = ErrorCodes.Err_Unknown;

            if (datas == null || datas.Count == 0)
            {
                result = ErrorCodes.Err_DataNull;
                goto endFunc;
            }

            // Duyet insert tung ban ghi
            for (int i = 0; i < datas.Count; i++)
            {
                resCodes.Add(Update(requestId, transaction, datas[i]));
            }

            //
            if (resCodes.All(x => x > 0))
            {
                result = ErrorCodes.Success;
            }
            else
            {
                result = resCodes.Any(x => x < 0) ? resCodes.FirstOrDefault(x => x < 0) : ErrorCodes.Err_InvalidData;
            }

        //
        endFunc:
            Logger.log.Info($"[{requestId}] End. Tong thoi gian {ConstLog.GetProcessingMilliseconds(requestTime)} (ms)");
            return result;
        }

        public virtual decimal UpdateChildData(string requestId, IDbTransaction transaction, T data)
        {
            return ErrorCodes.Success;
        }

        #endregion


        // UPDATE
        #region DELETE

        public virtual decimal Delete(string requestId, decimal id)
        {
            using var connection = SQLHelper.GetConnection(Config_Info.gConnectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();

            var result = Delete(requestId, transaction, id);
            if (result > 0)
            {
                transaction.Commit();
            }
            else
            {
                transaction.Rollback();
            }

            return result;
        }

        public virtual decimal Delete(string requestId, IDbTransaction transaction, decimal id)
        {
            var requestTime = DateTime.Now;
            Logger.log.Info($"[{requestId}] Start. id=[{id}]");

            decimal result = ErrorCodes.Err_Unknown;

            if (id <= 0)
            {
                result = ErrorCodes.Err_DataNull;
                goto endFunc;
            }
            string sqlText = $"DELETE FROM {DbTable} WHERE {ProfileKeyField} = @id";
            //
            var deleteCount = transaction.Connection.Execute(sqlText, new { id }, transaction);

            result = deleteCount == 1 ? ErrorCodes.Success : ErrorCodes.Err_InvalidData;

        //
        endFunc:
            Logger.log.Info($"[{requestId}] End. Tong thoi gian {ConstLog.GetProcessingMilliseconds(requestTime)} (ms)");
            return result;
        }

        public virtual decimal Delete_List(string requestId, List<decimal> ids, out List<decimal> resCodes)
        {
            using var connection = SQLHelper.GetConnection(Config_Info.gConnectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();

            var result = Delete_List(requestId, transaction, ids, out resCodes);
            if (result > 0)
            {
                transaction.Commit();
            }
            else
            {
                transaction.Rollback();
            }

            return result;
        }

        public virtual decimal Delete_List(string requestId, IDbTransaction transaction, List<decimal> ids, out List<decimal> resCodes)
        {
            var requestTime = DateTime.Now;
            Logger.log.Info($"[{requestId}] Start. ids=[{JsonHelper.Serialize(ids)}]");

            resCodes = new List<decimal>();
            decimal result = ErrorCodes.Err_Unknown;

            if (ids == null || ids.Count == 0)
            {
                result = ErrorCodes.Err_DataNull;
                goto endFunc;
            }

            // Duyet insert tung ban ghi
            for (int i = 0; i < ids.Count; i++)
            {
                resCodes.Add(Delete(requestId, transaction, ids[i]));
            }

            //
            if (resCodes.All(x => x > 0))
            {
                result = ErrorCodes.Success;
            }
            else
            {
                result = resCodes.Any(x => x < 0) ? resCodes.FirstOrDefault(x => x < 0) : ErrorCodes.Err_InvalidData;
            }

        //
        endFunc:
            Logger.log.Info($"[{requestId}] End. Tong thoi gian {ConstLog.GetProcessingMilliseconds(requestTime)} (ms)");
            return result;
        }

        #endregion
    }
}
