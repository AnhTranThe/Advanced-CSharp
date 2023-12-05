using Advanced_CSharp.Database.Commons;
using Advanced_CSharp.Database.Constants;
using Advanced_CSharp.DTO.Requests.Authentication;
using Advanced_CSharp.DTO.Requests.Role;
using Advanced_CSharp.DTO.Requests.User;
using Advanced_CSharp.DTO.Requests.UserRole;
using Advanced_CSharp.DTO.Responses.Authentication;
using Advanced_CSharp.DTO.Responses.Role;
using Advanced_CSharp.DTO.Responses.User;
using Advanced_CSharp.DTO.Responses.UserRole;
using Advanced_CSharp.Service.Interfaces;


namespace Advanced_CSharp.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {


        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;
        private readonly IRoleService _roleService;
        private readonly IJwtService _jwtUtils;


        public AuthenticationService(IUserService userService, IJwtService jwtUtils, IUserRoleService userRoleService, IRoleService roleService)
        {


            _userService = userService;
            _jwtUtils = jwtUtils;
            _userRoleService = userRoleService;
            _roleService = roleService;

        }

        public async Task<AuthenticationRegisterResponse> RegisterUser(AuthenticationRegisterRequest Request)
        {

            AuthenticationRegisterResponse response = new();
            BaseResponse baseResponse = response.BaseResponse;
            baseResponse.Success = false;


            // if didnt find out then continue logic 
            try
            {

                // check if existed user account in database 
                UserSearchRequest userSearchRequest = new()
                {
                    UserName = Request.UserName,
                    Email = Request.Email

                };
                UserSearchResponse userSearchResonse = await _userService.SearchAsync(userSearchRequest);
                if (userSearchResonse != null)
                {
                    baseResponse.Message = "An account with this email already exists.";
                    return response;
                }
                else
                {
                    if (Request.Password != Request.ComparePassword)
                    {

                        baseResponse.Message = "Password and Confirmation Password must match.";
                        return response;
                    }
                    // Create new Appuser instance
                    UserCreateRequest userCreateRequest = new()
                    {
                        FirstName = Request.FirstName,
                        LastName = Request.LastName,
                        Email = Request.Email,
                        UserName = Request.UserName,
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword(Request.Password)

                    };
                    UserCreateResponse userCreateResponse = await _userService.AddAsync(userCreateRequest);

                    if (userCreateResponse == null)
                    {
                        baseResponse.Message = "Register failed. Please check provided information.";
                        return response;
                    }

                    RoleSearchRequest roleSearchRequest = new()
                    {
                        RoleName = ConstSystem.CustomerRole
                    };
                    RoleSearchResponse roleSearchResponse = await _roleService.SearchAsync(roleSearchRequest);


                    if (roleSearchResponse == null)
                    {
                        baseResponse.Message = "There are no role customer";
                        return response;

                    }

                    UserRoleGetByIdRequest userRoleGetByIdRequest = new()
                    {
                        RoleId = roleSearchResponse.roleResponse.RoleId,
                        UserId = userCreateResponse.UserResponse.Id

                    };
                    UserRoleGetByIdResponse userRoleGetByIdResponse = await _userRoleService.GetByIdAsync(userRoleGetByIdRequest);
                    if (userRoleGetByIdResponse == null)

                    {

                        UserRoleCreateRequest userRoleCreateRequest = new()
                        {
                            roleId = roleSearchResponse.roleResponse.RoleId,
                            userId = userCreateResponse.UserResponse.Id

                        };
                        UserRoleCreateResponse userRoleCreateResponse = await _userRoleService.AddUserRoleAsync(userRoleCreateRequest);

                        if (userRoleCreateResponse != null)
                        {

                            baseResponse.Success = true;
                            baseResponse.Message = "Register Succesfully";
                        }
                        else
                        {

                            baseResponse.Message = "Add User to Role fail";

                        }

                    }
                    else
                    {
                        baseResponse.Message = "There are have user role already";
                    }










                }





            }
            catch (Exception ex)
            {


                baseResponse.Message = "An error occurred during registration.";
                baseResponse.Errors.Add(ex.Message);
            }

            return response;
        }

        public async Task<AuthenticationLoginResponse> ValidateUser(AuthenticationLoginRequest Request)
        {
            AuthenticationLoginResponse response = new();
            BaseResponse baseResponse = response.BaseResponse;
            baseResponse.Success = false;

            try
            {
                // Attempt to find the user by email or username
                UserSearchRequest userSearchRequest = new()
                {
                    Email = Request.Email,
                    UserName = Request.UserName
                };

                UserSearchResponse existedUser = await _userService.SearchAsync(userSearchRequest);

                if (existedUser == null)
                {
                    baseResponse.Message = "User not found. Please check your email or username.";
                    return response;
                }
                else
                {
                    UserGetByIdRequest userGetByIdRequest = new()
                    {
                        Id = existedUser.userResponse.Id
                    };
                    UserGetByIdResponse userGetByIdResponse = await _userService.GetByIdAsync(userGetByIdRequest);

                    if (userGetByIdResponse == null)
                    {
                        baseResponse.Message = "Not found User in database";
                        return response;

                    }
                    else

                    {
                        if (!BCrypt.Net.BCrypt.Verify(Request.Password, existedUser.userResponse.PasswordHash))
                        {
                            baseResponse.Message = "Incorrect password. Please check your password.";
                            return response;
                        }
                        else
                        {

                            string newToken = await _jwtUtils.GenerateToken(existedUser.userResponse);
                            response.Token = newToken;
                            UserGenerateTokenRequest userGenerateTokenRequest = new()
                            {
                                UserId = existedUser.userResponse.Id,
                                Token = newToken
                            };
                            UserGenerateTokenResponse userGenerateTokenResponse = await _userService.GenerateTokenAsync(userGenerateTokenRequest);


                            if (userGenerateTokenResponse != null)
                            {
                                // Authentication successful
                                baseResponse.Success = true;
                                baseResponse.Message = "Login successful";
                            }


                            return response;

                        }
                    }



                }


            }
            catch (Exception ex)
            {
                // Log the exception here

                baseResponse.Message = "An error occurred during login.";
                baseResponse.Errors.Add(ex.Message);
            }

            return response;

        }

    }
}
