using Advanced_CSharp.DTO.Requests.AppVersion;
using Advanced_CSharp.DTO.Responses.AppVersion;

namespace Advanced_CSharp.Service.Interfaces
{
    public interface IApplicationService
    {
        /// <summary>
        /// Get application by version string
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<AppVersionGetListResponse> GetApplicationVersionList(AppVersionGetListRequest request);

    }


}
