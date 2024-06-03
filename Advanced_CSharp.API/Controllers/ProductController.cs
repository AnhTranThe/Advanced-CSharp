using Advanced_CSharp.Database.Constants;
using Advanced_CSharp.DTO.Requests.Product;
using Advanced_CSharp.DTO.Responses.Product;
using Advanced_CSharp.Service.Helper;
using Advanced_CSharp.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Advanced_CSharp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        private readonly IloggingService _loggingService;

        private readonly IProductService _productService;
        /// <summary>
        /// ProductController
        /// </summary>
        /// <param name="productService"></param>
        /// <param name="loggingService"></param>
        public ProductController(IProductService productService, IloggingService loggingService)
        {
            _productService = productService;

            _loggingService = loggingService;

        }
        /// <summary>
        /// GetAllProducts
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [Route("get-all-products")]
        [HttpGet]

        public async Task<IActionResult> GetAllProducts([FromQuery] ProductGetListRequest request)
        {
            try
            {

                ProductGetListResponse response = await _productService.GetAllAsync(request);
                Console.WriteLine(response.TotalProduct.ToString());
                _loggingService.LogInfo(response.TotalProduct.ToString());
                return response.BaseResponse.Success ? new JsonResult(response) : BadRequest(response.BaseResponse.Message);

            }
            catch (Exception ex)
            {
                _loggingService.LogError(ex);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// GetProductById
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("get-product-by-id")]
        [HttpGet]
        [Authorize($"{ConstSystem.AdminRole},{ConstSystem.CustomerRole}")]
        public async Task<IActionResult> GetProductById([FromQuery] ProductGetByIdRequest request)
        {
            try
            {
                ProductGetByIdResponse response = await _productService.GetByIdAsync(request);
                return response.BaseResponse.Success ? new JsonResult(response) : BadRequest(response.BaseResponse.Message);

            }
            catch (Exception ex)
            {
                _loggingService.LogError(ex);
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// AdminAddProduct
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [Route("admin/add-new-product")]
        [HttpPost]
        [Authorize(ConstSystem.AdminRole)]
        public async Task<IActionResult> AdminAddProduct([FromQuery] ProductCreateRequest request)
        {
            try
            {
                ProductCreateResponse response = await _productService.AddAsync(request);
                return response.BaseResponse.Success ? new JsonResult(response) : BadRequest(response.BaseResponse.Message);

            }
            catch (Exception ex)
            {
                _loggingService.LogError(ex);
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// AdminUpdateProduct
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [Route("admin/update-product")]
        [HttpPut]
        [Authorize(ConstSystem.AdminRole)]
        public async Task<IActionResult> AdminUpdateProduct([FromQuery] ProductUpdateRequest request)
        {
            try
            {
                ProductUpdateResponse response = await _productService.UpdateAsync(request);
                return response.BaseResponse.Success ? new JsonResult(response) : BadRequest(response.BaseResponse.Message);

            }
            catch (Exception ex)
            {
                _loggingService.LogError(ex);
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// AdminDeleteProduct
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("admin/delete-product")]
        [HttpDelete]
        [Authorize(ConstSystem.AdminRole)]
        public async Task<IActionResult> AdminDeleteProduct([FromQuery] ProductDeleteRequest request)
        {
            try
            {
                ProductDeleteResponse response = await _productService.DeleteAsync(request);
                return response.BaseResponse.Success ? new JsonResult(response) : BadRequest(response.BaseResponse.Message);

            }
            catch (Exception ex)
            {
                _loggingService.LogError(ex);
                return StatusCode(500, ex.Message);
            }
        }


    }
}
