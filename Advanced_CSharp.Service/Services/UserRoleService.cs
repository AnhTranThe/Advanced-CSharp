﻿using Advanced_CSharp.Database.Commons;
using Advanced_CSharp.Database.EF;
using Advanced_CSharp.Database.Entities;
using Advanced_CSharp.DTO.Requests.UserRole;
using Advanced_CSharp.DTO.Responses.UserRole;
using Advanced_CSharp.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Advanced_CSharp.Service.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly AdvancedCSharpDbContext _context;
        private readonly IUnitWork _unitWork;
        private readonly string _userName;
        public UserRoleService(AdvancedCSharpDbContext context, IUnitWork unitWork, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _unitWork = unitWork;
            _userName = httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "System";

        }
        public async Task<UserRoleCreateResponse> AddUserRoleAsync(UserRoleCreateRequest request)
        {
            UserRoleCreateResponse response = new();
            BaseResponse baseResponse = response.BaseResponse;
            baseResponse.Success = false;

            try
            {
                if (_context != null && _context.AppUserRoles != null)
                {
                    UserRoleGetByIdRequest userRoleGetByIdRequest = new()
                    {
                        RoleId = request.roleId,
                        UserId = request.userId
                    };

                    UserRoleGetByIdResponse userRoleGetByIdResponse = await GetByIdAsync(userRoleGetByIdRequest);
                    // Check if the role already exists for the user
                    if (userRoleGetByIdResponse != null)
                    {
                        baseResponse.Message = $"User already has the role";

                    }
                    else
                    {

                        AppUserRole newAppUserRole = new()
                        {
                            UserId = userRoleGetByIdRequest.UserId,
                            RoleId = userRoleGetByIdRequest.RoleId
                        };


                        // Add the role to the user
                        _ = await _context.AppUserRoles.AddAsync(newAppUserRole);
                        bool addResult = await _unitWork.CompleteAsync(_userName);

                        if (addResult)
                        {
                            // Role added successfully
                            baseResponse.Success = true;
                            baseResponse.Message = $"Role added to the user succesfully.";

                        }
                        else
                        {
                            baseResponse.Message = "Failed to add role. Please try again later.";

                        }

                    }

                }

            }
            catch (Exception ex)
            {
                // Log the exception here

                baseResponse.Message = "An error occurred while adding user role. Please try again later.";
                baseResponse.Errors.Add(ex.Message);
            }

            return response;

        }

        public async Task<UserRoleGetByIdResponse> GetByIdAsync(UserRoleGetByIdRequest request)
        {
            UserRoleGetByIdResponse response = new();
            BaseResponse baseResponse = response.BaseResponse;
            baseResponse.Success = false;

            try
            {
                if (_context != null && _context.AppUserRoles != null && _context.AppRoles != null)
                {
                    AppUserRole? existedAppUserRole = await (
                               from user in _context.AppUsers
                               join userRole in _context.AppUserRoles on user.Id equals userRole.UserId
                               join role in _context.AppRoles on userRole.RoleId equals role.Id
                               where user.Id == request.UserId || role.Id == request.RoleId
                               select userRole
                           ).FirstOrDefaultAsync();

                    if (existedAppUserRole != null)
                    {

                        baseResponse.Success = true;
                        response.userRoleResponse = new()
                        {
                            UserId = existedAppUserRole.UserId,
                            RoleId = existedAppUserRole.RoleId,

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