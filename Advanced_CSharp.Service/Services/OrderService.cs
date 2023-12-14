using Advanced_CSharp.Database.Commons;
using Advanced_CSharp.Database.Constants;
using Advanced_CSharp.Database.EF;
using Advanced_CSharp.Database.Entities;
using Advanced_CSharp.DTO.Requests.Order;
using Advanced_CSharp.DTO.Responses.Order;
using Advanced_CSharp.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Advanced_CSharp.Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly AdvancedCSharpDbContext _context;
        private readonly IUnitWork _unitWork;
        private readonly Guid _userId;
        private readonly string _userName;
        /// <summary>
        /// OrderService
        /// </summary>
        /// <param name="context"></param>
        /// <param name="unitWork"></param>
        public OrderService(AdvancedCSharpDbContext context, IUnitWork unitWork)
        {
            _context = context;
            _unitWork = unitWork;
            _userId = ConstSystem.loggedInUserId;
            _userName = ConstSystem.loggedUserName;

        }
        /// <summary>
        /// CheckAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<OrderCheckResponse> CheckAsync(OrderCheckRequest request)
        {
            OrderCheckResponse response = new();
            BaseResponse baseResponse = response.BaseResponse;
            baseResponse.Success = false;

            try
            {
                if (_context != null && _context.Orders != null)
                {
                    Order? existedOrder = new();


                    if (request.UserId != Guid.Empty)
                    {
                        existedOrder = await _context.Orders.Where(t => t.UserId == request.UserId).FirstOrDefaultAsync();
                    }


                    if (existedOrder != null)
                    {

                        baseResponse.Success = true;
                        baseResponse.Message = "Entity found";

                    }
                    else
                    {

                        baseResponse.Message = "Entity not found.";

                    }
                }
            }

            catch (Exception ex)
            {
                // Handle exceptions, log, or rethrow

                baseResponse.Message = $"An error occurred while check existed the entity: {ex.Message}";
            }

            return response;

        }
        /// <summary>
        /// CreateAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<OrderCreateResponse> CreateAsync(OrderCreateRequest request)
        {
            OrderCreateResponse response = new();
            BaseResponse baseResponse = response.BaseResponse;
            baseResponse.Success = false;
            try
            {
                if (_context != null && _context.Orders != null)
                {

                    Order newOrder = new()
                    {
                        Id = Guid.NewGuid(),
                        UserId = request.UserId,
                        Status = Database.Enums.EOrderStatus.Pending

                    };
                    _ = await _context.Orders.AddAsync(newOrder);
                    _ = await _unitWork.CompleteAsync(_userName);

                    // create DTO to product info
                    OrderResponse orderResponse = new()
                    {
                        Id = newOrder.Id,
                        UserId = newOrder.UserId,
                        Status = newOrder.Status,
                        CreatedAt = newOrder.CreatedAt,
                        CreatedBy = newOrder.CreatedBy,
                        UpdatedAt = newOrder.UpdatedAt,
                        UpdatedBy = newOrder.UpdatedBy,
                        Amount = newOrder.Amount


                    };

                    response.OrderResponse = orderResponse;
                    baseResponse.Success = true;
                    baseResponse.Message = "Create User Order succesfully";

                }

            }
            catch (Exception ex)
            {
                // Handle exceptions, log, or rethrow
                baseResponse.Success = false;
                baseResponse.Message = $"An error occurred while adding the entity: {ex.Message}";
            }

            return response;
        }
        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<OrderDeleteResponse> DeleteAsync(OrderDeleteRequest request)
        {
            OrderDeleteResponse response = new();
            BaseResponse baseResponse = response.BaseResponse;
            baseResponse.Success = false;

            try
            {
                if (_context != null && _context.Orders != null && _context.OrdersDetail != null)
                {
                    OrderCheckRequest orderCheck = new()
                    {
                        UserId = _userId
                    };
                    OrderCheckResponse orderCheckResponse = await CheckAsync(orderCheck);
                    if (!orderCheckResponse.BaseResponse.Success)
                    {
                        baseResponse.Message = "cannot found order";
                        return response;
                    }
                    Order? orderToDelete = await _context.Orders.FirstOrDefaultAsync(c => c.Id == request.OrderId && c.UserId == orderCheck.UserId);

                    if (orderToDelete != null)
                    {

                        orderToDelete.IsDeleted = true;
                        _ = _context.Orders.Update(orderToDelete);


                        // Update the context and complete the unit of work
                        _ = await _unitWork.CompleteAsync(_userName);

                        baseResponse.Success = true;
                        baseResponse.Message = $"Delete Order with Id {request.OrderId} successfully";
                    }
                    else
                    {
                        baseResponse.Message = "Order not found.";
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, log, or rethrow
                baseResponse.Success = false;
                baseResponse.Message = $"An error occurred while deleting the cart: {ex.Message}";
                // Log exception details for debugging and troubleshooting
            }

            return response;
        }
        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<OrderGetByIdResponse> GetByIdAsync(OrderGetByIdRequest request)
        {
            OrderGetByIdResponse response = new();
            BaseResponse baseResponse = response.BaseResponse;
            baseResponse.Success = false;

            try
            {
                if (_context != null && _context.Orders != null)
                {
                    if (request.UserId == Guid.Empty)
                    {
                        request.UserId = _userId;
                    }

                    // Retrieve the order based on the provided criteria
                    Order? order = await _context.Orders
                        .FirstOrDefaultAsync(o => o.Id == request.OrderId && o.UserId == request.UserId);

                    if (order != null)
                    {
                        // Convert the Order entity to a DTO if needed
                        OrderResponse orderResponse = new()
                        {
                            // Map properties accordingly
                            Id = order.Id,
                            UserId = order.UserId,
                            Amount = order.Amount,
                            Status = order.Status,
                            CreatedAt = order.CreatedAt,
                            CreatedBy = order.CreatedBy,
                            UpdatedAt = order.UpdatedAt,
                            UpdatedBy = order.UpdatedBy
                            // Add other properties if needed
                        };

                        response.orderResponse = orderResponse;
                        baseResponse.Success = true;
                        baseResponse.Message = "Retrieve order by Id successfully.";
                    }
                    else
                    {
                        baseResponse.Message = "Order not found.";
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, log, or rethrow
                baseResponse.Message = $"An error occurred while retrieving the order: {ex.Message}";
            }

            return response;
        }
        /// <summary>
        /// GetAllAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<OrderGetListResponse> GetAllAsync(OrderGetListRequest request)
        {
            OrderGetListResponse response = new();
            BaseResponse baseResponse = response.BaseResponse;
            baseResponse.Success = false;


            try
            {


                if (_context != null && _context.Orders != null)
                {

                    if (request.UserId == Guid.Empty)
                    {
                        request.UserId = _userId;
                    }


                    // Build a query based on the provided criteria
                    IQueryable<Order> query = _context.Orders.AsQueryable();


                    query = query.Where(o => o.UserId == request.UserId);


                    // Add more criteria as needed...

                    // Retrieve orders
                    List<Order> orders = await query.ToListAsync();

                    // Convert Order entities to DTOs if needed
                    List<OrderResponse> orderResponses = orders
                        .Select(o => new OrderResponse
                        {
                            // Map properties accordingly
                            Id = o.Id,
                            UserId = o.UserId,
                            Amount = o.Amount,
                            Status = o.Status,
                            CreatedAt = o.CreatedAt,
                            CreatedBy = o.CreatedBy,
                            UpdatedAt = o.UpdatedAt,
                            UpdatedBy = o.UpdatedBy

                            // Add other properties if needed
                        })
                        .ToList();

                    response.orderResponses = orderResponses;
                    baseResponse.Success = true;
                    baseResponse.Message = "Retrieve orders successfully.";
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, log, or rethrow
                baseResponse.Message = $"An error occurred while retrieving orders: {ex.Message}";
            }

            return response;
        }
        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<OrderUpdateResponse> UpdateAsync(OrderUpdateRequest request)
        {
            OrderUpdateResponse response = new();
            BaseResponse baseResponse = response.BaseResponse;
            baseResponse.Success = false;

            try
            {
                if (_context != null && _context.Orders != null)
                {

                    // Retrieve the order based on the provided criteria
                    Order? existingOrder = await _context.Orders
                        .FirstOrDefaultAsync(o => o.Id == request.OrderId && o.UserId == _userId);

                    if (existingOrder != null)
                    {
                        if (request.Status != Database.Enums.EOrderStatus.Pending)
                        {
                            response.oldOrderStatus = existingOrder.Status;
                            existingOrder.Status = request.Status;
                        }
                        if (request.Amount != 0)
                        {

                            response.Amount = request.Amount;
                            existingOrder.Amount = response.Amount;

                        }

                        // Update the context and complete the unit of work

                        response.UpdatedOrderStatus = request.Status;
                        _ = _context.Orders.Update(existingOrder);
                        _ = await _unitWork.CompleteAsync(_userName);

                        baseResponse.Success = true;
                        baseResponse.Message = $"Update Order with Id {request.OrderId} successfully.";
                    }
                    else
                    {
                        baseResponse.Message = "Order not found.";
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, log, or rethrow
                baseResponse.Message = $"An error occurred while updating the order: {ex.Message}";
            }

            return response;
        }
    }
}
