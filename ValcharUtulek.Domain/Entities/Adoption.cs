using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValcharUtulek.Domain.Entities
{
    public class Adoption
    {
        public int AdoptionId { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int AnimalId { get; set; }
        public Animal? Animal { get; set; }
        public DateOnly AdoptionDate { get; set; }
        public double Amount { get; set; }
    }
}
