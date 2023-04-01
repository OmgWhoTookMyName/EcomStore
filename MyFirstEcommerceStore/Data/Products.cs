namespace MyFirstEcommerceStore.Data
{
    public class Products
    {
        public string ProductId { get; set; } = null!;

        public string ProductName { get; set; } = null!;

        public string? ProductDescription { get; set; } 
        
        public int? Price { get; set; }

        public Brand? Brand { get; set; }

        public string? BrandName { get; set; }

        public string? URL { get; set; }


    }
}
