using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechChallenge.AutomotiveMechanics.Services.Business.Result
{
    public class OrderResult
    {
        public int Id { get; set; }

        public int ServiceId { get; set; }
        public string VehicleName { get; set; }
        public decimal ServicePrice { get; set; }
        public string Email { get; set; }
        public bool Paid { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? LastModifiedDate { get; set; }
    }
}
