using ChipShop.Controllers.Interfaces;
using ChipShop.Models;

namespace ChipShop.Test.Test.Mocks
{
    public class KitchenControllerMock : IKitchenController
    {
        public void SendOrder(CustomerOrder order)
        {
            return;
        }
    }
}
