using Advanced_CSharp.DTO.Requests.CartDetail;
using Advanced_CSharp.DTO.Responses.CartDetail;
using Advanced_CSharp.Service.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced_CSharp.Test
{
    [TestClass]
    public class CartServiceTest
    {

        private readonly ICartDetailService _cartDetailService;

        public CartServiceTest(ICartDetailService cartDetailService)
        {

            _cartDetailService = cartDetailService;
        }

        /// <summary>
        /// GetApplicationCart happy case request
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task GetAllUserCartTestAsync()
        {
            CartDetailGetItemListRequest request = new();
            CartDetailGetItemListResponse response = await _cartDetailService.GetItemsAllAsync(request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.cartDetailResponses?.Count > 0);
        }


    }
}
