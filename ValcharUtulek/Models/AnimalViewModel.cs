using System;
using System.ComponentModel.DataAnnotations;
using ValcharUtulek.Domain.Entities;

namespace ValcharUtulek.Models
{
    public class AnimalViewModel
    {
        public int AnimalId { get; set; }

        [Required(ErrorMessage = "Jméno je povinné.")]
        [Display(Name = "Jméno")]
        public string Name { get; set; } = "";

        [Display(Name = "Druh")]
        public string? Species { get; set; }

        [Display(Name = "Plemeno")]
        public string? Breed { get; set; }

        [Display(Name = "Pohlaví")]
        public string? Sex { get; set; }

        [Display(Name = "Datum narození")]
        [DataType(DataType.Date)]
        public DateOnly? BirthDate { get; set; }

        [Display(Name = "Popis")]
        public string? Description { get; set; }

        [Display(Name = "Fotografie")]
        public byte[]? Photo { get; set; }

        [Display(Name = "Je adoptováno")]
        public bool IsAdopted { get; set; }
    }
}