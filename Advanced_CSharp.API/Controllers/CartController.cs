using Advanced_CSharp.Database.Constants;
using Advanced_CSharp.DTO.Requests.CartDetail;
using Advanced_CSharp.DTO.Responses.CartDetail;
using Advanced_CSharp.Service.Helper;
using Advanced_CSharp.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Advanced_CSharp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {

        private readonly ICartDetailService _cartDetailService;
        private readonly IloggingService _loggingService;

        public CartController(ICartDetailService cartDetailService, IloggingService loggingService)
        {


            _cartDetailService = cartDetailService ?? throw new ArgumentNullException(nameof(cartDetailService));
            _loggingService = loggingService;
        }



        /// <summary>
        ///  Customer Area
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>


        [Route("add-user-cart-item")]
        [HttpPost]
        [Authorize(ConstSystem.CustomerRole)]
        public async Task<IActionResult> AddItemToCart([FromQuery] CartDetailAddItemRequest request)
        {
            try
            {
                CartDetailAddItemResponse response = await _cartDetailService.AddItemAsync(request);

                // Check the response and return appropriate results
                return response.BaseResponse.Success
                    ? Ok(response) // Successful addition to the cart
                    : BadRequest(response.BaseResponse.Message); // Failed to add item to the cart
            }
            catch (Exception ex)
            {
                _loggingService.LogError(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [Route("delete-user-cart-item")]
        [HttpPost]
        [Authorize(ConstSystem.CustomerRole)]
        public async Task<IActionResult> DeleteItemFromCart([FromQuery] CartDetailDeleteItemRequest request)
        {
            try
            {

                CartDetailDeleteItemResponse response = await _cartDetailService.DeleteItemAsync(request);

                // Check the response and return appropriate results
                return response.BaseResponse.Success
                    ? Ok(response) // Successful addition to the cart
                    : BadRequest(response.BaseResponse.Message); // Failed to add item to the cart
            }
            catch (Exception ex)
            {
                _loggingService.LogError(ex);
                return StatusCode(500, ex.Message);
            }
        }


        [Route("get-all-user-cart-item")]
        [HttpGet]
        [Authorize(ConstSystem.CustomerRole)]
        public async Task<IActionResult> GetAllItemFromCart([FromQuery] CartDetailGetItemListRequest request)
        {
            try
            {
                if (ConstSystem.loggedInUserId == Guid.Empty)
                {
                    return BadRequest("User is not authenticated.");
                }


                CartDetailGetItemListResponse response = await _cartDetailService.GetItemsAllAsync(request);

                // Check the response and return appropriate results
                return response.BaseResponse.Success
                    ? Ok(response) // Successful addition to the cart
                    : BadRequest(response.BaseResponse.Message); // Failed to add item to the cart
            }
            catch (Exception ex)
            {
                _loggingService.LogError(ex);
                return StatusCode(500, ex.Message);
            }
        }


        [Route("update-cart-item-quantity")]
        [HttpPut]
        [Authorize(ConstSystem.CustomerRole)]
        public async Task<IActionResult> UpdateItemQuantityFromCart([FromQuery] CartDetailUpdateItemQuantityRequest request)
        {
            try
            {
                if (ConstSystem.loggedInUserId == Guid.Empty)
                {
                    return BadRequest("User is not authenticated.");
                }

                CartDetailUpdateItemQuantityResponse response = await _cartDetailService.UpdateItemQuantityAsync(request);

                // Check the response and return appropriate results
                return response.BaseResponse.Success
                    ? Ok(response) // Successful addition to the cart
                    : BadRequest(response.BaseResponse.Message); // Failed to add item to the cart
            }
            catch (Exception ex)
            {
                _loggingService.LogError(ex);
                return StatusCode(500, ex.Message);
            }
        }

    }
}
