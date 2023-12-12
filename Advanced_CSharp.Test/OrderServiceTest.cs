using Advanced_CSharp.DTO.Requests.OrderDetail;
using Advanced_CSharp.DTO.Responses.OrderDetail;
using Advanced_CSharp.Service.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Advanced_CSharp.Test
{
    [TestClass]
    public class OrderServiceTest
    {


        private readonly IOrderDetailService _orderDetailService;
        public OrderServiceTest(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }



        [TestMethod]
        public async Task GetAllUserCartTestAsync()
        {
            OrderDetailGetListRequest orderDetailGetListRequest = new();
            OrderDetailGetListResponse response = await _orderDetailService.GetItemsAllAsync(orderDetailGetListRequest);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.orderDetailResponses.Count > 0);
        }


    }

}
