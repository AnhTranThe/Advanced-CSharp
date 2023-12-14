using Advanced_CSharp.DTO.Requests.Order;
using Advanced_CSharp.DTO.Responses.Order;

namespace Advanced_CSharp.Service.Interfaces
{
    public interface IOrderService
    {
        /// <summary>
        /// CreateAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<OrderCreateResponse> CreateAsync(OrderCreateRequest request);
        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<OrderUpdateResponse> UpdateAsync(OrderUpdateRequest request);
        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<OrderDeleteResponse> DeleteAsync(OrderDeleteRequest request);
        /// <summary>
        /// GetAllAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<OrderGetListResponse> GetAllAsync(OrderGetListRequest request);
        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<OrderGetByIdResponse> GetByIdAsync(OrderGetByIdRequest request);
        /// <summary>
        /// CheckAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<OrderCheckResponse> CheckAsync(OrderCheckRequest request);




    }
}
