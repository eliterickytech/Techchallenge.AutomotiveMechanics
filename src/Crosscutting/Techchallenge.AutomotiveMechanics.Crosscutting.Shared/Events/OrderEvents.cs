using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechChallenge.AutomotiveMechanics.Crosscutting.Shared.Events
{
    public class OrderEvents
    {
        public string VehicleName { get; set; }
        public decimal ServicePrice { get; set; }
        public string Email { get; set; }

        public OrderEvents(string vehicleName, decimal servicePrice, string email)
        {
            VehicleName = vehicleName;
            ServicePrice = servicePrice;
            Email = email;
        }
    }
}
