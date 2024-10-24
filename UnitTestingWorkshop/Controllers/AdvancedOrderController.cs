using ChipShop.Controllers.Interfaces;
using ChipShop.Enums;
using ChipShop.Models;

namespace ChipShop.Controllers
{
    public class AdvancedOrderController(
        IKitchenController kitchenController
            ) : IOrderController
    {
        public decimal SmallSizeModifier { get; set; }
        public decimal LargeSizeModifier { get; set; }

        public Dictionary<int, decimal> Fridge = [];

        private readonly IKitchenController _kitchenController = kitchenController;

        public decimal CalculateOrderCost(List<MenuItem> items)
        {
            decimal total = 0;

            foreach (MenuItem item in items)
                total += item.BasePrice * GetItemModifierBySize(item);

            return total;
        }

        public void PlaceOrder(CustomerOrder order)
        {
            CheckFridge(order);
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
                receipt.Add($"{item.Name} : {item.Size} : x{amount} : €{GetItemModifierBySize(item) * amount * item.BasePrice}");

            receipt.Add($"Total Cost: {CalculateOrderCost(order.OrderList)}");
            return receipt;
        }

        private decimal GetItemModifierBySize(MenuItem item)
        {
            var modifier = item.Size switch
            {
                Size.Small => SmallSizeModifier,
                Size.Large => LargeSizeModifier,
                _ => 1,
            };
            return modifier;
        }

        private static Dictionary<MenuItem, int> GroupMenuItemsByType(List<MenuItem> menuItems)
        {
            var orderTotal = new Dictionary<MenuItem, int>();

            foreach (var item in menuItems)
            {
                if (orderTotal.TryGetValue(item, out int value))
                    orderTotal[item] = ++value;
                else
                    orderTotal[item] = 1;
            }
            return orderTotal;
        }

        public void StockFridge(MenuItem menuItem, decimal quantity)
        {
            if (Fridge.ContainsKey(menuItem.Id))
                Fridge[menuItem.Id] += quantity;
            else
                Fridge[menuItem.Id] = quantity;
        }

        private void CheckFridge(CustomerOrder order)
        {
            var orderTotal = GroupMenuItemsByType(order.OrderList);

            foreach(var (item, amount) in orderTotal)
            {
                var sizedAmount = amount * GetItemModifierBySize(item);
                if (!Fridge.TryGetValue(item.Id, out decimal value))
                    throw new ArgumentException($"We don't serve {item.Name} here!");
                
                if (value == 0)
                    throw new ArgumentException($"Out of stock for {item.Name}.");

                if (value < sizedAmount)
                    throw new ArgumentException($"We don't have enough {item.Name} for this order.");

                Fridge[item.Id] -= sizedAmount;
            }
        }
    }
}

