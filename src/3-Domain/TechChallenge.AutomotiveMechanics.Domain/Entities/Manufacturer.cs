using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechChallenge.AutomotiveMechanics.Domain.Entities
{
    public class Manufacturer : Entity
    {
        public Manufacturer() 
        { 
            Models = new HashSet<Model>();
        }
        public string Name { get; set; }

        public ICollection<Model> Models { get; set; }
    }
}
