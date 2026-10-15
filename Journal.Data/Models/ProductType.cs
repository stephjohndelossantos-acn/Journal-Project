namespace Journal.Models
{
    public enum CatalogSection { Journal = 1, Accessory = 2 }

    public class ProductType
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";       // "Leather Wrapped"
        public string Subtitle { get; set; } = "";   // "A5/A6 Size"
        public string ImageUrl { get; set; } = "";
        public CatalogSection Section { get; set; }
        public int SortOrder { get; set; }
        public List<JournalItem> Items { get; set; } = new();
    }
}