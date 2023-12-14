using Advanced_CSharp.Database.Constants;
using Advanced_CSharp.DTO.Requests.Order;
using Advanced_CSharp.DTO.Requests.OrderDetail;
using Advanced_CSharp.DTO.Responses.Order;
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
        private readonly IOrderService _orderService;
        private readonly IloggingService _loggingService;
        /// <summary>
        /// OrderController
        /// </summary>
        /// <param name="orderDetailService"></param>
        /// <param name="loggingService"></param>
        /// <param name="orderService"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public OrderController(IOrderDetailService orderDetailService, IloggingService loggingService, IOrderService orderService)
        {

            _orderDetailService = orderDetailService ?? throw new ArgumentNullException(nameof(orderDetailService));
            _loggingService = loggingService;
            _orderService = orderService;
        }



        /// <summary>
        /// AdminUpdateOrder
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("admin/update-order-user-by-id")]
        [HttpPost]
        [Authorize(ConstSystem.AdminRole)]
        public async Task<IActionResult> AdminUpdateOrder([FromQuery] OrderUpdateRequest request)
        {
            try
            {
                OrderUpdateRequest orderUpdateRequest = new()

                {
                    OrderId = request.OrderId,
                    Status = request.Status,
                    Amount = request.Amount
                };
                OrderUpdateResponse response = await _orderService.UpdateAsync(orderUpdateRequest);

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


        /// <summary>
        /// AddItemFromCartToOrder
        /// </summary>
        /// <returns></returns>

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
                _loggingService.LogError(ex);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// <summary>GetItemOrderByOrderId
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

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
                _loggingService.LogError(ex);
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// GetItemAllOrders
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

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
                _loggingService.LogError(ex);
                return StatusCode(500, ex.Message);
            }
        }






    }
}
