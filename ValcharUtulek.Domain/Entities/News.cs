using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValcharUtulek.Domain.Entities
{
    public class News
    {
        public int NewsId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateOnly DateAdded { get; set; }
        public string? Photo { get; set; }
        public int AuthorId { get; set; }
    }
}
