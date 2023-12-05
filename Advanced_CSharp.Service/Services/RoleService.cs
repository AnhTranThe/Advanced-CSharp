using Advanced_CSharp.Database.Commons;
using Advanced_CSharp.Database.EF;
using Advanced_CSharp.Database.Entities;
using Advanced_CSharp.DTO.Requests.Role;
using Advanced_CSharp.DTO.Responses.Role;
using Advanced_CSharp.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Advanced_CSharp.Service.Services
{
    public class RoleService : IRoleService
    {
        private readonly AdvancedCSharpDbContext _context;

        public RoleService(AdvancedCSharpDbContext context)
        {

            _context = context;

        }
        public async Task<RoleSearchResponse> SearchAsync(RoleSearchRequest request)
        {
            RoleSearchResponse response = new();
            BaseResponse baseResponse = response.BaseResponse;
            baseResponse.Success = false;

            try
            {
                if (_context != null && _context.AppRoles != null)
                {

                    AppRole? existedAppRole = await _context.AppRoles.SingleOrDefaultAsync(x => x.RoleName == request.RoleName);

                    if (existedAppRole != null)
                    {

                        baseResponse.Success = true;
                        response.roleResponse = new()
                        {
                            RoleId = existedAppRole.Id,
                            RoleName = existedAppRole.RoleName

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
