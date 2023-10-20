using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Domain.Entities;

namespace TechChallenge.AutomotiveMechanics.Services.Business.Input
{
    public class ServiceInsertInput
    {
        public string Name { get; set; }

        public int CarId { get; set; }

    }
}
