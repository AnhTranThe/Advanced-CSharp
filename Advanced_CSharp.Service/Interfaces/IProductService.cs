using Advanced_CSharp.DTO.Requests.Product;
using Advanced_CSharp.DTO.Responses.Product;

namespace Advanced_CSharp.Service.Interfaces
{
    public interface IProductService
    {
        /// <summary>
        /// GetAllAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ProductGetListResponse> GetAllAsync(ProductGetListRequest request);
        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ProductGetByIdResponse> GetByIdAsync(ProductGetByIdRequest request);
        /// <summary>
        /// AddAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ProductCreateResponse> AddAsync(ProductCreateRequest request);
        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ProductUpdateResponse> UpdateAsync(ProductUpdateRequest request);
        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ProductDeleteResponse> DeleteAsync(ProductDeleteRequest request);
        /// <summary>
        /// CheckAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ProductCheckResponse> CheckAsync(ProductCheckRequest request);
        /// <summary>
        /// UpdateInventoryAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ProductUpdateInventoryResponse> UpdateInventoryAsync(ProductUpdateInventoryRequest request);




    }
}
