using ChipShop.Models;

namespace ChipShop.Controllers.Interfaces
{
    public interface IOrderController
    {
        public void PlaceOrder(CustomerOrder order);
        public List<string> PrintReceipt(CustomerOrder order);
        public decimal CalculateOrderCost(List<MenuItem> items);
    }
}
