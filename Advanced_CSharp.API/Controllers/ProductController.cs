﻿using Advanced_CSharp.Database.Constants;
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
        public ProductController(IProductService productService, IloggingService loggingService)
        {
            _productService = productService;

            _loggingService = loggingService;

        }

        //http://localhost:port api/product/get-all-products
        [Route("get-all-products")]
        [HttpGet]
        [Authorize($"{ConstSystem.AdminRole},{ConstSystem.CustomerRole}")]
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductGetListRequest request)
        {
            try
            {

                ProductGetListResponse response = await _productService.GetAllAsync(request);

                return response.BaseResponse.Success ? new JsonResult(response) : BadRequest(response.BaseResponse.Message);

            }
            catch (Exception ex)
            {
                _loggingService.LogError(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [Route("get-product")]
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

        [Route("admin/add-product")]
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
