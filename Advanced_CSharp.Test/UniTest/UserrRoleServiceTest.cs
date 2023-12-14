using Advanced_CSharp.Database.Constants;
using Advanced_CSharp.DTO.Requests.UserRole;
using Advanced_CSharp.DTO.Responses.UserRole;
using Advanced_CSharp.Service.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced_CSharp.Test.UniTest
{
    [TestClass]
    public class UserrRoleServiceTest
    {
        /// <summary>
        /// UserrRoleServiceTest
        /// </summary>
        private readonly IUserRoleService _userRoleService;
        public UserrRoleServiceTest()
        {
            _userRoleService = DomainServiceCollectionExtensions.SetupUserRoleService();
        }


        /// <summary>
        /// GetUserRoleByUserIdTestAsync happy case request
        /// </summary>
        /// <returns></returns>

        [TestMethod]
        public async Task GetUserRoleByUserIdTestAsync()
        {
            UserRoleGetByIdRequest userRoleGetByIdRequest = new()
            {
                UserId = new Guid(ConstSystem.AdminUserId)
            };
            UserRoleGetByIdResponse response = await _userRoleService.GetByIdAsync(userRoleGetByIdRequest);
            Assert.IsNotNull(response.userRoleResponse);
            Assert.AreEqual(response.userRoleResponse.UserId, new Guid(ConstSystem.AdminUserId));
        }


        /// <summary>
        /// GetUserRoleByRoleIdTestAsync happy case request
        /// </summary>
        /// <returns></returns>

        [TestMethod]
        public async Task GetUserRoleByRoleIdTestAsync()
        {
            UserRoleGetByIdRequest userRoleGetByIdRequest = new()
            {
                RoleId = new Guid(ConstSystem.AdminRoleId)
            };
            UserRoleGetByIdResponse response = await _userRoleService.GetByIdAsync(userRoleGetByIdRequest);
            Assert.IsNotNull(response.userRoleResponse);
            Assert.AreEqual(response.userRoleResponse.RoleId, new Guid(ConstSystem.AdminRoleId));
        }


    }

}
