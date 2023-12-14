using Advanced_CSharp.DTO.Requests.Cart;
using Advanced_CSharp.DTO.Responses.Cart;

namespace Advanced_CSharp.Service.Interfaces
{
    public interface ICartService
    {
        /// <summary>
        /// CreateAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<CartCreateResponse> CreateAsync(CartCreateRequest request);
        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<CartGetByIdResponse> GetByIdAsync(CartGetByIdRequest request);
        /// <summary>
        /// CheckAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<CartCheckResponse> CheckAsync(CartCheckRequest request);






    }
}
