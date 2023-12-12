using Advanced_CSharp.Database.Commons;
using Advanced_CSharp.Database.Constants;
using Advanced_CSharp.Database.EF;
using Advanced_CSharp.Database.Entities;
using Advanced_CSharp.DTO.Requests.Cart;
using Advanced_CSharp.DTO.Responses.Cart;
using Advanced_CSharp.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Advanced_CSharp.Service.Services
{
    public class CartService : ICartService

    {
        private readonly AdvancedCSharpDbContext _context;
        private readonly IUnitWork _unitWork;
        private readonly string _userName;
        private readonly Guid _userId;

        public CartService(AdvancedCSharpDbContext context, IUnitWork unitWork)
        {
            _context = context;
            _unitWork = unitWork;
            _userId = ConstSystem.loggedInUserId;
            _userName = ConstSystem.loggedUserName;

        }

        public async Task<CartCheckResponse> CheckAsync(CartCheckRequest request)
        {
            CartCheckResponse response = new();
            BaseResponse baseResponse = response.BaseResponse;
            baseResponse.Success = false;
            request.UserId = _userId;
            try
            {
                if (_context != null && _context.Carts != null)
                {
                    Cart? existedCart = new();
                    if (request.UserId != Guid.Empty)
                    {
                        existedCart = await _context.Carts.FindAsync(request.UserId);
                    }

                    if (existedCart != null)
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

        public async Task<CartCreateResponse> CreateAsync(CartCreateRequest request)
        {
            CartCreateResponse response = new();
            BaseResponse baseResponse = response.BaseResponse;
            baseResponse.Success = false;
            request.UserId = _userId;
            try
            {
                if (_context != null && _context.Carts != null)
                {
                    CartCheckRequest cartCheckRequest = new()
                    {
                        UserId = request.UserId
                    };
                    CartCheckResponse cartCheckResponse = await CheckAsync(cartCheckRequest);
                    if (!cartCheckResponse.BaseResponse.Success)
                    {

                        Cart newCart = new()
                        {
                            Id = Guid.NewGuid(),
                            UserId = request.UserId

                        };
                        _ = await _context.Carts.AddAsync(newCart);
                        _ = await _unitWork.CompleteAsync(_userName);

                        // create DTO to product info
                        CartResponse cartResponse = new()
                        {
                            Id = newCart.Id,
                            UserId = newCart.UserId

                        };

                        response.CartResponse = cartResponse;
                        baseResponse.Success = true;
                        baseResponse.Message = "Create User Cart succesfully";
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

        public async Task<CartGetByIdResponse> GetByIdAsync(CartGetByIdRequest request)
        {
            CartGetByIdResponse response = new();
            BaseResponse baseResponse = response.BaseResponse;
            baseResponse.Success = false;
            request.UserId = _userId;
            try
            {
                if (_context != null && _context.Carts != null)
                {

                    Cart? existedCart = await _context.Carts.Where(t => t.UserId == request.UserId).FirstOrDefaultAsync();
                    if (existedCart != null)
                    {

                        baseResponse.Success = true;
                        response.CartResponse = new()
                        {
                            Id = existedCart.Id,
                            UserId = existedCart.UserId,

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
    }
}
