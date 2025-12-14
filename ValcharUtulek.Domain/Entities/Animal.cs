using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValcharUtulek.Domain.Entities
{
    public class Animal
    {
        public int AnimalId { get; set; }
        public string Name { get; set; } = "";
        public string? Species { get; set; }
        public string? Gender { get; set; }
        public int Age { get; set; }
        public string? Description { get; set; }
        public string? Photo { get; set; }
        public bool IsAvailable { get; set; }
    }
}
