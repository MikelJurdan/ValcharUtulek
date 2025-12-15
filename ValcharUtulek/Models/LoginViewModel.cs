using System.ComponentModel.DataAnnotations;

namespace ValcharUtulek.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Uživatelské jméno je povinné.")]
        [Display(Name = "Uživatelské jméno")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Heslo je povinné.")]
        [DataType(DataType.Password)]
        [Display(Name = "Heslo")]
        public string Password { get; set; } = "";
    }
}
