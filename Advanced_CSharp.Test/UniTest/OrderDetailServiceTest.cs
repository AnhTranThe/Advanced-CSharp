using Advanced_CSharp.Database.Constants;
using Advanced_CSharp.DTO.Requests.OrderDetail;
using Advanced_CSharp.DTO.Responses.OrderDetail;
using Advanced_CSharp.Service.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced_CSharp.Test.UniTest
{
    [TestClass]
    public class OrderDetailServiceTest
    {
        private readonly IOrderDetailService _orderDetailService;
        /// <summary>
        /// OrderDetailServiceTest
        /// </summary>
        public OrderDetailServiceTest()
        {
            _orderDetailService = DomainServiceCollectionExtensions.SetupOrderDetailService();
        }

        /// <summary>
        /// GetItemFromAllOrderByUserIdTestAsync happy case request
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task GetItemFromAllOrderByUserIdTestAsync()
        {
            OrderDetailGetListRequest request = new()
            {
                UserId = new Guid(ConstSystem.TesterUserId)
            };

            OrderDetailGetListResponse response = await _orderDetailService.GetItemsAllAsync(request);
            Assert.IsNotNull(response.orderDetailResponses);
            Assert.IsTrue(response.baseResponse.Success && response.orderDetailResponses.Count > 0);

        }
        /// <summary>
        /// GetItemFromPerOrderByUserIdTestAsync happy case request
        /// </summary>
        /// <returns></returns>

        [TestMethod]
        public async Task GetItemFromPerOrderByUserIdTestAsync()
        {
            OrderDetailGetByIdRequest request = new()
            {
                UserId = new Guid(ConstSystem.TesterUserId),
                OrderId = new Guid("30ED53BC-8638-499E-93E1-531EA56A8B56")
            };

            OrderDetailGetByIdResponse response = await _orderDetailService.GetItemByIdAsync(request);
            Assert.IsNotNull(response.orderResponse);
            Assert.IsNotNull(response.orderDetailResponses);
            Assert.IsTrue(response.baseResponse.Success && response.orderDetailResponses.Count > 0);
        }
    }
}
