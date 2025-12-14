using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValcharUtulek.Domain.Entities
{
    public class Gift
    {
        public int GiftId { get; set; }
        public int UserId { get; set; }
        public double Amount { get; set; }
        public DateOnly GiftDate { get; set; }
    }
}
