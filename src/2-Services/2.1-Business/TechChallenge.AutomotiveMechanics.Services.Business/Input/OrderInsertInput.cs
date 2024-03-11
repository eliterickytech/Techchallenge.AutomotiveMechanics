using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechChallenge.AutomotiveMechanics.Services.Business.Input
{
    public class OrderInsertInput
    {
        public string VehicleName { get; set; }
        public decimal ServicePrice { get; set; }
        public string Email { get; set; }
    }
}
