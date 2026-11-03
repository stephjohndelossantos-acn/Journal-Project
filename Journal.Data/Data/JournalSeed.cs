using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Journal.Models;
using Microsoft.EntityFrameworkCore;

namespace Journal.Data
{
    public static class JournalSeed
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            var existing = await context.JournalItems.Select(i => i.Code).ToListAsync();

            var samples = new List<JournalItem>
            {
                new() { Code = "custom01", Title = "Azure Everyday Journal", Description = "A clean and versatile blue notebook-style journal designed for effortless everyday writing. Ideal for capturing thoughts, ideas, plans, and daily moments in a simple, classic format.",
                        Price = 1700, ImageUrl = "https://journalstorageaccount.blob.core.windows.net/journals/custom/Nama Pacis Handmade Notebook Journal.jpg",
                        SortOrder = 10, ProductTypeId = 4 },
                new() { Code = "croc01", Title = "Brown-Crocodile Embrossed", Description = "A sophisticated brown journal featuring a crocodile-embossed cover for a timeless, refined look. Perfect for personal reflections, important notes, and everyday writing.",
                        Price = 1800, ImageUrl = "https://journalstorageaccount.blob.core.windows.net/journals/crocodile/brown-crocodile embossed.jpg",
                        IsFeatured = false, SortOrder = 10, ProductTypeId = 3 },
                // add one line per journal: crocodile01 (type 3), custom01 (type 4), and so on
            };

            var toAdd = samples.Where(s => !existing.Contains(s.Code)).ToList();
            if (toAdd.Count == 0) return;

            context.JournalItems.AddRange(toAdd);
            await context.SaveChangesAsync();
        }
    }
}