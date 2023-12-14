using Advanced_CSharp.Database.Commons;
using Advanced_CSharp.Database.Constants;
using Advanced_CSharp.Database.EF;
using Advanced_CSharp.Database.Entities;
using Advanced_CSharp.DTO.Requests.Cart;
using Advanced_CSharp.DTO.Requests.Order;
using Advanced_CSharp.DTO.Requests.OrderDetail;
using Advanced_CSharp.DTO.Requests.Product;
using Advanced_CSharp.DTO.Responses.Cart;
using Advanced_CSharp.DTO.Responses.Order;
using Advanced_CSharp.DTO.Responses.OrderDetail;
using Advanced_CSharp.DTO.Responses.Product;
using Advanced_CSharp.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Advanced_CSharp.Service.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly AdvancedCSharpDbContext _context;
        private readonly IUnitWork _unitWork;
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        private readonly Guid _userId;
        private readonly string _userName;
        public OrderDetailService(AdvancedCSharpDbContext context,
            IOrderService orderService,
            ICartService cartService,
            IProductService productService,
            IUnitWork unitWork
          )

        {
            _context = context;
            _unitWork = unitWork;
            _orderService = orderService;
            _cartService = cartService;
            _userId = ConstSystem.loggedInUserId;
            _userName = ConstSystem.loggedUserName;
            _productService = productService;
        }

        public async Task<OrderDetailAddItemResponse> AddItemAsync()
        {
            OrderDetailAddItemResponse response = new();
            BaseResponse baseResponse = response.BaseResponse;
            baseResponse.Success = false;
            using (Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (_context != null && _context.CartDetails != null && _context.OrdersDetail != null && _context.Carts != null && _context.Products != null)
                    {
                        // Assuming you have a method to retrieve the user's active cart
                        CartGetByIdRequest cartGetByIdRequest = new()
                        {
                            UserId = _userId
                        };

                        CartGetByIdResponse cartGetByIdResponse = await _cartService.GetByIdAsync(cartGetByIdRequest);

                        if (!cartGetByIdResponse.BaseResponse.Success)
                        {
                            baseResponse.Message = "Cannot find the user's active cart.";
                            return response;
                        }

                        // Retrieve the cart detail by its ID



                        List<CartDetail>? cartDetails = await _context.CartDetails
                             .Where(cd => cd.CartId == cartGetByIdResponse.CartResponse.Id).ToListAsync();

                        if (cartDetails != null && cartDetails.Count > 0)
                        {

                            OrderCreateRequest orderCreateRequest = new()
                            {
                                UserId = _userId
                                // You might need to include additional details for creating the order
                            };

                            OrderCreateResponse orderCreateResponse = await _orderService.CreateAsync(orderCreateRequest);

                            if (!orderCreateResponse.BaseResponse.Success)
                            {
                                baseResponse.Message = "Failed to create an order.";
                                return response;
                            }

                            OrderGetByIdRequest orderGetByIdRequest = new()

                            {
                                UserId = _userId,
                                OrderId = orderCreateResponse.OrderResponse.Id

                            };

                            OrderGetByIdResponse orderGetByIdResponse = await _orderService.GetByIdAsync(orderGetByIdRequest);


                            if (!orderGetByIdResponse.BaseResponse.Success)
                            {

                                baseResponse.Message = "Not found entity.";
                                return response;
                            }

                            List<OrderDetailResponse> orderDetailResponses = new();
                            decimal totalAmount = 0;

                            foreach (CartDetail cartDetail in cartDetails)
                            {
                                decimal itemPrice = 0;
                                // Retrieve the cart detail by its ID

                                Product? productInCart = await _context.Products
                                     .Where(cd => cd.Id == cartDetail.ProductId).FirstOrDefaultAsync();

                                if (productInCart != null)
                                {
                                    // Create an order detail using the cart detail information
                                    OrderDetail newOrderDetail = new()
                                    {
                                        OrderId = orderGetByIdResponse.orderResponse.Id,
                                        ProductId = cartDetail.ProductId,
                                        Quantity = cartDetail.Quantity,
                                        CurrentPrice = productInCart.Price,

                                    };

                                    _ = await _context.OrdersDetail.AddAsync(newOrderDetail);
                                    OrderDetailResponse orderDetailResponse = new()
                                    {
                                        Id = newOrderDetail.Id,
                                        OrderId = newOrderDetail.OrderId,
                                        ProductId = newOrderDetail.ProductId,
                                        Quantity = cartDetail.Quantity,
                                        CurrentPrice = productInCart.Price,
                                        ProductName = productInCart.Name,
                                        Category = productInCart.Category,
                                        Unit = productInCart.Unit,
                                        Images = productInCart.Images,

                                    };
                                    itemPrice = orderDetailResponse.CurrentPrice * orderDetailResponse.Quantity;
                                    totalAmount += itemPrice;

                                    orderDetailResponses.Add(orderDetailResponse);


                                    ProductUpdateInventoryRequest productUpdateRequest = new()
                                    {
                                        Id = newOrderDetail.ProductId,
                                        Inventory = productInCart.Inventory - cartDetail.Quantity

                                    };
                                    ProductUpdateInventoryResponse productUpdateResponse = await _productService.UpdateInventoryAsync(productUpdateRequest);

                                    if (!productUpdateResponse.BaseResponse.Success)
                                    {
                                        baseResponse.Message = "Cannot update inventory Product after add order";
                                        return response;
                                    }
                                    _ = await _unitWork.CompleteAsync(_userName);

                                    // Optionally, you may want to remove the cart detail after adding it to the order
                                    _ = _context.CartDetails.Remove(cartDetail);
                                    _ = await _unitWork.CompleteAsync(_userName);

                                }

                            }

                            OrderUpdateRequest orderUpdateRequest = new()
                            {
                                OrderId = orderGetByIdResponse.orderResponse.Id,
                                Amount = totalAmount
                            };
                            OrderUpdateResponse orderUpdateResponse = await _orderService.UpdateAsync(orderUpdateRequest);
                            if (!orderUpdateResponse.BaseResponse.Success)
                            {
                                baseResponse.Message = orderUpdateResponse.BaseResponse.Message;
                                return response;
                            }

                            response.orderDetailResponse = orderDetailResponses;
                            baseResponse.Success = true;
                            baseResponse.Message = $"Added item from cart to order successfully.";
                        }
                        else
                        {
                            baseResponse.Message = "Cart detail not found.";
                        }

                        transaction.Commit();

                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    // Handle exceptions and log details for debugging
                    baseResponse.Message = $"An error occurred while adding the item: {ex.Message}";
                }

            }




            return response;

        }


        public async Task<OrderDetailGetByIdResponse> GetItemByIdAsync(OrderDetailGetByIdRequest request)
        {
            OrderDetailGetByIdResponse response = new();
            BaseResponse baseResponse = response.baseResponse;
            baseResponse.Success = false;

            try
            {


                if (_context != null && _context.OrdersDetail != null)
                {
                    if (request.UserId == Guid.Empty)
                    {
                        request.UserId = _userId;
                    }
                    // Assuming you have a method to retrieve the user's orders
                    OrderGetByIdRequest orderGetByIdRequest = new()
                    {
                        OrderId = request.OrderId,
                        UserId = request.UserId

                    };

                    OrderGetByIdResponse orderGetByIdResponse = await _orderService.GetByIdAsync(orderGetByIdRequest);

                    if (orderGetByIdResponse.BaseResponse.Success)
                    {

                        // Retrieve all order details associated with the user's orders
                        List<OrderDetail> orderDetails = await _context.OrdersDetail
                            .Where(od => od.OrderId == request.OrderId)
                            .ToListAsync();



                        OrderResponse orderResponse = new()
                        {
                            Id = orderGetByIdResponse.orderResponse.Id,
                            UserId = orderGetByIdResponse.orderResponse.UserId,
                            Amount = orderGetByIdResponse.orderResponse.Amount,
                            Status = orderGetByIdResponse.orderResponse.Status,
                            CreatedAt = orderGetByIdResponse.orderResponse.CreatedAt,
                            CreatedBy = orderGetByIdResponse.orderResponse.CreatedBy,
                            UpdatedAt = orderGetByIdResponse.orderResponse.UpdatedAt,
                            UpdatedBy = orderGetByIdResponse.orderResponse.UpdatedBy
                        };

                        List<OrderDetailResponse> orderDetailResponses = new();



                        foreach (OrderDetail orderDetail in orderDetails)
                        {
                            ProductGetByIdRequest productGetByIdRequest = new()
                            {
                                Id = orderDetail.ProductId
                            };

                            ProductGetByIdResponse productGetByIdResponse = await _productService.GetByIdAsync(productGetByIdRequest);

                            // Create DTO to represent order detail info
                            OrderDetailResponse orderDetailResponse = new()
                            {
                                Id = orderDetail.Id,
                                OrderId = orderDetail.OrderId,
                                ProductId = orderDetail.ProductId,
                                Quantity = orderDetail.Quantity,
                                CurrentPrice = orderDetail.CurrentPrice,
                                ProductName = productGetByIdResponse.productGetByIdResponse.Name,
                                Unit = productGetByIdResponse.productGetByIdResponse.Unit,
                                Images = productGetByIdResponse.productGetByIdResponse.Images,
                                Category = productGetByIdResponse.productGetByIdResponse.Category
                            };


                            orderDetailResponses.Add(orderDetailResponse);
                        }
                        response.orderResponse = orderResponse;
                        response.orderDetailResponses = orderDetailResponses;
                        baseResponse.Success = true;
                        baseResponse.Message = "Retrieve order details successfully.";
                    }
                    else
                    {
                        baseResponse.Message = "Cannot retrieve user's orders.";
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, log, or rethrow
                baseResponse.Message = $"An error occurred while retrieving order details: {ex.Message}";
            }

            return response;
        }

        public async Task<OrderDetailGetListResponse> GetItemsAllAsync(OrderDetailGetListRequest request)
        {
            OrderDetailGetListResponse response = new();
            BaseResponse baseResponse = response.baseResponse;
            baseResponse.Success = false;

            try
            {
                if (_context != null && _context.OrdersDetail != null && _context.Orders != null)
                {
                    // Assuming you have a method to retrieve the user's orders
                    if (request.UserId == Guid.Empty)
                    {
                        request.UserId = _userId;
                    }
                    OrderGetListRequest orderGetListRequest = new()
                    {

                        UserId = request.UserId
                    };
                    OrderGetListResponse orderGetListResponse = await _orderService.GetAllAsync(orderGetListRequest);

                    if (orderGetListResponse.BaseResponse.Success)
                    {
                        IQueryable<OrderDetail> query = (from orderDetail in _context.OrdersDetail
                                                         join order in _context.Orders on orderDetail.OrderId equals order.Id
                                                         where orderGetListResponse.orderResponses.Select(o => o.Id).Contains(order.Id)
                                                         select orderDetail).AsQueryable();

                        int totalItems = await query.CountAsync();
                        int totalPages = (int)Math.Ceiling((double)totalItems / request.PageSize);
                        int itemsToSkip = (request.PageIndex - 1) * request.PageSize;

                        // Retrieve all order details associated with the user's orders
                        List<OrderDetail> orderDetails = await query.Skip(itemsToSkip).Take(request.PageSize).ToListAsync();
                        List<OrderDetailResponse> orderDetailResponses = new();
                        decimal TotalAmount = 0;
                        foreach (OrderDetail orderDetail in orderDetails)
                        {
                            decimal itemPrice = 0;
                            ProductGetByIdRequest productGetByIdRequest = new()
                            {
                                Id = orderDetail.ProductId
                            };

                            ProductGetByIdResponse productGetByIdResponse = await _productService.GetByIdAsync(productGetByIdRequest);

                            // Create DTO to represent cart detail info
                            OrderDetailResponse orderDetailResponse = new()
                            {
                                Id = orderDetail.Id,
                                OrderId = orderDetail.OrderId,
                                ProductId = orderDetail.ProductId,
                                Quantity = orderDetail.Quantity,
                                CurrentPrice = orderDetail.CurrentPrice,
                                ProductName = productGetByIdResponse.productGetByIdResponse.Name,
                                Unit = productGetByIdResponse.productGetByIdResponse.Unit,
                                Images = productGetByIdResponse.productGetByIdResponse.Images,
                                Category = productGetByIdResponse.productGetByIdResponse.Category
                            };
                            itemPrice = orderDetailResponse.CurrentPrice * orderDetailResponse.Quantity;
                            TotalAmount += itemPrice;
                            orderDetailResponses.Add(orderDetailResponse);
                        }
                        response.TotalAmount = TotalAmount;
                        response.PageIndex = request.PageIndex;
                        response.PageSize = request.PageSize;
                        response.TotalPage = totalPages;
                        response.TotalItems = totalItems;
                        response.orderDetailResponses = orderDetailResponses;
                        baseResponse.Success = true;
                        baseResponse.Message = "Retrieve order details successfully.";
                    }
                    else
                    {
                        baseResponse.Message = "Cannot retrieve user's orders.";
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, log, or rethrow
                baseResponse.Message = $"An error occurred while retrieving order details: {ex.Message}";
            }

            return response;
        }
    }
}
