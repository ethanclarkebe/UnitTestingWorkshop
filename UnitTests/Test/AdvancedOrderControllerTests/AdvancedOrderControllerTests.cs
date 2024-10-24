using ChipShop.Controllers;
using ChipShop.Controllers.Interfaces;
using Moq;

namespace ChipShop.Test.Test.AdvancedOrderControllerTests
{
    public class AdvancedOrderControllerTests
    {
        public IOrderController _orderController;
        public Mock<IKitchenController> _kitchenControllerMock;

        public AdvancedOrderControllerTests()
        {
            _kitchenControllerMock = new Mock<IKitchenController>();
            _orderController = new AdvancedOrderController(_kitchenControllerMock.Object);
        }
    }
}
