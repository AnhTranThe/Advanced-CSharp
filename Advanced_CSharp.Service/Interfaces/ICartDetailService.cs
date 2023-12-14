using Advanced_CSharp.DTO.Requests.CartDetail;
using Advanced_CSharp.DTO.Responses.CartDetail;

namespace Advanced_CSharp.Service.Interfaces
{
    public interface ICartDetailService
    {

        Task<CartDetailAddItemResponse> AddItemAsync(CartDetailAddItemRequest request);
        Task<CartDetailGetItemListResponse> GetItemsAllAsync(CartDetailGetItemListRequest request);
        Task<CartDetailUpdateItemQuantityResponse> UpdateItemQuantityAsync(CartDetailUpdateItemQuantityRequest request);
        Task<CartDetailDeleteItemResponse> DeleteItemAsync(CartDetailDeleteItemRequest request);





    }
}
