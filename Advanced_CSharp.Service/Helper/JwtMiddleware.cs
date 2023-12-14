using Advanced_CSharp.Database.Entities;
using Advanced_CSharp.DTO.Requests.Role;
using Advanced_CSharp.DTO.Requests.User;
using Advanced_CSharp.DTO.Requests.UserRole;
using Advanced_CSharp.DTO.Responses.Role;
using Advanced_CSharp.DTO.Responses.User;
using Advanced_CSharp.DTO.Responses.UserRole;
using Advanced_CSharp.Service.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Advanced_CSharp.Service.Helper
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;

        }

        public async Task Invoke(HttpContext context, IUserService userService, IJwtService jwtUtils, IRoleService roleService, IUserRoleService userRoleService)
        {
            string? token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").LastOrDefault();

            if (token != null)
            {
                Guid? userId = jwtUtils.ValidateJwtToken(token);
                UserGetByIdRequest userGetByIdRequest = new()
                {
                    Id = userId
                };
                UserGetByIdResponse userGetByIdResponse = await userService.GetByIdAsync(userGetByIdRequest);
                UserRoleGetByIdRequest userRoleGetByIdRequest = new()
                {
                    UserId = userGetByIdResponse.userResponse.Id
                };

                UserRoleGetByIdResponse userRoleGetByIdResponse = await userRoleService.GetByIdAsync(userRoleGetByIdRequest);
                RoleGetByIdRequest roleGetByIdRequest = new()
                {
                    Id = userRoleGetByIdResponse.userRoleResponse.RoleId
                };
                RoleGetByIdResponse roleGetByIdResponse = await roleService.GetByIdAsync(roleGetByIdRequest);

                AppUser appUser = new()
                {
                    Id = userGetByIdResponse.userResponse.Id,
                    FirstName = userGetByIdResponse.userResponse.FirstName,
                    LastName = userGetByIdResponse.userResponse.LastName,
                    FullName = userGetByIdResponse.userResponse.FullName,
                    Email = userGetByIdResponse.userResponse.Email,
                    UserName = userGetByIdResponse.userResponse.UserName,
                    PasswordHash = userGetByIdResponse.userResponse.PasswordHash

                };

                AppRole appRole = new()
                {
                    Id = roleGetByIdResponse.roleResponse.RoleId,
                    RoleName = roleGetByIdResponse.roleResponse.RoleName
                };
                AppUserRole appUserRole = new()
                {
                    RoleId = userRoleGetByIdResponse.userRoleResponse.RoleId,
                    UserId = userRoleGetByIdResponse.userRoleResponse.UserId
                };

                context.Items["AppUser"] = appUser;
                context.Items["AppRole"] = appRole;
                context.Items["AppUserRole"] = appUserRole;

            }

            await _next(context);

        }


    }
}
