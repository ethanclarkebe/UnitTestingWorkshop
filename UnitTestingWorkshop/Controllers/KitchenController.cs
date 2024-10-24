using ChipShop.Controllers.Interfaces;
using ChipShop.Models;

namespace ChipShop.Controllers
{
    public class KitchenController : IKitchenController
    {
        public List<CustomerOrder> CustomerOrders { get; set; }

        public KitchenController()
        {
            CustomerOrders = new List<CustomerOrder>();
        }

        public void SendOrder(CustomerOrder order)
        {
            throw new NotImplementedException("You need to Mock this!");
        }
    }
}
