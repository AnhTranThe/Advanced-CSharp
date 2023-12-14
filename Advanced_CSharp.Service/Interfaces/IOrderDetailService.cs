using Advanced_CSharp.DTO.Requests.OrderDetail;
using Advanced_CSharp.DTO.Responses.OrderDetail;

namespace Advanced_CSharp.Service.Interfaces
{
    public interface IOrderDetailService
    {
        Task<OrderDetailAddItemResponse> AddItemAsync();
        Task<OrderDetailGetListResponse> GetItemsAllAsync(OrderDetailGetListRequest request);
        Task<OrderDetailGetByIdResponse> GetItemByIdAsync(OrderDetailGetByIdRequest request);


    }
}
