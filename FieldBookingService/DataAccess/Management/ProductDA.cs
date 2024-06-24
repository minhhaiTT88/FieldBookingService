using CommonLib;
using ObjectInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Management
{
    public class ProductDA : DataAccessBase<ProductInfo>
    {
       

        public override string ProfileKeyField => CommonLib.ProfileKeyField.PRODUCT;
        public override string DbTable => CommonLib.DbTable.PRODUCT;
        public override string InsertSqlText => $@"
                INSERT INTO {DbTable} (
                    ProductId, ProductName, Price, Count, ImageUrl, Description,
                    CreatedBy, CreatedDate
                ) VALUES (
                    @ProductId, @ProductName, @Price, @Count, @ImageUrl, @Description,
                    @CreatedBy, @CreatedDate
                )";
        public override string UpdateSqlText => $@"
                UPDATE {DbTable} SET 
                    ImageUrl = @ImageUrl,
                    ProductName = @ProductName, 
                    Price = @Price, 
                    Count = @Count,  
                    Description = @Description, 
                    
                    ModifiedBy = @ModifiedBy, 
                    ModifiedDate = @ModifiedDate
                WHERE {ProfileKeyField} = @ProductId ";

        public override string MakeCondition(string[] listKeySearch)
        {
            string sqlCondition = "";

            if (listKeySearch != null)
            {
                //ProductName
                if (listKeySearch.Length >= 1 && listKeySearch[0] != "")
                {
                    sqlCondition += $" AND ProductName like '%{listKeySearch[0]}%' ";
                }
            }
            return sqlCondition;
        }

    }
}
