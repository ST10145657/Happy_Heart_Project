namespace HappyHearts_Draft.Models.ViewModels.cs
{
    public class CartViewModel
    {
        public List<CartItemViewModel> Items { get; set; } = new();

        public decimal TotalPrice { get; set; }
    }
}
