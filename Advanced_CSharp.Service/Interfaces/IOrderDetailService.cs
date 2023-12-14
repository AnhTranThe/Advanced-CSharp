using Advanced_CSharp.DTO.Requests.OrderDetail;
using Advanced_CSharp.DTO.Responses.OrderDetail;

namespace Advanced_CSharp.Service.Interfaces
{
    public interface IOrderDetailService
    {
        /// <summary>
        /// AddItemAsync
        /// </summary>
        /// <returns></returns>
        Task<OrderDetailAddItemResponse> AddItemAsync();
        /// <summary>
        /// GetItemsAllAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<OrderDetailGetListResponse> GetItemsAllAsync(OrderDetailGetListRequest request);
        /// <summary>
        /// GetItemByIdAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<OrderDetailGetByIdResponse> GetItemByIdAsync(OrderDetailGetByIdRequest request);


    }
}
