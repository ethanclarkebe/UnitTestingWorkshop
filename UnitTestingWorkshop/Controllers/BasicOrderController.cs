
using ChipShop.Controllers.Interfaces;
using ChipShop.Enums;
using ChipShop.Models;

namespace ChipShop.Controllers
{
    public class BasicOrderController : IOrderController
    {
        // Send order to the kitchen and print customer's receipt
        public void PlaceOrder(CustomerOrder order)
        {
            SendToKitchen(order);
            this.PrintReceipt(order);
        }

        // Print every item in a list, with the total cost at the bottom
        public List<string> PrintReceipt(CustomerOrder order)
        {
            // Throw error if there are no orders
            if (order.OrderList.Count < 1)
                throw new ArgumentException(
                    "This order is empty",
                    nameof(order));

            var receipt = new List<string>
            {
                $"Order Number {order.Id}"
            };

            foreach (MenuItem item in order.OrderList)
            {
                receipt.Add(item.Name);
            }

            receipt.Add($"Total Cost: {CalculateOrderCost(order.OrderList)}");

            return receipt;
        }

        // Get the total cost of the user's order
        public decimal CalculateOrderCost(List<MenuItem> menuItems)
        {
            decimal totalCost = 0;
            foreach (MenuItem item in menuItems)
            {
                totalCost += item.BasePrice;
            }
            return totalCost;
        }

        // Send the order to the kitchen
        private static void SendToKitchen(CustomerOrder order)
        {
            order.OrderStatus = OrderStatus.InProgress;
        }
    }
}
