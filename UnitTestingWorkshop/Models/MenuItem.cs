using ChipShop.Enums;

namespace ChipShop.Models
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Size Size { get; set; }
        public decimal BasePrice { get; set; }
    }
}
