namespace MyFirstEcommerceStore.Data.Models
{
    public class Brand
    {
        public string BrandId { get; set; } = default!;

        public string Name { get; set; } = null!;

        public string? Description { get; set; }
    }
}
