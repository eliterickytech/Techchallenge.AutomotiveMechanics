using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechChallenge.AutomotiveMechanics.Domain.Entities
{
    public class Car : Entity
    {
        public int ModelId { get; set; }  
                
        public int YearManufactured { get; set; }

        public string Plate { get; set; }

        public Model Model { get; set; }

        public ICollection<Service> Services { get; set; }
    }
}
