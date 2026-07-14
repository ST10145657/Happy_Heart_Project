namespace HappyHearts_Draft.Models.ViewModels.cs
{
    public class CartItemViewModel
    {
        public long CartDetailId { get; set; }

        public long? ProductId { get; set; }

        public long? PetId { get; set; }

        public string Name { get; set; } = "";

        public string ImageUrl { get; set; } = "";

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Total =>
            Price * Quantity;
    }
}
