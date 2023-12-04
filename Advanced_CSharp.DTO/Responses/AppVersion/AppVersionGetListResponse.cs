using Advanced_CSharp.Database.Commons;

namespace Advanced_CSharp.DTO.Responses.AppVersion
{
    public class AppVersionGetListResponse
    {

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public long Total { get; set; }
        public BaseResponse BaseResponse { get; set; } = new BaseResponse();

        public List<AppVersionResponse> Data { get; set; } = new List<AppVersionResponse>();



    }
}
