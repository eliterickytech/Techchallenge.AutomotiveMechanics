using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechChallenge.AutomotiveMechanics.Domain.Entities
{
    public class Model : Entity
    {
        public Model()
        {
            Cars = new HashSet<Car>();
        }

        public int ManufacturerId { get; set; }

        public string Name { get; set; }

        public Manufacturer Manufacturer { get; set; }

        public ICollection<Car> Cars { get; set; }
    }
}
