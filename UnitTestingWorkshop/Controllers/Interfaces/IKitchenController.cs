using ChipShop.Models;

namespace ChipShop.Controllers.Interfaces
{
    public interface IKitchenController
    {
        public void SendOrder(CustomerOrder order);
        public List<CustomerOrder> GetInProgressOrders();
    }
}
