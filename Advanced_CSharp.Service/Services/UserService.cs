using Advanced_CSharp.Database.Commons;
using Advanced_CSharp.Database.Constants;
using Advanced_CSharp.Database.EF;
using Advanced_CSharp.Database.Entities;
using Advanced_CSharp.Database.Enums;
using Advanced_CSharp.DTO.Requests.User;
using Advanced_CSharp.DTO.Responses.User;
using Advanced_CSharp.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Advanced_CSharp.Service.Services
{
    public class UserService : IUserService
    {
        private readonly AdvancedCSharpDbContext _context;
        private readonly IUnitWork _unitWork;
        private readonly string _userName;

        /// <summary>
        /// UserService
        /// </summary>
        /// <param name="context"></param>
        /// <param name="unitWork"></param>
        public UserService(AdvancedCSharpDbContext context, IUnitWork unitWork)
        {
            _context = context;
            _unitWork = unitWork;
            _userName = string.IsNullOrEmpty(ConstSystem.loggedUserName) ? "System" : ConstSystem.loggedUserName;


        }
        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserGetByIdResponse> GetByIdAsync(UserGetByIdRequest request)
        {
            UserGetByIdResponse response = new();
            BaseResponse baseResponse = response.BaseResponse;
            baseResponse.Success = false;

            try
            {
                if (_context != null && _context.AppUsers != null)
                {

                    AppUser? existedAppUser = await _context.AppUsers.FindAsync(request.Id);
                    if (existedAppUser != null)
                    {

                        baseResponse.Success = true;
                        response.userResponse = new()
                        {
                            Id = existedAppUser.Id,
                            UserName = existedAppUser.UserName,
                            FirstName = existedAppUser.FirstName,
                            LastName = existedAppUser.LastName,
                            FullName = existedAppUser.FullName,
                            Email = existedAppUser.Email,
                            Gender = existedAppUser.Gender,
                            Dob = existedAppUser.Dob,
                            Address = existedAppUser.Address,
                            PhoneNumber = existedAppUser.PhoneNumber,
                            CreatedAt = existedAppUser.CreatedAt,
                            CreatedBy = existedAppUser.CreatedBy,
                            UpdatedAt = existedAppUser.UpdatedAt,
                            UpdatedBy = existedAppUser.UpdatedBy,
                            PasswordHash = existedAppUser.PasswordHash
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
        /// <summary>
        /// SearchAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserSearchResponse> SearchAsync(UserSearchRequest request)
        {
            UserSearchResponse response = new();
            BaseResponse baseResponse = response.BaseResponse;
            baseResponse.Success = false;

            try
            {
                if (_context != null && _context.AppUsers != null)
                {

                    AppUser? existedAppUser = await _context.AppUsers.Where(x => x.UserName == request.UserName || x.Email == request.Email).FirstOrDefaultAsync();

                    if (existedAppUser != null)
                    {

                        baseResponse.Success = true;
                        response.userResponse = new()
                        {
                            Id = existedAppUser.Id,
                            UserName = existedAppUser.UserName,
                            FirstName = existedAppUser.FirstName,
                            LastName = existedAppUser.LastName,
                            FullName = existedAppUser.FullName,
                            Email = existedAppUser.Email,
                            Gender = existedAppUser.Gender,
                            Dob = existedAppUser.Dob,
                            Address = existedAppUser.Address,
                            PhoneNumber = existedAppUser.PhoneNumber,
                            CreatedAt = existedAppUser.CreatedAt,
                            CreatedBy = existedAppUser.CreatedBy,
                            UpdatedAt = existedAppUser.UpdatedAt,
                            UpdatedBy = existedAppUser.UpdatedBy,
                            PasswordHash = existedAppUser.PasswordHash,
                            Token = existedAppUser.Token

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
        /// <summary>
        /// AddAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserCreateResponse> AddAsync(UserCreateRequest request)
        {
            UserCreateResponse response = new();
            BaseResponse baseResponse = response.BaseResponse;
            baseResponse.Success = false;
            try
            {

                if (_context != null && _context.AppUsers != null)
                {
                    AppUser newUser = new()
                    {
                        Id = Guid.NewGuid(),
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        UserName = request.UserName,
                        Email = request.Email,
                        PasswordHash = request.PasswordHash

                    };
                    _ = await _context.AppUsers.AddAsync(newUser);
                    _ = await _unitWork.CompleteAsync(_userName);

                    // create DTO to product info
                    UserResponse userResponse = new()
                    {
                        Id = newUser.Id,
                        FirstName = newUser.FirstName,
                        LastName = newUser.LastName,
                        UserName = newUser.UserName,
                        Email = newUser.Email,
                        PasswordHash = newUser.PasswordHash,
                        Gender = newUser.Gender,
                        Address = newUser.Address,
                        Dob = newUser.Dob,
                        PhoneNumber = newUser.PhoneNumber,
                        CreatedAt = newUser.CreatedAt,
                        CreatedBy = newUser.CreatedBy,
                        UpdatedAt = newUser.UpdatedAt,
                        UpdatedBy = newUser.UpdatedBy
                    };

                    response.UserResponse = userResponse;
                    baseResponse.Success = true;

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
        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserUpdateResponse> UpdateAsync(UserUpdateRequest request)
        {


            UserUpdateResponse response = new();
            BaseResponse baseResponse = response.BaseResponse;
            baseResponse.Success = false;
            try
            {
                if (_context != null && _context.AppUsers != null)
                {

                    // Check if the product exists
                    AppUser? existedUser = await _context.AppUsers.FindAsync(request.Id);

                    if (existedUser != null)
                    {

                        UserResponse oldUser = new()
                        {
                            Id = existedUser.Id,
                            FirstName = existedUser.FirstName,
                            LastName = existedUser.LastName,
                            Email = existedUser.Email,
                            Gender = existedUser.Gender,
                            Dob = existedUser.Dob,
                            Address = existedUser.Address,
                            PhoneNumber = existedUser.PhoneNumber,
                            CreatedAt = existedUser.CreatedAt,
                            CreatedBy = existedUser.CreatedBy,
                            UpdatedAt = existedUser.UpdatedAt,
                            UpdatedBy = existedUser.UpdatedBy

                        };


                        // Update product information
                        if (!string.IsNullOrEmpty(request.FirstName))
                        {
                            existedUser.FirstName = request.FirstName;
                        }
                        if (!string.IsNullOrEmpty(request.LastName))
                        {
                            existedUser.LastName = request.LastName;
                        }

                        if (!string.IsNullOrEmpty(request.Email))
                        {
                            existedUser.Email = request.Email;
                        }

                        if (request.Gender != (EGender)(-1))
                        {
                            existedUser.Gender = request.Gender;
                        }

                        if (!string.IsNullOrEmpty(request.PhoneNumber))
                        {
                            existedUser.PhoneNumber = request.PhoneNumber;
                        }
                        if (!string.IsNullOrEmpty(request.UserName))
                        {
                            existedUser.UserName = request.UserName;
                        }
                        if (!string.IsNullOrEmpty(request.Address))
                        {
                            existedUser.Address = request.Address;
                        }
                        if (request.Dob != null)
                        {
                            existedUser.Dob = request.Dob;
                        }



                        // Save changes to the database


                        //_userName
                        _ = await _unitWork.CompleteAsync(_userName);

                        // Generate DTO for product information after update
                        UserResponse updatedUser = new()
                        {
                            Id = existedUser.Id,
                            FirstName = existedUser.FirstName,
                            LastName = existedUser.LastName,
                            Email = existedUser.Email,
                            Gender = existedUser.Gender,
                            Dob = existedUser.Dob,
                            Address = existedUser.Address,
                            PhoneNumber = existedUser.PhoneNumber,
                            CreatedAt = existedUser.CreatedAt,
                            CreatedBy = existedUser.CreatedBy,
                            UpdatedAt = existedUser.UpdatedAt,
                            UpdatedBy = existedUser.UpdatedBy

                        };

                        baseResponse.Success = true;
                        response.UpdatedUserResponse = oldUser;
                        response.UpdatedUserResponse = updatedUser;

                    }
                    else
                    {


                        baseResponse.Message = "User found but null";


                    }
                }
                else
                {


                    baseResponse.Message = "Existed User not found ";


                }

            }
            catch (Exception ex)
            {
                // Handle exceptions, log, or rethrow

                baseResponse.Message = $"An error occurred while updating the entity: {ex.Message}";
            }

            return response;
        }
        /// <summary>
        /// GenerateTokenAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserGenerateTokenResponse> GenerateTokenAsync(UserGenerateTokenRequest request)
        {
            UserGenerateTokenResponse response = new();
            BaseResponse baseResponse = response.BaseResponse;
            baseResponse.Success = false;
            try
            {
                if (_context != null && _context.AppUsers != null)
                {

                    // Check if the user exists
                    AppUser? existedUser = await _context.AppUsers.FindAsync(request.UserId);

                    if (existedUser != null)
                    {

                        // Update user information
                        if (!string.IsNullOrEmpty(request.Token))
                        {
                            existedUser.Token = request.Token;
                            response.Token = existedUser.Token;
                        }

                        // Save changes to the database


                        //_userName
                        _ = await _unitWork.CompleteAsync(_userName);

                        // Generate DTO for product information after update

                        baseResponse.Success = true;


                    }
                    else
                    {


                        baseResponse.Message = "User found but null";


                    }
                }
                else
                {


                    baseResponse.Message = "Existed User not found ";


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
