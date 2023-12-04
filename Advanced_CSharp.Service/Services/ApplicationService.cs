using Advanced_CSharp.Database.EF;
using Advanced_CSharp.Database.Entities;
using Advanced_CSharp.DTO.Requests.AppVersion;
using Advanced_CSharp.DTO.Responses.AppVersion;
using Advanced_CSharp.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Advanced_CSharp.Service.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly AdvancedCSharpDbContext _context;

        public ApplicationService(AdvancedCSharpDbContext context)
        {
            _context = context;

        }
        public async Task<AppVersionGetListResponse> GetApplicationVersionList(AppVersionGetListRequest request)
        {

            AppVersionGetListResponse appVersionGetListResponse = new()
            {
                PageSize = request.PageSize,
                PageIndex = request.PageIndex
            };

            if (_context.AppVersions != null)
            {
                IQueryable<AppVersion> query = _context.AppVersions
                .Where(a => a.Version.Contains(request.Version))
                .OrderBy(a => a.Version)
                .AsQueryable(); // not excute

                // Debug linq
                string queryString = query
                    .Skip(request.PageSize * (request.PageIndex - 1))
                    .Take(request.PageSize).ToQueryString();

                Console.WriteLine(queryString);

                appVersionGetListResponse.Data = await query
                    .Skip(request.PageSize * (request.PageIndex - 1))
                    .Take(request.PageSize)
                    .Select(a => new AppVersionResponse
                    {
                        Id = a.Id,
                        Version = a.Version
                    }).ToListAsync();

                _ = await query.CountAsync();
            }

            return appVersionGetListResponse;

        }
    }
}
