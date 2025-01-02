using InventoryDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryBAL.Interface
{
    public interface IProduct
    {
        /// <summary>
        ///  Used for Save Product Details
        /// </summary>
        /// <param name="productRequest"></param>
        /// <returns></returns>
        APIResponseModel<object> SaveProductDetails(ProductRequest productRequest);
        /// <summary>
        ///  Used for upadte Product Details
        /// </summary>
        /// <param name="productRequest"></param>
        /// <returns></returns>
        APIResponseModel<object> UpdateProductDetails(ProductRequest productRequest);
        /// <summary>
        /// Used for Get All Products
        /// </summary>
        /// <returns></returns>
        List<ProductResponse> GetAllProducts();
        /// <summary>
        /// Used to Get Product By Product Id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        ProductResponse GetProductById(int productId);
        /// <summary>
        ///  Used for Save Product Details
        /// </summary>
        /// <param name="productRequest"></param>
        /// <returns></returns>
        APIResponseModel<object> DeleteProductDetails(int productId);
    }
}
