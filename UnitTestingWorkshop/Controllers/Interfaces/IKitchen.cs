using ChipShop.Models;
using System.Collections.ObjectModel;

namespace ChipShop.Controllers.Interfaces
{
    public interface IKitchen
    {
        public void PlaceOrder(CustomerOrder order);
        public List<string> PrintReceipt(CustomerOrder order);
        public decimal CalculateOrderCost(Collection<MenuItem> items);
    }
}
