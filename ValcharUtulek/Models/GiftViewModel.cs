using System;
using System.ComponentModel.DataAnnotations;

namespace ValcharUtulek.Models
{
    public class GiftViewModel
    {
        public int GiftId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Částka je povinná.")]
        [Range(1, double.MaxValue, ErrorMessage = "Částka musí být kladné číslo.")]
        [Display(Name = "Částka")]
        public decimal Amount { get; set; }

        [Display(Name = "Datum daru")]
        [DataType(DataType.Date)]
        public DateOnly GiftDate { get; set; }

        [Display(Name = "Zpráva")]
        public string? Message { get; set; }
    }
}