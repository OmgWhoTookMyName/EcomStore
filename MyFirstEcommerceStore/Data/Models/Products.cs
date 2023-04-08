namespace MyFirstEcommerceStore.Data.Models
{
    public class Products
    {
        public string ProductId { get; set; } = null!;

        public string ProductName { get; set; } = null!;

        public string? ProductDescription { get; set; }

        public double? Price { get; set; }

        public Brand? Brand { get; set; }

        public string? BrandName { get; set; }

        public string? URL { get; set; }

        public Category? Category { get; set; }

        public string? CategoryName { get; set;}


    }
}
