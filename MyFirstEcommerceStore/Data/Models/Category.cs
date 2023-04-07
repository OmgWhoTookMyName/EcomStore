using MyFirstEcommerceStore.Data.Enums;

namespace MyFirstEcommerceStore.Data.Models
{
    public class Category
    {
        public string CategoryId { get; set; } = null!;
        public string CategoryName { get; set; } = null!;

        public string CategoryDescription { get; set; } = null !;

        public string ParentCategoryId { get; set;} = null!;

        public Tier? Tier { get; set; } = null;
    }
}
