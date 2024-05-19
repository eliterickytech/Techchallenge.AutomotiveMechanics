using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechChallenge.AutomotiveMechanics.Services.Business.Input
{
    public class CarInsertInput
    {
        public int ModelId { get; set; }    

        public int YearManufactured { get; set; }

        public string Plate { get; set; }
    }
}
