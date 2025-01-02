using InventoryBAL.Interface;
using InventoryRepository.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryDTO;
using InventoryUtility;

namespace InventoryBAL.Implementation
{
    public class ProductBAL : IProduct
    {
        private readonly IConfiguration configuration;
        private readonly IProductRepository prdRepository;
        private readonly ProductLogger logger;
        public ProductBAL(IConfiguration config, IProductRepository productRepository)
        {
            configuration = config;
            prdRepository = productRepository;
            logger = new ProductLogger();
        }
        public APIResponseModel<object> DeleteProductDetails(int productId)
        {
            var response = new APIResponseModel<object>();
            try
            {
                response = prdRepository.DeleteProductDetails(productId);
            }
            catch (Exception ex)
            {
                logger.LogInformation("DeleteProductDetails(), Error : {'" + ex.Message + "'} at {'" + DateTime.Now + "'}");
            }
            return response;
        }

        public List<ProductResponse> GetAllProducts()
        {
            var response = new List<ProductResponse>();
            try
            {
                response = prdRepository.GetAllProducts();
            }
            catch (Exception ex)
            {
                logger.LogInformation("GetAllProducts(), Error : {'" + ex.Message + "'} at {'" + DateTime.Now + "'}");
            }
            return response;
        }

        public ProductResponse GetProductById(int productId)
        {
            var response = new ProductResponse();
            try
            {
                response = prdRepository.GetProductById(productId);
            }
            catch (Exception ex)
            {
                logger.LogInformation("GetProductById(), Error : {'" + ex.Message + "'} at {'" + DateTime.Now + "'}");
            }
            return response;
        }

        public APIResponseModel<object> SaveProductDetails(ProductRequest productRequest)
        {
            var response = new APIResponseModel<object>();
            try
            {
                response = prdRepository.SaveProductDetails(productRequest);
            }
            catch (Exception ex)
            {
                logger.LogInformation("SaveProductDetails(), Error : {'" + ex.Message + "'} at {'" + DateTime.Now + "'}");
            }
            return response;
        }

        public APIResponseModel<object> UpdateProductDetails(ProductRequest productRequest)
        {
            var response = new APIResponseModel<object>();
            try
            {
                response = prdRepository.UpdateProductDetails(productRequest);
            }
            catch(Exception ex)
            {
                logger.LogInformation("SaveProductDetails(), Error : {'" + ex.Message + "'} at {'" + DateTime.Now + "'}");
            }
            return response;
        }
    }
}
