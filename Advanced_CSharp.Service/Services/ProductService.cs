
using Advanced_CSharp.Database.Commons;
using Advanced_CSharp.Database.Constants;
using Advanced_CSharp.Database.EF;
using Advanced_CSharp.Database.Entities;
using Advanced_CSharp.DTO.Requests.Product;
using Advanced_CSharp.DTO.Responses.Product;
using Advanced_CSharp.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Advanced_CSharp.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly AdvancedCSharpDbContext _context;
        private readonly IUnitWork _unitWork;
        private readonly string _userName;

        public ProductService(AdvancedCSharpDbContext context, IUnitWork unitWork)
        {
            _context = context;
            _unitWork = unitWork;

            _userName = string.IsNullOrEmpty(ConstSystem.loggedUserName) ? "System" : ConstSystem.loggedUserName;

        }

        public async Task<ProductGetListResponse> GetAllAsync(ProductGetListRequest request)
        {
            ProductGetListResponse response = new();
            BaseResponse baseResponse = response.BaseResponse;
            baseResponse.Success = false;
            try
            {
                if (_context.Products != null)
                {
                    IQueryable<Product> query = _context.Products.OrderByDescending(e => e.Id).AsQueryable();


                    int totalProducts = await query.CountAsync();

                    int totalPages = (int)Math.Ceiling((double)totalProducts / request.PageSize);

                    List<ProductResponse> productListResult = await query
                  .Skip(request.PageSize * (request.PageIndex - 1))
                  .Take(request.PageSize)
                  .Select(a => new ProductResponse
                  {
                      Id = a.Id,
                      Name = a.Name,
                      Price = a.Price,
                      Inventory = a.Inventory,
                      Unit = a.Unit,
                      Images = a.Images,
                      Category = a.Category,
                      CreatedAt = a.CreatedAt,
                      CreatedBy = a.CreatedBy,
                      UpdatedAt = a.UpdatedAt,
                      UpdatedBy = a.UpdatedBy,

                  }).ToListAsync();

                    if (productListResult != null && productListResult.Any())
                    {
                        baseResponse.Success = true;
                        response.productGetListResponses = productListResult;
                        response.TotalProduct = totalProducts;
                        response.TotalPage = totalPages;
                        response.PageIndex = request.PageIndex;
                        response.PageSize = request.PageSize;

                    }
                    else
                    {
                        baseResponse.Success = false;
                        baseResponse.Message = "No entities found.";
                    }

                }

            }
            catch (Exception ex)
            {
                baseResponse.Success = false;
                baseResponse.Message = $"An error occurred while fetching entities: {ex.Message}";
            }
            return response;
        }

        public async Task<ProductGetByIdResponse> GetByIdAsync(ProductGetByIdRequest request)
        {
            ProductGetByIdResponse response = new();
            BaseResponse baseResponse = response.BaseResponse;
            baseResponse.Success = false;

            try
            {
                if (_context != null && _context.Products != null)
                {

                    Product? existedProduct = await _context.Products.FindAsync(request.Id);
                    if (existedProduct != null)
                    {

                        baseResponse.Success = true;
                        response.productGetByIdResponse = new()
                        {
                            Id = existedProduct.Id,
                            Name = existedProduct.Name,
                            Price = existedProduct.Price,
                            Inventory = existedProduct.Inventory,
                            Unit = existedProduct.Unit,
                            Images = existedProduct.Images,
                            Category = existedProduct.Category,
                            CreatedAt = existedProduct.CreatedAt,
                            CreatedBy = existedProduct.CreatedBy,
                            UpdatedAt = existedProduct.UpdatedAt,
                            UpdatedBy = existedProduct.UpdatedBy
                        };

                    }
                    else
                    {

                        baseResponse.Message = "Entity not found.";


                    }
                }
            }

            catch (Exception ex)
            {
                // Handle exceptions, log, or rethrow

                baseResponse.Message = $"An error occurred while check existed the entity: {ex.Message}";
            }

            return response;

        }

        public async Task<ProductCreateResponse> AddAsync(ProductCreateRequest request)
        {
            ProductCreateResponse response = new();
            BaseResponse baseResponse = response.BaseResponse;
            baseResponse.Success = false;
            try
            {
                if (_context != null && _context.Products != null)
                {

                    ProductCheckRequest productCheckRequest = new()
                    {
                        ProductName = request.Name
                    };

                    ProductCheckResponse productCheckResponse = await CheckAsync(productCheckRequest);
                    if (!productCheckResponse.baseResponse.Success)
                    {
                        Product newProduct = new()
                        {
                            Name = request.Name,
                            Price = request.Price,
                            Inventory = request.Inventory,
                            Unit = request.Unit,
                            Images = request.Images,
                            Category = request.Category,

                        };
                        _ = await _context.Products.AddAsync(newProduct);
                        _ = await _unitWork.CompleteAsync(_userName);
                        // create DTO to product info
                        ProductResponse productResponse = new()
                        {
                            Id = newProduct.Id,
                            Name = newProduct.Name,
                            Price = newProduct.Price,
                            Inventory = newProduct.Inventory,
                            Unit = newProduct.Unit,
                            Images = newProduct.Images,
                            Category = newProduct.Category,
                            CreatedAt = newProduct.CreatedAt,
                            CreatedBy = newProduct.CreatedBy,
                            UpdatedAt = newProduct.UpdatedAt,
                            UpdatedBy = newProduct.UpdatedBy
                        };
                        response.productCreateResponse = productResponse;
                        baseResponse.Success = true;
                        baseResponse.Message = "Create new Product succesfully";

                    }


                }

            }
            catch (Exception ex)
            {
                // Handle exceptions, log, or rethrow
                baseResponse.Success = false;
                baseResponse.Message = $"An error occurred while adding the entity: {ex.Message}";
            }

            return response;
        }

        public async Task<ProductUpdateResponse> UpdateAsync(ProductUpdateRequest request)
        {
            ProductUpdateResponse response = new();
            BaseResponse baseResponse = response.BaseResponse;
            baseResponse.Success = false;
            try
            {
                if (_context != null && _context.Products != null)
                {

                    // Check if the product exists
                    Product? existedProduct = await _context.Products.FindAsync(request.Id);

                    if (existedProduct != null)
                    {

                        ProductResponse oldProduct = new()
                        {
                            Id = existedProduct.Id,
                            Name = existedProduct.Name,
                            Price = existedProduct.Price,
                            Inventory = existedProduct.Inventory,
                            Unit = existedProduct.Unit,
                            Images = existedProduct.Images,
                            Category = existedProduct.Category,
                            CreatedAt = existedProduct.CreatedAt,
                            CreatedBy = existedProduct.CreatedBy,
                            UpdatedAt = existedProduct.UpdatedAt,
                            UpdatedBy = existedProduct.UpdatedBy

                        };


                        // Update product information
                        if (!string.IsNullOrEmpty(request.Name))
                        {
                            existedProduct.Name = request.Name;
                        }
                        if (request.Price != 0)
                        {
                            existedProduct.Price = request.Price;
                        }
                        if (request.Inventory != 0)
                        {
                            existedProduct.Inventory = request.Inventory;
                        }
                        if (request.Unit != "VND")
                        {
                            existedProduct.Unit = request.Unit;

                        }
                        if (!string.IsNullOrEmpty(request.Images))
                        {

                            existedProduct.Images = request.Images;
                        }

                        if (!string.IsNullOrEmpty(request.Category))

                        {
                            existedProduct.Category = request.Category;
                        }

                        // Save changes to the database


                        //_userName
                        _ = await _unitWork.CompleteAsync(_userName);

                        // Generate DTO for product information after update
                        ProductResponse updatedProduct = new()
                        {
                            Id = existedProduct.Id,
                            Name = existedProduct.Name,
                            Price = existedProduct.Price,
                            Inventory = existedProduct.Inventory,
                            Unit = existedProduct.Unit,
                            Images = existedProduct.Images,
                            Category = existedProduct.Category,
                            CreatedAt = existedProduct.CreatedAt,
                            CreatedBy = existedProduct.CreatedBy,
                            UpdatedAt = existedProduct.UpdatedAt,
                            UpdatedBy = existedProduct.UpdatedBy

                        };

                        baseResponse.Success = true;
                        response.OldProductResponse = oldProduct;
                        response.UpdatedProductResponse = updatedProduct;

                    }
                    else
                    {


                        baseResponse.Message = "Product found but null";


                    }
                }
                else
                {


                    baseResponse.Message = "Existed Product not found ";


                }

            }
            catch (Exception ex)
            {
                // Handle exceptions, log, or rethrow

                baseResponse.Message = $"An error occurred while updating the entity: {ex.Message}";
            }

            return response;

        }

        public async Task<ProductDeleteResponse> DeleteAsync(ProductDeleteRequest request)
        {
            ProductDeleteResponse response = new();
            BaseResponse baseResponse = response.BaseResponse;
            baseResponse.Success = false;

            try
            {

                if (_context != null && _context.Products != null)
                {

                    // Check if the product exists
                    Product? existedProduct = await _context.Products.FindAsync(request.Id);

                    if (existedProduct != null)
                    {
                        ProductResponse deletedProduct = new()
                        {
                            Id = existedProduct.Id,
                            Name = existedProduct.Name,
                            Price = existedProduct.Price,
                            Inventory = existedProduct.Inventory,
                            Unit = existedProduct.Unit,
                            Images = existedProduct.Images,
                            Category = existedProduct.Category,
                            CreatedAt = existedProduct.CreatedAt,
                            CreatedBy = existedProduct.CreatedBy,
                            UpdatedAt = existedProduct.UpdatedAt,
                            UpdatedBy = existedProduct.UpdatedBy

                        };

                        _ = _context.Products.Remove(existedProduct);

                        _ = await _unitWork.CompleteAsync(_userName);


                        response.productDeleteResponse = deletedProduct;

                    }
                    else
                    {

                        baseResponse.Message = "Entity not found.";
                    }
                }
            }

            catch (Exception ex)
            {

                baseResponse.Message = $"An error occurred while deleting the entity: {ex.Message}";
            }
            return response;
        }

        public async Task<ProductCheckResponse> CheckAsync(ProductCheckRequest request)
        {
            ProductCheckResponse response = new();
            BaseResponse baseResponse = response.baseResponse;
            baseResponse.Success = false;

            try
            {
                if (_context != null && _context.Products != null)
                {
                    Product? existedProduct = new();
                    if (request.ProductId != Guid.Empty)
                    {
                        existedProduct = await _context.Products.FindAsync(request.ProductId);
                    }
                    else if (!string.IsNullOrEmpty(request.ProductName))
                    {
                        existedProduct = await _context.Products.Where(t => request.ProductName.Contains(t.Name)).FirstOrDefaultAsync();
                    }

                    if (existedProduct != null)
                    {

                        baseResponse.Success = true;
                        baseResponse.Message = "Entity found";


                    }
                    else
                    {

                        baseResponse.Message = "Entity not found.";

                    }
                }
            }

            catch (Exception ex)
            {
                // Handle exceptions, log, or rethrow

                baseResponse.Message = $"An error occurred while check existed the entity: {ex.Message}";
            }

            return response;
        }

        public async Task<ProductUpdateInventoryResponse> UpdateInventoryAsync(ProductUpdateInventoryRequest request)
        {
            ProductUpdateInventoryResponse response = new();
            BaseResponse baseResponse = response.BaseResponse;
            baseResponse.Success = false;
            try
            {
                if (_context != null && _context.Products != null)
                {

                    // Check if the product exists
                    Product? existedProduct = await _context.Products.FindAsync(request.Id);

                    if (existedProduct != null)
                    {

                        ProductResponse oldProduct = new()
                        {
                            Id = existedProduct.Id,
                            Name = existedProduct.Name,
                            Price = existedProduct.Price,
                            Inventory = existedProduct.Inventory,
                            Unit = existedProduct.Unit,
                            Images = existedProduct.Images,
                            Category = existedProduct.Category,
                            CreatedAt = existedProduct.CreatedAt,
                            CreatedBy = existedProduct.CreatedBy,
                            UpdatedAt = existedProduct.UpdatedAt,
                            UpdatedBy = existedProduct.UpdatedBy

                        };


                        if (request.Inventory != 0)
                        {
                            existedProduct.Inventory = request.Inventory;
                        }


                        // Save changes to the database


                        //_userName
                        _ = await _unitWork.CompleteAsync(_userName);

                        // Generate DTO for product information after update
                        ProductResponse updatedProduct = new()
                        {
                            Id = existedProduct.Id,
                            Name = existedProduct.Name,
                            Price = existedProduct.Price,
                            Inventory = existedProduct.Inventory,
                            Unit = existedProduct.Unit,
                            Images = existedProduct.Images,
                            Category = existedProduct.Category,
                            CreatedAt = existedProduct.CreatedAt,
                            CreatedBy = existedProduct.CreatedBy,
                            UpdatedAt = existedProduct.UpdatedAt,
                            UpdatedBy = existedProduct.UpdatedBy

                        };

                        baseResponse.Success = true;
                        response.OldProductResponse = oldProduct;
                        response.UpdatedProductResponse = updatedProduct;

                    }
                    else
                    {


                        baseResponse.Message = "Product found but null";


                    }
                }
                else
                {


                    baseResponse.Message = "Existed Product not found ";


                }

            }
            catch (Exception ex)
            {
                // Handle exceptions, log, or rethrow

                baseResponse.Message = $"An error occurred while updating the entity: {ex.Message}";
            }

            return response;
        }
    }
}
