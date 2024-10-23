
using ChipShop.Enums;
using System.Collections.ObjectModel;

namespace ChipShop.Models
{
    public class CustomerOrder
    {
        public int Id { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public List<MenuItem> OrderList { get; set; } = [];
    }
}
