using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechChallenge.AutomotiveMechanics.Domain.Entities
{
    public class Order : Entity
    {
        public int ServiceId { get; set; }
        public string VehicleName { get; set; }
        public decimal ServicePrice { get; set; }
        public string Email { get; set; }
        public bool Paid { get; set; }
    }
}
