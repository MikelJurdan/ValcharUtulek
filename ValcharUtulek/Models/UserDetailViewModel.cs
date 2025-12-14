using ValcharUtulek.Domain.Entities;

namespace ValcharUtulek.Models
{
    public class UserDetailViewModel
    {
        public int UserId { get; set; }
        public string Name { get; set; } = "";
        public string? Email { get; set; }
        public DateOnly RegistrationDate { get; set; }
        public Role Role { get; set; }
        
        // Seznam adoptovaných zvířat
        public List<AdoptionDetailViewModel> Adoptions { get; set; } = new();
        
        // Seznam darů
        public List<Gift> Gifts { get; set; } = new();
    }
    
    public class AdoptionDetailViewModel
    {
        public int AdoptionId { get; set; }
        public int AnimalId { get; set; }
        public string AnimalName { get; set; } = "";
        public string? AnimalSpecies { get; set; }
        public DateOnly AdoptionDate { get; set; }
        public double Amount { get; set; }
    }
}
