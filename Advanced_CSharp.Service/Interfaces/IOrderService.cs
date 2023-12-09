using Advanced_CSharp.DTO.Requests.Order;
using Advanced_CSharp.DTO.Responses.Order;

namespace Advanced_CSharp.Service.Interfaces
{
    public interface IOrderService
    {
        Task<OrderCreateResponse> CreateAsync(OrderCreateRequest request);
        Task<OrderUpdateResponse> UpdateAsync(OrderUpdateRequest request);

    }
}
