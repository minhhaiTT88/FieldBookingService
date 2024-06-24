using CommonLib;
using ObjectInfo.Core;
using ObjectInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Base;
using CommonLib.Extensions;

namespace BusinessLayer.Base
{
    public class BusinessBase<T> : IBusinessBase<T> where T : MasterDataBase, new()
    {
        public virtual string ProfileKeyField { get; } = "Id";
        public IDataAccessBase<T> DataAccessBase { get; private set; }

        public BusinessBase(IDataAccessBase<T> dataAccessBase)
        {
            DataAccessBase = dataAccessBase;
        }

        // DELETE
        #region DELETE

        public virtual decimal Delete(string requestId, decimal id, ClientInfo clientInfo)
        {
            if (id <= 0)
            {
                return ErrorCodes.Err_InvalidData;
            }

            var oldData = DataAccessBase.GetById(requestId, id);
            if (oldData == null || string.IsNullOrEmpty(oldData.GetPropertyValue(ProfileKeyField)?.ToString()))
            {
                return ErrorCodes.Err_NotFound;
            }
            //
            return DataAccessBase.Delete(requestId, id);
        }
        #endregion

        // GET
        #region GET
        public virtual List<T> Search(string requestId, string p_keysearch, string p_orderBy, ref decimal p_total_record, decimal p_from = 0, decimal p_to = 0)
        {
            return DataAccessBase.Search(requestId, p_keysearch, p_orderBy, ref p_total_record, p_from, p_to);
        }
        public virtual List<T> GetAll(string requestId)
        {
            return DataAccessBase.GetAll(requestId);
        }

        public T GetById(string requestId, decimal id)
        {
            return DataAccessBase.GetById(requestId, id);
        }
        #endregion

        // CREATE
        #region CREATE

        public virtual void BeforeCreate(string requestId, T data)
        {

        }


        public virtual decimal Insert(string requestId, T data, ClientInfo clientInfo)
        {


            if (data == null)
            {
                return ErrorCodes.Err_DataNull;
            }

            data.TrimStringProperty();

            data.CreatedBy = clientInfo?.UserName ?? string.Empty;
            data.CreatedDate = clientInfo?.ActionTime ?? DateTime.Now;

            //
            BeforeCreate(requestId, data);


            return DataAccessBase.Insert(requestId, data);
        }
        #endregion

        // UPDATE
        #region UPDATE

        public virtual void BeforeUpdate(string requestId, T oldData, T newData)
        {

        }

        public virtual decimal Update(string requestId, T data, ClientInfo clientInfo)
        {
            if (data == null)
            {
                return ErrorCodes.Err_DataNull;
            }

            // Lay thong tin cu ra check
            var oldData = DataAccessBase.GetById(requestId, Convert.ToDecimal(data.GetPropertyValue(ProfileKeyField)));
            if (oldData == null || string.IsNullOrEmpty(oldData.GetPropertyValue(ProfileKeyField)?.ToString()))
            {
                return ErrorCodes.Err_NotFound;
            }

            //
            data.TrimStringProperty();

            data.ModifiedBy = clientInfo?.UserName ?? string.Empty;
            data.ModifiedDate = clientInfo?.ActionTime ?? DateTime.Now;


            //
            BeforeUpdate(requestId, oldData, data);

            //
            return DataAccessBase.Update(requestId, data);
        }

        #endregion

    }
}
