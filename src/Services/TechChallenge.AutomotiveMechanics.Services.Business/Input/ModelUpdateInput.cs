using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechChallenge.AutomotiveMechanics.Services.Business.Input
{
    public class ModelUpdateInput
    {
        public int Id { get; set; }

        public int ManufacturerId { get; set; }

        public string Name { get; set; }
    }
}
