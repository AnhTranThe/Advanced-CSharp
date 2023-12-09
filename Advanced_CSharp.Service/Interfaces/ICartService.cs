using Advanced_CSharp.DTO.Requests.Cart;
using Advanced_CSharp.DTO.Responses.Cart;

namespace Advanced_CSharp.Service.Interfaces
{
    public interface ICartService
    {


        Task<CartCreateResponse> CreateAsync(CartCreateRequest request);


    }
}
