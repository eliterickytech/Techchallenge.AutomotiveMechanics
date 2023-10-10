using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechChallenge.AutomotiveMechanics.Domain.Entities
{
    public class Service : Entity
    {
        public Service()
        {
            Cars = new HashSet<Car>();
        }
        public string Name { get; set; }

        public ICollection<Car> Cars { get; set; }
    }
}
