using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ValcharUtulek.Domain.Entities
{
    public enum Role
    {
        Admin,
        Zakaznik
    }

    public class User 
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; } = "";
        public string? Email { get; set; }
        [Column("password_hash")]
        public string PasswordHash { get; set; } = "";
        public DateOnly RegistrationDate { get; set; }
        [Column(TypeName = "varchar(20)")]
        public Role Role { get; set; }

    }
}
