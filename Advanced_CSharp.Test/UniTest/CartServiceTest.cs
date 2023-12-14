using Advanced_CSharp.Database.Constants;
using Advanced_CSharp.DTO.Requests.Cart;
using Advanced_CSharp.DTO.Responses.Cart;
using Advanced_CSharp.Service.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced_CSharp.Test.UniTest
{
    [TestClass]
    public class CartServiceTest
    {
        private readonly ICartService _cartService;
        /// <summary>
        /// CartServiceTest
        /// </summary>
        public CartServiceTest()
        {
            _cartService = DomainServiceCollectionExtensions.SetupCartService();
        }

        /// <summary>
        /// GetCartByUserIdTestAsync happy case request
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task GetCartByUserIdTestAsync()
        {
            CartGetByIdRequest request = new()
            {
                UserId = new Guid(ConstSystem.TesterUserId)
            };

            CartGetByIdResponse response = await _cartService.GetByIdAsync(request);
            Assert.IsNotNull(response.CartResponse);
            Assert.IsTrue(response.BaseResponse.Success);
        }




    }
}
