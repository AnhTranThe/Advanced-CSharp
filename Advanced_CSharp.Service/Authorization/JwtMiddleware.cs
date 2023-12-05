using Advanced_CSharp.DTO.Requests.User;
using Advanced_CSharp.DTO.Responses.User;
using Advanced_CSharp.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using static Advanced_CSharp.Service.Services.JwtService;

namespace Advanced_CSharp.Service.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService, IJwtService jwtUtils)
        {
            string? token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").LastOrDefault();
            if (token != null)
            {
                Guid? userId = jwtUtils.ValidateToken(token);
                if (userId != null)
                {
                    UserGetByIdRequest userGetByIdRequest = new()
                    {
                        Id = userId,
                    };
                    // attach user to context on successful jwt validation

                    UserGetByIdResponse userGetByIdResponse = await userService.GetByIdAsync(userGetByIdRequest);
                    if (userGetByIdResponse != null)
                    {
                        context.Items["User"] = userGetByIdResponse;
                    }

                }

            }

            await _next(context);
        }

    }
}
