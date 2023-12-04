using Advanced_CSharp.Database.Commons;
using Advanced_CSharp.Database.Constants;
using Advanced_CSharp.Database.Entities;
using Advanced_CSharp.DTO.Requests.Authentication;
using Advanced_CSharp.DTO.Responses.Authentication;
using Advanced_CSharp.Service.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Advanced_CSharp.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly SignInManager<AppUser> _signInManager;

        public AuthenticationService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

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

                AppUser userId = await _userManager.FindByEmailAsync(Request.Email);


                if (userId != null)
                {

                    baseResponse.Message = "An account with this email already exists.";


                }
                if (Request.Password != Request.ComparePassword)
                {

                    baseResponse.Message = "Password and Confirmation Password must match.";

                }

                // Create new Appuser instance
                AppUser appUser = new()
                {
                    FirstName = Request.FirstName,
                    LastName = Request.LastName,
                    Email = Request.Email,
                    UserName = Request.UserName

                };

                IdentityResult result = await _userManager.CreateAsync(appUser, Request.Password);

                if (result.Succeeded)
                {
                    IdentityResult addToRoleResult = await _userManager.AddToRoleAsync(appUser, ConstSystem.GuestRole);
                    if (addToRoleResult.Succeeded)
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

                    baseResponse.Message = "Register fail, Please check provide informations";

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
                AppUser existedUser = await _userManager.FindByEmailAsync(Request.Email);

                if (existedUser == null)
                {
                    baseResponse.Message = "not found account in database";

                }
                else
                {
                    // Use UserManager to verify the password
                    SignInResult result = await _signInManager.CheckPasswordSignInAsync(existedUser, Request.Password, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        // Authentication successful
                        baseResponse.Success = true;
                        baseResponse.Message = "Login successful";
                    }
                    else
                    {
                        // Authentication failed

                        baseResponse.Message = "Invalid credentials";
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
