using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ValcharUtulek.Domain.Entities;

namespace ValcharUtulek.Models
{
    public class GiftViewModel
    {
        public int GiftId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int AnimalId { get; set; }

        [Required(ErrorMessage = "Částka je povinná.")]
        [Range(100, double.MaxValue, ErrorMessage = "Částka musí být minimálně 100 Kč.")]
        [Display(Name = "Částka")]
        public decimal Amount { get; set; }

        [Display(Name = "Datum daru")]
        [DataType(DataType.Date)]
        public DateOnly GiftDate { get; set; }

        [Display(Name = "Zpráva")]
        public string? Message { get; set; }

        public IEnumerable<Animal> Animals { get; set; } = new List<Animal>();
        public IEnumerable<Gift> Gifts { get; set; } = new List<Gift>();
    }
}