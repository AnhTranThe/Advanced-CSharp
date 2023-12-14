using Advanced_CSharp.Database.Constants;
using Advanced_CSharp.DTO.Requests.Role;
using Advanced_CSharp.DTO.Responses.Role;
using Advanced_CSharp.Service.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced_CSharp.Test.UniTest
{
    [TestClass]
    public class RoleServiceTest
    {


        private readonly IRoleService _roleService;
        public RoleServiceTest()
        {
            _roleService = DomainServiceCollectionExtensions.SetupRoleService();
        }

        [TestMethod]
        public async Task GetByAdminRoleIdTestAsync()
        {
            RoleGetByIdRequest roleGetByIdRequest = new()
            {
                Id = new Guid(ConstSystem.AdminRoleId)
            };
            RoleGetByIdResponse response = await _roleService.GetByIdAsync(roleGetByIdRequest);
            Assert.IsNotNull(response.roleResponse);
            Assert.AreEqual(response.roleResponse.RoleId, new Guid(ConstSystem.AdminRoleId));
        }


        [TestMethod]
        public async Task GetByCustomerRoleIdTestAsync()
        {
            RoleGetByIdRequest roleGetByIdRequest = new()
            {
                Id = new Guid(ConstSystem.CustomerRoleId)
            };
            RoleGetByIdResponse response = await _roleService.GetByIdAsync(roleGetByIdRequest);
            Assert.IsNotNull(response.roleResponse);
            Assert.AreEqual(response.roleResponse.RoleId, new Guid(ConstSystem.CustomerRoleId));
        }


    }
}
