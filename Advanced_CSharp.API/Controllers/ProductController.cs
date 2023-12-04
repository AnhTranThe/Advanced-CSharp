using Advanced_CSharp.DTO.Requests.Product;
using Advanced_CSharp.DTO.Responses.Product;
using Advanced_CSharp.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Advanced_CSharp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;

        }

        //http://localhost:port api/product/get-all-products
        [Route("admin/get-all-products")]
        [HttpGet]
        public async Task<IActionResult> AdminGetAllProducts([FromQuery] ProductGetListRequest request)
        {
            try
            {
                ProductGetListResponse response = await _productService.GetAllAsync(request);
                return response.BaseResponse.Success ? new JsonResult(response) : BadRequest(response.BaseResponse.Message);

            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [Route("admin/get-product")]
        [HttpGet]
        public async Task<IActionResult> AdminGetProductById([FromQuery] ProductGetByIdRequest request)
        {
            try
            {
                ProductGetByIdResponse response = await _productService.GetByIdAsync(request);
                return response.BaseResponse.Success ? new JsonResult(response) : BadRequest(response.BaseResponse.Message);

            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [Route("admin/add-product")]
        [HttpGet]
        public async Task<IActionResult> AdminAddProduct([FromQuery] ProductCreateRequest request)
        {
            try
            {
                ProductCreateResponse response = await _productService.AddAsync(request);
                return response.BaseResponse.Success ? new JsonResult(response) : BadRequest(response.BaseResponse.Message);

            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [Route("admin/update-product")]
        [HttpGet]
        public async Task<IActionResult> AdminUpdateProduct([FromQuery] ProductUpdateRequest request)
        {
            try
            {
                ProductUpdateResponse response = await _productService.UpdateAsync(request);
                return response.BaseResponse.Success ? new JsonResult(response) : BadRequest(response.BaseResponse.Message);

            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [Route("admin/delete-product")]
        [HttpGet]
        public async Task<IActionResult> AdminDeleteProduct([FromQuery] ProductDeleteRequest request)
        {
            try
            {
                ProductDeleteResponse response = await _productService.DeleteAsync(request);
                return response.BaseResponse.Success ? new JsonResult(response) : BadRequest(response.BaseResponse.Message);

            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }


    }
}
