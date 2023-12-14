using Advanced_CSharp.Database.Constants;
using Advanced_CSharp.DTO.Requests.CartDetail;
using Advanced_CSharp.DTO.Responses.CartDetail;
using Advanced_CSharp.Service.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced_CSharp.Test.UniTest
{
    [TestClass]
    public class CartDetailServiceTest
    {
        private readonly ICartDetailService _cartDetailService;
        /// <summary>
        /// CartServiceTest
        /// </summary>
        public CartDetailServiceTest()
        {
            _cartDetailService = DomainServiceCollectionExtensions.SetupCartDetailService();
        }

        /// <summary>
        /// GetCartByUserIdTestAsync happy case request
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task GetItemsAllByUserIdAsync()
        {
            CartDetailGetItemListRequest request = new()
            {
                UserId = new Guid(ConstSystem.TesterUserId)
            };
            CartDetailGetItemListResponse response = await _cartDetailService.GetItemsAllAsync(request);
            Assert.IsNotNull(response.cartDetailResponses);
            Assert.IsTrue(response.BaseResponse.Success && response.cartDetailResponses.Count > 0);
        }




    }
}
