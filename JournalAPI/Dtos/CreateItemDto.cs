using System.ComponentModel.DataAnnotations;

namespace JournalAPI.Dtos
{
    public class CreateItemDto
    {
        [Required, StringLength(50)] public string Code { get; set; } = "";
        [Required, StringLength(150)] public string Title { get; set; } = "";
        [StringLength(2000)] public string Description { get; set; } = "";
        [Range(0, 10000000)] public decimal Price { get; set; }
        public string ImageUrl { get; set; } = "";
        public bool IsFeatured { get; set; }
        public int SortOrder { get; set; }
        [Range(1, int.MaxValue)] public int ProductTypeId { get; set; }
    }
}
