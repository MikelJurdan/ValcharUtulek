using System;
using System.ComponentModel.DataAnnotations;

namespace ValcharUtulek.Models
{
    public class NewsViewModel
    {
        public int NewsId { get; set; }

        [Required(ErrorMessage = "Titulek je povinný.")]
        [Display(Name = "Titulek")]
        public string Title { get; set; } = "";

        [Required(ErrorMessage = "Obsah je povinný.")]
        [Display(Name = "Obsah")]
        public string Content { get; set; } = "";

        [Display(Name = "Datum přidání")]
        [DataType(DataType.Date)]
        public DateOnly DateAdded { get; set; }

        [Display(Name = "Fotografie")]
        public byte[]? Photo { get; set; }

        [Required]
        public int AuthorId { get; set; }
    }
}