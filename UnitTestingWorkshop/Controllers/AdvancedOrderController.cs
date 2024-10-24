using ChipShop.Controllers.Interfaces;
using ChipShop.Enums;
using ChipShop.Models;

namespace ChipShop.Controllers
{
    public class AdvancedOrderController : IOrderController
    {
        public decimal SmallSizePricing { get; set; }
        public decimal LargeSizePricing { get; set; }

        public Dictionary<MenuItem, int> Fridge;

        private IKitchenController _kitchenController;

        public AdvancedOrderController(
            IKitchenController kitchenController,
            decimal smallSizePricing = 0.8m,
            decimal largeSizePricing = 1.2m
            )
        {
            _kitchenController = kitchenController;
            SmallSizePricing = smallSizePricing;
            LargeSizePricing = largeSizePricing;

            Fridge = new Dictionary<MenuItem, int>();
        }

        public decimal CalculateOrderCost(List<MenuItem> items)
        {
            decimal total = 0;

            foreach (MenuItem item in items)
                total += GetItemPriceBySize(item);

            return total;
        }

        public void PlaceOrder(CustomerOrder order)
        {
            CheckFridgeStock(order);
            _kitchenController.SendOrder(order);
            PrintReceipt(order);
        }

        public List<string> PrintReceipt(CustomerOrder order)
        {
            var receipt = new List<string>
            {
                $"Order Number {order.Id}",
            };

            var orderTotal = GroupMenuItemsByType(order.OrderList);

            foreach (var (item, amount) in orderTotal)
                receipt.Add($"{item.Name} : {item.Size} : x{amount} : €{GetItemPriceBySize(item) * amount}");

            receipt.Add($"Total Cost: {CalculateOrderCost(order.OrderList)}");
            return receipt;
        }

        private decimal GetItemPriceBySize(MenuItem item)
        {
            decimal sizeMultiplier;
            switch (item.Size)
            {
                case Size.Small:
                    sizeMultiplier = SmallSizePricing;
                    break;
                case Size.Large:
                    sizeMultiplier = LargeSizePricing;
                    break;
                case Size.Medium:
                default:
                    sizeMultiplier = 1;
                    break;
            }
            return sizeMultiplier * item.BasePrice;
        }

        private Dictionary<MenuItem, int> GroupMenuItemsByType(List<MenuItem> menuItems)
        {
            var orderTotal = new Dictionary<MenuItem, int>();

            foreach (var item in menuItems)
            {
                if (orderTotal.ContainsKey(item))
                    orderTotal[item]++;
                else
                    orderTotal[item] = 1;
            }
            return orderTotal;
        }

        private void CheckFridgeStock(CustomerOrder order)
        {
            var orderTotal = GroupMenuItemsByType(order.OrderList);

            foreach(var (item, amount) in orderTotal)
            {
                if (!Fridge.ContainsKey(item))
                    throw new ArgumentException($"We don't serve {item.Name} here!");
                
                if (Fridge[item] == 0)
                    throw new ArgumentException($"Out of stock for {item.Name}.");

                if (Fridge[item] < amount)
                    throw new ArgumentException($"We don't have enough {item.Name} for this order.");
            }
        }
    }
}

