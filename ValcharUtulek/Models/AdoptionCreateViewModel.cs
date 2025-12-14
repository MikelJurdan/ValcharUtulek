using System.ComponentModel.DataAnnotations;
using ValcharUtulek.Domain.Entities;

namespace ValcharUtulek.Models
{
    public class AdoptionCreateViewModel
    {
        public int UserId { get; set; }
        public int? AnimalId { get; set; }
        public DateTime AdoptionDate { get; set; }
        public string? AnimalPhoto { get; set; }

        public string? AnimalName { get; set; }
        public string? AnimalSpecies { get; set; }
        public int? AnimalAge { get; set; }
    }
}
