using InventoryBAL.Interface;
using InventoryDTO;
using InventoryUtility;
using Microsoft.AspNetCore.Mvc;

namespace InventoryShipmentMgmtAPI.Controllers
{
    [Route(ConstantResources.APIRoute)]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IConfiguration configuration;
        private IProduct prds;
        private readonly ProductLogger logger;
        public ProductController(IConfiguration config, IProduct products)
        {
            configuration = config;
            prds = products;
            logger = new ProductLogger();
        }
        /// <summary>
        /// API for to create new product
        /// </summary>
        /// <param name="productRequest"></param>
        /// <returns></returns>

        [HttpPost]
        [Route(ConstantResources.AddNewProduct)]
        public IActionResult CreateNewProduct(ProductRequest productRequest)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = prds.SaveProductDetails(productRequest);
            if (result.Status && result.StatusCode == 200)
            {
                responseModel.Status = true;
                responseModel.StatusCode = 200;
                responseModel.ResponseMessage = result.ResponseMessage;
                responseModel.Data = string.Empty;
                logger.LogInformation("CreateNewProduct(), Product added successfully at {'" + DateTime.Now + "'}");
                return Ok(responseModel);
                
            }
            else
            {
                responseModel.Status = false;
                responseModel.StatusCode = 400;
                responseModel.ResponseMessage = "No Record found!";
                responseModel.Data = result;
                logger.LogInformation("CreateNewProduct(), Product failed to add at {'" + DateTime.Now + "'}");
                return NotFound(responseModel);
            }

        }

        /// <summary>
        /// API for to update product details
        /// </summary>
        /// <param name="productRequest"></param>
        /// <returns></returns>

        [HttpPut]
        [Route(ConstantResources.UpdateProduct)]
        public IActionResult UpdateProduct(ProductRequest productRequest)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = prds.UpdateProductDetails(productRequest);
            if (result.Status && result.StatusCode == 200)
            {
                responseModel.Status = true;
                responseModel.StatusCode = 200;
                responseModel.ResponseMessage = result.ResponseMessage;
                responseModel.Data = string.Empty;
                logger.LogInformation("UpdateProduct(), Product updated successfully at {'" + DateTime.Now + "'}");
                return Ok(responseModel);
            }
            else
            {
                responseModel.Status = false;
                responseModel.StatusCode = 400;
                responseModel.ResponseMessage = "No Record found!";
                responseModel.Data = result;
                logger.LogInformation("UpdateProduct(), Product failed to update at {'" + DateTime.Now + "'}");
                return NotFound(responseModel);
            }

        }
        /// <summary>
        /// Used to get Product Details By Id
        /// </summary>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(ConstantResources.GetProductById)]
        public IActionResult GetProductById(int productId)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = prds.GetProductById(productId);
            if (result != null)
            {
                responseModel.Status = true;
                responseModel.StatusCode = 200;
                responseModel.ResponseMessage = ConstantResources.Success;
                responseModel.Data = result;
                logger.LogInformation("GetProductById(), Product reterived successfully at {'" + DateTime.Now + "'}");
                return Ok(responseModel);
            }
            else
            {
                responseModel.Status = false;
                responseModel.StatusCode = 400;
                responseModel.ResponseMessage = "No Record found!";
                responseModel.Data = result;
                logger.LogInformation("UpdateProduct(), Product failed to reterived at {'" + DateTime.Now + "'}");
                return NotFound(responseModel);
            }

        }
        /// <summary>
        /// Used to get All Product Details
        /// </summary>
        /// <param name="partnerId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(ConstantResources.GetAllProducts)]
        public IActionResult GetAllProducts()
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = prds.GetAllProducts();
            if (result.Count > 0)
            {
                responseModel.Status = true;
                responseModel.StatusCode = 200;
                responseModel.ResponseMessage = ConstantResources.Success;
                responseModel.Data = result;
                logger.LogInformation("GetAllProducts(), All Product reterived successfully at {'" + DateTime.Now + "'}");
                return Ok(responseModel);
            }
            else
            {
                responseModel.Status = false;
                responseModel.StatusCode = 400;
                responseModel.ResponseMessage = "No Record found!";
                responseModel.Data = result;
                logger.LogInformation("GetAllProducts(), All Product failed to reterived at {'" + DateTime.Now + "'}");
                return NotFound(responseModel);
            }

        }

        /// <summary>
        /// API for to create new product
        /// </summary>
        /// <param name="productRequest"></param>
        /// <returns></returns>

        [HttpDelete]
        [Route(ConstantResources.DeleteProduct)]
        public IActionResult DeleteProduct(int productId)
        {
            APIResponseModel<object> responseModel = new APIResponseModel<object>();
            var result = prds.DeleteProductDetails(productId);
            if (result.Status && result.StatusCode == 200)
            {
                responseModel.Status = true;
                responseModel.StatusCode = 200;
                responseModel.ResponseMessage = result.ResponseMessage;
                responseModel.Data = string.Empty;
                logger.LogInformation("DeleteProduct(), Product deleted successfully at {'" + DateTime.Now + "'}");
                return Ok(responseModel);
            }
            else
            {
                responseModel.Status = false;
                responseModel.StatusCode = 400;
                responseModel.ResponseMessage = "No Record found!";
                responseModel.Data = result;
                logger.LogInformation("DeleteProduct(), Product failed to delete at {'" + DateTime.Now + "'}");
                return NotFound(responseModel);
            }

        }
    }
}
