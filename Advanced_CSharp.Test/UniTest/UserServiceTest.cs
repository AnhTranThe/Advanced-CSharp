using Advanced_CSharp.Database.Constants;
using Advanced_CSharp.DTO.Requests.User;
using Advanced_CSharp.DTO.Responses.User;
using Advanced_CSharp.Service.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced_CSharp.Test.UniTest
{
    [TestClass]
    public class UserServiceTest
    {


        private readonly IUserService _userService;
        public UserServiceTest()
        {
            _userService = DomainServiceCollectionExtensions.SetupUserService();
        }

        [TestMethod]
        public async Task GetByAdminUserIdTestAsync()
        {
            UserGetByIdRequest userGetByIdRequest = new()
            {
                Id = new Guid(ConstSystem.AdminUserId)
            };
            UserGetByIdResponse response = await _userService.GetByIdAsync(userGetByIdRequest);
            Assert.IsNotNull(response.userResponse);
            Assert.AreEqual(response.userResponse.Id, new Guid(ConstSystem.AdminUserId));
        }


        [TestMethod]
        public async Task GetByTesterUserIdTestAsync()
        {
            UserGetByIdRequest userGetByIdRequest = new()
            {
                Id = new Guid(ConstSystem.TesterUserId)
            };
            UserGetByIdResponse response = await _userService.GetByIdAsync(userGetByIdRequest);
            Assert.IsNotNull(response.userResponse);
            Assert.AreEqual(response.userResponse.Id, new Guid(ConstSystem.TesterUserId));
        }




    }
}
