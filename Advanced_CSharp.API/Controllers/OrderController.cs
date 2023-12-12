using Advanced_CSharp.Database.Constants;
using Advanced_CSharp.DTO.Requests.OrderDetail;
using Advanced_CSharp.DTO.Responses.OrderDetail;
using Advanced_CSharp.Service.Helper;
using Advanced_CSharp.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Advanced_CSharp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;

        public OrderController(IOrderDetailService orderDetailService)
        {

            _orderDetailService = orderDetailService ?? throw new ArgumentNullException(nameof(orderDetailService));
        }


        [Route("add-item-from-cart-to-order")]
        [HttpPost]
        [Authorize(ConstSystem.CustomerRole)]
        public async Task<IActionResult> AddItemFromCartToOrder()
        {
            try
            {
                OrderDetailAddItemResponse response = await _orderDetailService.AddItemAsync();

                // Check the response and return appropriate results
                return response.BaseResponse.Success
                    ? Ok(response) // Successful addition to the cart
                    : BadRequest(response.BaseResponse.Message); // Failed to add item to the cart
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }



        [Route("get-item-user-order-by-order-id")]
        [HttpGet]
        [Authorize(ConstSystem.CustomerRole)]
        public async Task<IActionResult> GetItemOrderByOrderId([FromQuery] OrderDetailGetByIdRequest request)
        {
            try
            {
                OrderDetailGetByIdResponse response = await _orderDetailService.GetItemByIdAsync(request);

                // Check the response and return appropriate results
                return response.baseResponse.Success
                    ? Ok(response) // Successful addition to the cart
                    : BadRequest(response.baseResponse.Message); // Failed to add item to the cart
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }



        [Route("get-all-item-user-order")]
        [HttpGet]
        [Authorize(ConstSystem.CustomerRole)]
        public async Task<IActionResult> GetItemAllOrders([FromQuery] OrderDetailGetListRequest request)
        {
            try
            {
                OrderDetailGetListResponse response = await _orderDetailService.GetItemsAllAsync(request);

                // Check the response and return appropriate results
                return response.baseResponse.Success
                    ? Ok(response) // Successful addition to the cart
                    : BadRequest(response.baseResponse.Message); // Failed to add item to the cart
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }






    }
}
