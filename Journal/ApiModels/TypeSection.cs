namespace Journal.ApiModels
{
    public class TypeSection
    {
        public ProductTypeDto Type { get; set; } = new();
        public List<ItemDto> Items { get; set; } = new();
    }
}
