using System.ComponentModel.DataAnnotations;

namespace ValcharUtulek.Application.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Jméno je povinné.")]
        [Display(Name = "Uživatelské jméno")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Email je povinný.")]
        [EmailAddress(ErrorMessage = "Neplatná emailová adresa.")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Heslo je povinné.")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "{0} musí mít délku alespoň {2}", MinimumLength = 6)]
        [Display(Name = "Heslo")]
        public string Password { get; set; } = "";
    }
}
