using System;
using System.ComponentModel.DataAnnotations;

namespace ValcharUtulek.Models
{
    public class AdoptionViewModel
    {
        public int AdoptionId { get; set; }

        [Required]
        public int AnimalId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Display(Name = "Datum adopce")]
        [DataType(DataType.Date)]
        public DateOnly AdoptionDate { get; set; }

        [Display(Name = "Poznámky")]
        public string? Notes { get; set; }
    }
}