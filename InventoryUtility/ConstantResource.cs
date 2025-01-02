using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryUtility
{
    public class ConstantResources
    {
        public ConstantResources() { }

        #region Constant Values
        public const string InventoryDbConnection = "InventoryDbConnection";
        public const string ProductId = "ProductId";
        public const string ProductName = "ProductName";
        public const string Quantity = "Quantity";
        public const string Price = "Price";
        public const string CreatedOn = "CreatedOn";
        public const string UpdatedOn = "UpdatedOn";

        #endregion

        #region Sql Parameter
        public const string ParamProductId = "@ProductId";
        public const string ParamProductName = "@ProductName";
        public const string ParamQuantity = "@Quantity";
        public const string ParamPrice = "@Price";
        public const string ParamCreatedOn = "@CreatedOn";
        public const string ParamUpdatedOn = "@UpdatedOn";
        public const string ParamIsSuccess = "@IsSuccess";
        public const string ParamIsError = "@IsError";
        public const string ParamErrorMsg = "@ErrorMsg";

        #endregion

        #region Store Procedure
        public const string spSaveProduct = "spSaveProduct";
        public const string spUpdateProduct = "spUpdateProduct";
        public const string spGetAllProducts = "spGetAllProducts";
        public const string spDeleteProduct = "spDeleteProduct";
        public const string spGetProductById = "spGetProductById";

        #endregion

        #region API Response
        public readonly static string Failed = "Failed";
        public readonly static string Success = "Success";
        public readonly static string InvaidUser = "Invalid User";
        #endregion

        #region Route Files and API Name
        public const string APIRoute = "api";
        public const string AddNewProduct = "AddNewProduct";
        public const string UpdateProduct = "UpdateProduct";
        public const string GetProductById = "GetProductById";
        public const string GetAllProducts = "GetAllProducts";
        public const string DeleteProduct = "DeleteProduct";

        #endregion

    }
}
