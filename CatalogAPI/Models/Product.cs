namespace CatalogAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? Image { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public string? Inventory { get; set; }
    }
}
