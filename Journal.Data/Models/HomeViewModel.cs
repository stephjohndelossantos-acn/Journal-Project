using System;
using System.Collections.Generic;
using System.Text;

namespace Journal.Models
{
    public class HomeViewModel
    {
        public JournalItem? Featured { get; set; }
        public List<ProductType> Types { get; set; } = new();
    }
}
