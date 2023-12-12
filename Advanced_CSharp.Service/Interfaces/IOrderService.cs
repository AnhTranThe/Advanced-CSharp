using Advanced_CSharp.DTO.Requests.Order;
using Advanced_CSharp.DTO.Responses.Order;

namespace Advanced_CSharp.Service.Interfaces
{
    public interface IOrderService
    {
        Task<OrderCreateResponse> CreateAsync(OrderCreateRequest request);
        Task<OrderUpdateResponse> UpdateAsync(OrderUpdateRequest request);
        Task<OrderDeleteResponse> DeleteAsync(OrderDeleteRequest request);
        Task<OrderGetListResponse> GetAllAsync(OrderGetListRequest request);
        Task<OrderGetByIdResponse> GetByIdAsync(OrderGetByIdRequest request);
        Task<OrderCheckResponse> CheckAsync(OrderCheckRequest request);




    }
}
