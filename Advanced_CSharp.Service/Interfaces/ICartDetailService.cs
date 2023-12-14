using Advanced_CSharp.DTO.Requests.CartDetail;
using Advanced_CSharp.DTO.Responses.CartDetail;

namespace Advanced_CSharp.Service.Interfaces
{
    public interface ICartDetailService
    {
        /// <summary>
        /// AddItemAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<CartDetailAddItemResponse> AddItemAsync(CartDetailAddItemRequest request);
        /// <summary>
        /// GetItemsAllAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<CartDetailGetItemListResponse> GetItemsAllAsync(CartDetailGetItemListRequest request);
        /// <summary>
        /// UpdateItemQuantityAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<CartDetailUpdateItemQuantityResponse> UpdateItemQuantityAsync(CartDetailUpdateItemQuantityRequest request);
        /// <summary>
        /// DeleteItemAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<CartDetailDeleteItemResponse> DeleteItemAsync(CartDetailDeleteItemRequest request);





    }
}
