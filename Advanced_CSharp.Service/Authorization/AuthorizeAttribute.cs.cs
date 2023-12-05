using Advanced_CSharp.Database.Commons;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Advanced_CSharp.Service.Authorization
{

    public class AuthorizeAttribute : ActionFilterAttribute
    {
        public string RoleName { get; set; }
        public AuthorizeAttribute(string roleName)
        {
            RoleName = roleName;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // skip authorization if action is decorated with [AllowAnonymous] attribute
            bool allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
            {
                return;
            }
            BaseResponse baseResponse = new();

            if (!string.IsNullOrEmpty(RoleName))
            {
                if (RoleName == "Admin")
                {
                    return;
                }
                baseResponse.Message = "User is unauthorized";

                context.Result = new JsonResult(baseResponse) { StatusCode = StatusCodes.Status401Unauthorized };

            }


        }
    }
}
