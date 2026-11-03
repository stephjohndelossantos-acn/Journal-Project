namespace Journal.ApiModels
{
    public class HomePageModel
    {
        public ItemDto? Featured { get; set; }
        public List<TypeSection> Sections { get; set; } = new();
    }
}
