using Advanced_CSharp.Database.Commons;
using Advanced_CSharp.Database.Constants;
using Advanced_CSharp.Database.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Advanced_CSharp.Service.Helper
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {

        private readonly string _roles;
        /// <summary>
        /// AuthorizeAttribute 
        /// </summary>
        public AuthorizeAttribute(string roles)
        {
            _roles = roles;
        }
        /// <summary>
        /// OnAuthorization
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // skip authorization if action is decorated with [AllowAnonymous] attribute
            bool allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();

            if (allowAnonymous)
            {
                return;
            }
            BaseResponse baseResponse = new();
            if (context.HttpContext.Items["AppUser"] is not AppUser userHttpContext ||
            context.HttpContext.Items["AppRole"] is not AppRole roleHttpContext ||
             context.HttpContext.Items["AppUserRole"] is not AppUserRole ||
           (_roles.Any() && !_roles.Split(',').Contains(roleHttpContext.RoleName)))
            {
                ConstSystem.loggedInUserId = Guid.Empty;
                ConstSystem.loggedUserName = string.Empty;
                baseResponse.Message = "Unauthorized";
                context.Result = new JsonResult(baseResponse) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            else
            {
                // Authorized
                ConstSystem.loggedInUserId = userHttpContext.Id;
                ConstSystem.loggedUserName = userHttpContext.UserName;

            }


        }


    }
}
