using Advanced_CSharp.DTO.Requests.Product;
using Advanced_CSharp.DTO.Responses.Product;

namespace Advanced_CSharp.Service.Interfaces
{
    public interface IProductService
    {
        Task<ProductGetListResponse> GetAllAsync(ProductGetListRequest request);
        Task<ProductGetByIdResponse> GetByIdAsync(ProductGetByIdRequest request);
        Task<ProductCreateResponse> AddAsync(ProductCreateRequest request);
        Task<ProductUpdateResponse> UpdateAsync(ProductUpdateRequest request);
        Task<ProductDeleteResponse> DeleteAsync(ProductDeleteRequest request);



    }
}
