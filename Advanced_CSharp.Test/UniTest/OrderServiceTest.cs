using Advanced_CSharp.Database.Constants;
using Advanced_CSharp.DTO.Requests.Order;
using Advanced_CSharp.DTO.Responses.Order;
using Advanced_CSharp.Service.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced_CSharp.Test.UniTest
{
    [TestClass]
    public class OrderServiceTest
    {

        /// <summary>
        /// OrderServiceTest
        /// </summary>
        private readonly IOrderService _orderService;
        public OrderServiceTest()
        {
            _orderService = DomainServiceCollectionExtensions.SetupOrderService();
        }


        /// <summary>
        /// GetAllOrdersFromUserTestAsync happy case request
        /// </summary>
        /// <returns></returns>


        [TestMethod]
        public async Task GetAllOrdersFromUserTestAsync()
        {
            OrderGetListRequest orderGetListRequest = new()
            {
                UserId = new Guid(ConstSystem.TesterUserId)
            };
            OrderGetListResponse response = await _orderService.GetAllAsync(orderGetListRequest);
            Assert.IsNotNull(response.orderResponses);
            Assert.IsTrue(response.BaseResponse.Success && response.orderResponses.Count > 0);
        }


        /// <summary>
        /// GetOrderFromUserIdTestAsync happy case request
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task GetOrderFromUserIdTestAsync()
        {
            OrderGetByIdRequest orderGetByIdRequest = new()
            {
                UserId = new Guid(ConstSystem.TesterUserId),
                OrderId = new Guid("30ED53BC-8638-499E-93E1-531EA56A8B56")
            };
            OrderGetByIdResponse response = await _orderService.GetByIdAsync(orderGetByIdRequest);
            Assert.IsNotNull(response.orderResponse);
            Assert.IsTrue(response.BaseResponse.Success);
            Assert.AreEqual(response.orderResponse.UserId, new Guid(ConstSystem.TesterUserId));
        }


    }

}
