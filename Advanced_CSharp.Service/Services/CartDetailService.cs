using Advanced_CSharp.Database.Commons;
using Advanced_CSharp.Database.Constants;
using Advanced_CSharp.Database.EF;
using Advanced_CSharp.Database.Entities;
using Advanced_CSharp.DTO.Requests.Cart;
using Advanced_CSharp.DTO.Requests.CartDetail;
using Advanced_CSharp.DTO.Requests.Product;
using Advanced_CSharp.DTO.Responses.Cart;
using Advanced_CSharp.DTO.Responses.CartDetail;
using Advanced_CSharp.DTO.Responses.Product;
using Advanced_CSharp.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Advanced_CSharp.Service.Services
{
    public class CartDetailService : ICartDetailService
    {
        private readonly AdvancedCSharpDbContext _context;
        private readonly IUnitWork _unitWork;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly Guid _userId;
        private readonly string _userName;
        public CartDetailService(AdvancedCSharpDbContext context, IUnitWork unitWork, IProductService productService, ICartService cartService)
        {
            _context = context;
            _unitWork = unitWork;
            _productService = productService;
            _userId = ConstSystem.loggedInUserId;
            _userName = ConstSystem.loggedUserName;
            _cartService = cartService;

        }
        public async Task<CartDetailAddItemResponse> AddItemAsync(CartDetailAddItemRequest request)
        {
            CartDetailAddItemResponse response = new();
            BaseResponse baseResponse = response.BaseResponse;
            baseResponse.Success = false;
            using (Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (_context.Carts != null && _context.CartDetails != null)
                    {


                        CartGetByIdRequest cartGetByIdRequest = new()
                        {
                            UserId = _userId
                        };
                        CartGetByIdResponse cartGetByIdResponse = await _cartService.GetByIdAsync(cartGetByIdRequest);
                        Guid CartId = cartGetByIdResponse.CartResponse.Id;
                        if (!cartGetByIdResponse.BaseResponse.Success)
                        {
                            CartCreateRequest cartCreateRequest = new()
                            {
                                UserId = _userId,
                            };
                            CartCreateResponse cartCreateResponse = await _cartService.CreateAsync(cartCreateRequest);
                            // If CartId is not provided, create a new cart


                            if (!cartCreateResponse.BaseResponse.Success)
                            {
                                baseResponse.Message = "Failed to create a new cart.";
                                return response;
                            }

                            CartId = cartCreateResponse.CartResponse.Id;


                        }

                        // Check product availability
                        ProductGetByIdRequest productGetByIdRequest = new()
                        {
                            Id = request.ProductId
                        };

                        ProductGetByIdResponse productGetByIdResponse = await _productService.GetByIdAsync(productGetByIdRequest);

                        if (productGetByIdResponse == null || productGetByIdResponse.productGetByIdResponse.Inventory <= 0)
                        {
                            baseResponse.Message = "Product not available or sold out.";
                            return response;
                        }

                        // Add cart detail
                        CartDetail newCartDetail = new()
                        {
                            Id = Guid.NewGuid(),
                            CartId = CartId,
                            ProductId = request.ProductId,
                            Quantity = request.Quantity,
                        };

                        _ = await _context.CartDetails.AddAsync(newCartDetail);
                        _ = await _unitWork.CompleteAsync(_userName);

                        // Create DTO for response
                        CartDetailResponse cartDetailResponse = new()
                        {
                            Id = newCartDetail.Id,
                            CartId = CartId,
                            ProductId = request.ProductId,
                            Quantity = request.Quantity,
                            UserId = _userId,
                            Name = productGetByIdResponse.productGetByIdResponse.Name,
                            Price = productGetByIdResponse.productGetByIdResponse.Price,
                            Unit = productGetByIdResponse.productGetByIdResponse.Unit,
                            Images = productGetByIdResponse.productGetByIdResponse.Images,
                            Category = productGetByIdResponse.productGetByIdResponse.Category
                        };

                        response.cartDetailResponse = cartDetailResponse;
                        baseResponse.Success = true;
                        baseResponse.Message = $"Product '{productGetByIdResponse.productGetByIdResponse.Name}' added to the cart successfully.";
                        transaction.Commit();
                    }

                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    // Handle exceptions and log details for debugging
                    baseResponse.Message = $"An error occurred while adding the item: {ex.Message}";
                }
            }


            return response;
        }

        public async Task<CartDetailDeleteItemResponse> DeleteItemAsync(CartDetailDeleteItemRequest request)
        {
            CartDetailDeleteItemResponse response = new();
            BaseResponse baseResponse = response.BaseResponse;
            baseResponse.Success = false;

            try
            {
                if (_context != null && _context.CartDetails != null)
                {


                    CartGetByIdRequest cartGetByIdRequest = new()
                    {
                        UserId = _userId
                    };
                    CartGetByIdResponse cartGetByIdResponse = await _cartService.GetByIdAsync(cartGetByIdRequest);
                    if (!cartGetByIdResponse.BaseResponse.Success)
                    {
                        baseResponse.Message = "Failed to delete item in cart.";
                        return response;

                    }

                    // Find the cart detail by CartId and ProductId
                    CartDetail? cartDetailToDelete = await _context.CartDetails
                        .FirstOrDefaultAsync(cd => cd.CartId == cartGetByIdResponse.CartResponse.Id && (cd.ProductId == request.ProductId || cd.Id == request.Id));

                    if (cartDetailToDelete != null)
                    {
                        // Remove the cart detail from the context
                        _ = _context.CartDetails.Remove(cartDetailToDelete);
                        _ = await _unitWork.CompleteAsync(_userName);

                        baseResponse.Success = true;
                        baseResponse.Message = $"Delete Product with Id {request.ProductId} from cart successfully";
                    }
                    else
                    {
                        baseResponse.Message = "Cart item not found.";
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, log, or rethrow
                baseResponse.Success = false;
                baseResponse.Message = $"An error occurred while deleting the cart item: {ex.Message}";
                // Log exception details for debugging and troubleshooting
            }

            return response;
        }

        public async Task<CartDetailGetItemListResponse> GetItemsAllAsync(CartDetailGetItemListRequest request)
        {
            CartDetailGetItemListResponse response = new();
            BaseResponse baseResponse = response.BaseResponse;
            baseResponse.Success = false;

            try
            {

                if (_context != null && _context.CartDetails != null)
                {

                    CartGetByIdRequest cartGetByIdRequest = new()
                    {
                        UserId = _userId
                    };
                    CartGetByIdResponse cartGetByIdResponse = await _cartService.GetByIdAsync(cartGetByIdRequest);
                    if (!cartGetByIdResponse.BaseResponse.Success)
                    {
                        baseResponse.Message = "Failed to get list items in cart.";
                        return response;
                    }




                    // Retrieve all cart details for the specified cart
                    List<CartDetail> cartDetails = await _context.CartDetails
                        .Where(cd => cd.CartId == cartGetByIdResponse.CartResponse.Id)
                        .ToListAsync();

                    if (cartDetails != null && cartDetails.Any())
                    {
                        // Create a list to store DTOs representing cart details
                        List<CartDetailResponse> cartDetailResponses = new();

                        // Convert each CartDetail to a DTO
                        foreach (CartDetail cartDetail in cartDetails)
                        {
                            // You may need to retrieve additional information related to the product
                            // You can use a ProductService or similar service for that purpose
                            ProductGetByIdRequest productGetByIdRequest = new()
                            {
                                Id = cartDetail.ProductId
                            };

                            ProductGetByIdResponse productGetByIdResponse = await _productService.GetByIdAsync(productGetByIdRequest);

                            // Create DTO to represent cart detail info
                            CartDetailResponse cartDetailResponse = new()
                            {
                                Id = cartDetail.Id,
                                CartId = cartDetail.CartId,
                                ProductId = cartDetail.ProductId,
                                Quantity = cartDetail.Quantity,
                                UserId = _userId, // Ensure _userId is properly initialized
                                Name = productGetByIdResponse.productGetByIdResponse.Name,
                                Price = productGetByIdResponse.productGetByIdResponse.Price,
                                Unit = productGetByIdResponse.productGetByIdResponse.Unit,
                                Images = productGetByIdResponse.productGetByIdResponse.Images,
                                Category = productGetByIdResponse.productGetByIdResponse.Category
                            };

                            cartDetailResponses.Add(cartDetailResponse);
                        }

                        // Set the list of DTOs in the response
                        response.cartDetailResponses = cartDetailResponses;

                        baseResponse.Success = true;
                        baseResponse.Message = "Retrieve cart details successfully";
                    }
                    else
                    {
                        baseResponse.Message = "No cart items found.";
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, log, or rethrow
                baseResponse.Success = false;
                baseResponse.Message = $"An error occurred while retrieving cart details: {ex.Message}";
                // Log exception details for debugging and troubleshooting
            }

            return response;
        }

        public async Task<CartDetailUpdateItemQuantityResponse> UpdateItemQuantityAsync(CartDetailUpdateItemQuantityRequest request)
        {
            CartDetailUpdateItemQuantityResponse response = new();
            BaseResponse baseResponse = response.BaseResponse;
            baseResponse.Success = false;

            try
            {
                if (_context != null && _context.CartDetails != null && _context.Products != null)
                {

                    CartGetByIdRequest cartGetByIdRequest = new()
                    {
                        UserId = _userId
                    };
                    CartGetByIdResponse cartGetByIdResponse = await _cartService.GetByIdAsync(cartGetByIdRequest);
                    if (!cartGetByIdResponse.BaseResponse.Success)
                    {
                        baseResponse.Message = "Failed to update item in cart.";
                        return response;
                    }




                    CartDetail? cartDetailToUpdate = await _context.CartDetails
                        .FirstOrDefaultAsync(cd => cd.CartId == cartGetByIdResponse.CartResponse.Id && cd.ProductId == request.ProductId);

                    if (cartDetailToUpdate != null)
                    {



                        // You may want to retrieve additional information related to the product
                        Product? exisitedProduct = await _context.Products
                            .FirstOrDefaultAsync(p => p.Id == request.ProductId);

                        if (exisitedProduct != null && request.Quantity <= exisitedProduct.Inventory)
                        {

                            // Create DTO to represent updated cart detail info
                            CartDetailResponse oldCartDetailResponse = new()
                            {
                                Id = cartDetailToUpdate.Id,
                                CartId = cartDetailToUpdate.CartId,
                                ProductId = cartDetailToUpdate.ProductId,
                                Quantity = cartDetailToUpdate.Quantity,
                                UserId = _userId, // Ensure _userId is properly initialized
                                Name = exisitedProduct.Name,
                                Price = exisitedProduct.Price,
                                Unit = exisitedProduct.Unit,
                                Images = exisitedProduct.Images,
                                Category = exisitedProduct.Category
                            };

                            // Set the updated DTO in the response
                            response.oldCartDetailResponse = oldCartDetailResponse;

                            // Update the quantity or other details
                            cartDetailToUpdate.Quantity = request.Quantity;

                            // Update the context and complete the unit of work
                            _ = await _unitWork.CompleteAsync(string.Empty);




                            // Create DTO to represent updated cart detail info
                            CartDetailResponse updatedCartDetailResponse = new()
                            {
                                Id = cartDetailToUpdate.Id,
                                CartId = cartDetailToUpdate.CartId,
                                ProductId = cartDetailToUpdate.ProductId,
                                Quantity = cartDetailToUpdate.Quantity,
                                UserId = _userId, // Ensure _userId is properly initialized
                                Name = exisitedProduct.Name,
                                Price = exisitedProduct.Price,
                                Unit = exisitedProduct.Unit,
                                Images = exisitedProduct.Images,
                                Category = exisitedProduct.Category
                            };

                            // Set the updated DTO in the response
                            response.updatedCartDetailResponse = updatedCartDetailResponse;

                            baseResponse.Success = true;
                            baseResponse.Message = $"Update Product with Id {request.ProductId} in cart successfully";
                        }
                        else
                        {
                            baseResponse.Message = "Invalid quantity or product not found.";
                        }
                    }
                    else
                    {
                        baseResponse.Message = "Cart item not found.";
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, log, or rethrow
                baseResponse.Success = false;
                baseResponse.Message = $"An error occurred while updating the cart item: {ex.Message}";
                // Log exception details for debugging and troubleshooting
            }

            return response;
        }
    }
}
