namespace Journal.ApiModels
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = "";
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = "";  
        public bool IsFeatured { get; set; }
        public int TypeId { get; set; }
        public string TypeName { get; set; } = "";

        public DateTime CreatedAt { get; set; }

        //added during the podman part
        public string TypeSubtitle { get; set; } = "";
    }
}
