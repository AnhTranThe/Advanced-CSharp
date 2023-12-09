using Advanced_CSharp.DTO.Requests.CartDetail;
using Advanced_CSharp.DTO.Responses.CartDetail;

namespace Advanced_CSharp.Service.Interfaces
{
    public interface ICartDetailService
    {

        Task<CartDetailAddItemResponse> AddItemAsync(CartDetailAddItemRequest request);
        Task<CartDetailGetListResponse> GetItemsAllAsync(CartDetailGetListRequest request);
        Task<CartDetailGetItemByIdResponse> GetItemByIdAsync(CartDetailGetItemByIdRequest request);
        Task<CartDetailUpdateItemResponse> UpdateItemAsync(CartDetailUpdateItemRequest request);
        Task<CartDetailDeleteResponse> DeleteItemAsync(CartDetailDeleteItemRequest request);


    }
}
