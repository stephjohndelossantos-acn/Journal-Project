namespace Journal.ApiModels
{
    public class ProductTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Subtitle { get; set; } = "";
        public string ImageUrl { get; set; } = "";
        public int Section { get; set; }
        public int SortOrder { get; set; }

        //added in pod
        public string ThumbnailUrl { get; set; } = "";
    }
}
