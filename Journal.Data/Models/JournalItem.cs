// Models/JournalItem.cs
namespace Journal.Models
{
    public class JournalItem
    {
        public int Id { get; set; }
        public string Code { get; set; } = "";       // leather01, leather02...
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = "";
        public bool IsFeatured { get; set; }
        public int SortOrder { get; set; }
        public int ProductTypeId { get; set; }
        public ProductType? ProductType { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}