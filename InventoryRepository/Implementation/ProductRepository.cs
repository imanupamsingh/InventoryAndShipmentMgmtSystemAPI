using InventoryDAL;
using InventoryDTO;
using InventoryRepository.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using InventoryUtility;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using Azure;

namespace InventoryRepository.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private IConfiguration configuration;
        private InventoryDbContext dbContext;
        private readonly ProductLogger logger;
        public ProductRepository(IConfiguration config, InventoryDbContext dbContexts)
        {
            configuration = configuration;
            dbContext = dbContexts;
            logger = new ProductLogger();
        }
        public APIResponseModel<object> DeleteProductDetails(int productId)
        {
            logger.LogInformation("DeleteProductDetails() Repository execution process started at {'" + DateTime.Now + "'} for productId='"+ productId + "' ");
            var response = new APIResponseModel<object>
            {
                StatusCode = 404,
                Status = false,
                ResponseMessage = ConstantResources.Failed
            };
            try
            {
                if (productId > 0)
                {
                    var command = dbContext.Database.GetDbConnection().CreateCommand();
                    command.CommandText = ConstantResources.spDeleteProduct;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResources.ParamProductId, productId));

                    // output parameters
                    SqlParameter outputBitParm = new SqlParameter(ConstantResources.ParamIsSuccess, SqlDbType.Bit)
                    {

                        Direction = ParameterDirection.Output
                    };
                    SqlParameter outputErrorParm = new SqlParameter(ConstantResources.ParamIsError, SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };
                    SqlParameter outputErrorMessageParm = new SqlParameter(ConstantResources.ParamErrorMsg, SqlDbType.NVarChar, 404)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputBitParm);
                    command.Parameters.Add(outputErrorParm);
                    command.Parameters.Add(outputErrorMessageParm);
                    dbContext.Database.GetDbConnection().Open();
                    command.ExecuteScalar();
                    OutParameter parameterModel = new OutParameter
                    {
                        ErrorMessage = Convert.ToString(outputErrorMessageParm.Value),
                        IsError = outputErrorParm.Value as bool? ?? default,
                        IsSuccess = outputBitParm.Value as bool? ?? default,
                    };

                    if (parameterModel.IsSuccess)
                    {
                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = parameterModel.ErrorMessage;
                    }
                    else
                    {
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        response.Status = false;
                        response.ResponseMessage = ConstantResources.Failed;

                    }
                }
                else
                {
                    logger.LogInformation("Product Id should be greater then 0. ");
                }
            }
            catch (Exception ex)
            {
                response.ResponseMessage = ex.Message;
                logger.LogInformation("Failed to perform DeleteProductDetails(), Error :  {'" + ex.Message + "'} ");
            }
            finally
            {
                dbContext.Database.GetDbConnection().Close();
            }
            return response;

        }

        public List<ProductResponse> GetAllProducts()
        {
            logger.LogInformation("GetAllProducts() Repository execution process started at {'" + DateTime.Now + "'} ");

            List<ProductResponse> response = new List<ProductResponse>();
            try
            {
                if (dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    dbContext.Database.OpenConnection();
                var cmd = dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResources.spGetAllProducts;
                cmd.CommandType = CommandType.StoredProcedure;
                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ProductResponse productResponse = new ProductResponse();
                    productResponse.ProductId = reader[ConstantResources.ProductId] != DBNull.Value ? Convert.ToInt32(reader[ConstantResources.ProductId]) : 0;
                    productResponse.ProductName = reader[ConstantResources.ProductName] != DBNull.Value ? Convert.ToString(reader[ConstantResources.ProductName]) : string.Empty;
                    productResponse.Quantity = reader[ConstantResources.Quantity] != DBNull.Value ? Convert.ToInt32(reader[ConstantResources.Quantity]) : 0;
                    productResponse.Price = reader[ConstantResources.Price] != DBNull.Value ? Convert.ToDecimal(reader[ConstantResources.Price]) : 0;
                    productResponse.CreatedOn = reader[ConstantResources.CreatedOn] != DBNull.Value ? Convert.ToDateTime(reader[ConstantResources.CreatedOn]) : DateTime.Now;
                    productResponse.UpdatedOn = reader[ConstantResources.UpdatedOn] != DBNull.Value ? Convert.ToDateTime(reader[ConstantResources.UpdatedOn]) : DateTime.Now;
                    response.Add(productResponse);
                }
            }
            catch(Exception ex)
            { 
                logger.LogInformation("Failed to perform GetAllProducts() due to, Error :  {'" + ex.Message + "'} ");
            }
            finally
            {
                dbContext.Database.GetDbConnection().Close();
            }
            return response;
        }

        public ProductResponse GetProductById(int productId)
        {
            logger.LogInformation("GetProductById() Repository execution process started at {'" + DateTime.Now + "'} for productId='"+ productId + "' ");
            ProductResponse productResponse = new ProductResponse();
            try
            {
                if (dbContext.Database.GetDbConnection().State == ConnectionState.Closed)
                    dbContext.Database.OpenConnection();
                var cmd = dbContext.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = ConstantResources.spGetProductById;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter(ConstantResources.ParamProductId, productId));
                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    productResponse.ProductId = reader[ConstantResources.ProductId] != DBNull.Value ? Convert.ToInt32(reader[ConstantResources.ProductId]) : 0;
                    productResponse.ProductName = reader[ConstantResources.ProductName] != DBNull.Value ? Convert.ToString(reader[ConstantResources.ProductName]) : string.Empty;
                    productResponse.Quantity = reader[ConstantResources.Quantity] != DBNull.Value ? Convert.ToInt32(reader[ConstantResources.Quantity]) : 0;
                    productResponse.Price = reader[ConstantResources.Price] != DBNull.Value ? Convert.ToDecimal(reader[ConstantResources.Price]) : 0;
                    productResponse.CreatedOn = reader[ConstantResources.CreatedOn] != DBNull.Value ? Convert.ToDateTime(reader[ConstantResources.CreatedOn]) : DateTime.Now;
                    productResponse.UpdatedOn = reader[ConstantResources.UpdatedOn] != DBNull.Value ? Convert.ToDateTime(reader[ConstantResources.UpdatedOn]) : DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                logger.LogInformation("Failed to perform DeleteProductDetails(), Error :  {'" + ex.Message + "'} ");
            }
            finally
            {
                dbContext.Database.GetDbConnection().Close();
            }
            return productResponse;
        }

        public APIResponseModel<object> SaveProductDetails(ProductRequest productRequest)
        {
            logger.LogInformation("SaveProductDetails() Repository execution process started at {'" + DateTime.Now + "'} ");
            var response = new APIResponseModel<object>
            {
                StatusCode = 404,
                Status = false,
                ResponseMessage = ConstantResources.Failed
            };
            try
            {
                if (!string.IsNullOrEmpty(productRequest.ToString()))
                {
                    var command = dbContext.Database.GetDbConnection().CreateCommand();
                    command.CommandText = ConstantResources.spSaveProduct;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResources.ParamProductName, productRequest.ProductName));
                    command.Parameters.Add(new SqlParameter(ConstantResources.ParamQuantity, productRequest.Quantity));
                    command.Parameters.Add(new SqlParameter(ConstantResources.ParamPrice, productRequest.Price));

                    // output parameters
                    SqlParameter outputBitParm = new SqlParameter(ConstantResources.ParamIsSuccess, SqlDbType.Bit)
                    {

                        Direction = ParameterDirection.Output
                    };
                    SqlParameter outputErrorParm = new SqlParameter(ConstantResources.ParamIsError, SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };
                    SqlParameter outputErrorMessageParm = new SqlParameter(ConstantResources.ParamErrorMsg, SqlDbType.NVarChar, 404)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputBitParm);
                    command.Parameters.Add(outputErrorParm);
                    command.Parameters.Add(outputErrorMessageParm);
                    dbContext.Database.GetDbConnection().Open();
                    command.ExecuteScalar();
                    OutParameter parameterModel = new OutParameter
                    {
                        ErrorMessage = Convert.ToString(outputErrorMessageParm.Value),
                        IsError = outputErrorParm.Value as bool? ?? default,
                        IsSuccess = outputBitParm.Value as bool? ?? default,
                    };

                    if (parameterModel.IsSuccess)
                    {
                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = parameterModel.ErrorMessage;
                    }
                    else
                    {
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        response.Status = false;
                        response.ResponseMessage = ConstantResources.Failed;

                    }
                }
            }
            catch (Exception ex)
            {
                response.ResponseMessage = ex.Message;
                logger.LogInformation("Failed to perform SaveProductDetails() due to, Error :  {'" + ex.Message + "'} ");
            }
            finally
            {
                dbContext.Database.GetDbConnection().Close();
            }
            return response;
        }

        public APIResponseModel<object> UpdateProductDetails(ProductRequest productRequest)
        {
            logger.LogInformation("UpdateProductDetails() Repository execution process started at {'" + DateTime.Now + "'} for productId='"+ productRequest.ProductId + "'");
            var response = new APIResponseModel<object>
            {
                StatusCode = 404,
                Status = false,
                ResponseMessage = ConstantResources.Failed
            };
            try
            {
                if (!string.IsNullOrEmpty(productRequest.ToString()))
                {
                    var command = dbContext.Database.GetDbConnection().CreateCommand();
                    command.CommandText = ConstantResources.spUpdateProduct;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter(ConstantResources.ParamProductId, productRequest.ProductId));
                    command.Parameters.Add(new SqlParameter(ConstantResources.ParamProductName, productRequest.ProductName));
                    command.Parameters.Add(new SqlParameter(ConstantResources.ParamQuantity, productRequest.Quantity));
                    command.Parameters.Add(new SqlParameter(ConstantResources.ParamPrice, productRequest.Price));

                    // output parameters
                    SqlParameter outputBitParm = new SqlParameter(ConstantResources.ParamIsSuccess, SqlDbType.Bit)
                    {

                        Direction = ParameterDirection.Output
                    };
                    SqlParameter outputErrorParm = new SqlParameter(ConstantResources.ParamIsError, SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };
                    SqlParameter outputErrorMessageParm = new SqlParameter(ConstantResources.ParamErrorMsg, SqlDbType.NVarChar, 404)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(outputBitParm);
                    command.Parameters.Add(outputErrorParm);
                    command.Parameters.Add(outputErrorMessageParm);
                    dbContext.Database.GetDbConnection().Open();
                    command.ExecuteScalar();
                    OutParameter parameterModel = new OutParameter
                    {
                        ErrorMessage = Convert.ToString(outputErrorMessageParm.Value),
                        IsError = outputErrorParm.Value as bool? ?? default,
                        IsSuccess = outputBitParm.Value as bool? ?? default,
                    };

                    if (parameterModel.IsSuccess)
                    {
                        response.Status = true;
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ResponseMessage = parameterModel.ErrorMessage;
                    }
                    else
                    {
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        response.Status = false;
                        response.ResponseMessage = ConstantResources.Failed;

                    }
                }
            }
            catch (Exception ex)
            {
                response.ResponseMessage = ex.Message;
                logger.LogInformation("Failed to perform UpdateProductDetails() due to, Error :  {'" + ex.Message + "'} ");
            }
            finally
            {
                dbContext.Database.GetDbConnection().Close();
            }
            return response;
        }


    }
}
