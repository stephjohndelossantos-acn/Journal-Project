namespace JournalAPI.Dtos
{
    public class ItemQuery
    {
        public int? Type { get; set; }
        public string? Search { get; set; }
        public string Sort { get; set; } = "order";   // order | price_asc | price_desc | newest
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 12;

        public bool? Featured { get; set; }
    }
}
