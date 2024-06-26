using CommonLib;
using Dapper;
using ObjectInfo;
using System.Data;


namespace DataAccess.Management
{
    public class BillDetailDA : DataAccessBase<BillDetailInfo>
    {
        private ProductDA _productDA;
        public BillDetailDA()
        {
            _productDA = new ProductDA();
        }
        public override string ProfileKeyField => CommonLib.ProfileKeyField.BILLDETAIL;
        public override string DbTable => CommonLib.DbTable.BILLDETAIL;
        public override string InsertSqlText => $@"
                INSERT INTO {DbTable} (
                    BillDetailId, BillId, ProductId, ProductName, Count, Price, Description
                ) VALUES (
                    @BillDetailId, @BillId, @ProductId, @ProductName, @Count, @Price, @Description
                )";

        public List<BillDetailInfo> GetByParentId(string requestId, decimal parentId)
        {
            using var connection = SQLHelper.GetConnection(Config_Info.gConnectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();

            var requestTime = DateTime.Now;
            Logger.log.Info($"[{requestId}] Start. parentId=[{parentId}]]");

            string sqlText = $"SELECT * FROM vw_{DbTable} WHERE BillId = @parentId";
            var result = transaction.Connection.Query<BillDetailInfo>(sqlText, new { parentId }, transaction).ToList();

            //
            Logger.log.Info($"[{requestId}] End. Tong thoi gian {ConstLog.GetProcessingMilliseconds(requestTime)} (ms)");
            return result;
        }

        public override decimal Insert(string requestId, IDbTransaction transaction, BillDetailInfo data)
        {
            decimal result = base.Insert(requestId, transaction, data);
            if (result > 0 && data?.Type == BillDetailType.product && data?.ProductId > 0)
            {
                //lấy thông tin sản phẩm
                var productOld = _productDA.GetById(requestId, transaction, data.ProductId);
                if (productOld != null)
                {
                    productOld.Count = productOld.Count - data.Count;
                    result = _productDA.Update(requestId, transaction, productOld);
                }
            }
            return result;
        }
    }
}
