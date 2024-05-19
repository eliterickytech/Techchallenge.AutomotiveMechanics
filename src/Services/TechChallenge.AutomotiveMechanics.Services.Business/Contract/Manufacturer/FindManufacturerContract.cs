using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Services.Business.Shared;

namespace TechChallenge.AutomotiveMechanics.Services.Business.Contract.Manufacturer
{
    public class FindManufacturerContract : BaseContract<Domain.Entities.Manufacturer>
    {
        public FindManufacturerContract(Domain.Entities.Manufacturer input)
        {
            Validate(input);
        }
        protected override void Validate(Domain.Entities.Manufacturer input)
        {
            AddNotifications(new Flunt.Br.Contract()
                .Requires()
                .IsNotNull(input, "Manufacturer", "Manufacturer Id does not exists"));
        }
    }
}
