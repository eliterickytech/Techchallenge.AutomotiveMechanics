using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Services.Business.Shared;

namespace TechChallenge.AutomotiveMechanics.Services.Business.Contract.Car
{
    public class FindCarContract : BaseContract<Domain.Entities.Car>
    {
        public FindCarContract(Domain.Entities.Car input)
        {
            Validate(input);
        }
        protected override void Validate(Domain.Entities.Car input)
        {
            AddNotifications(new Flunt.Br.Contract()
                .Requires()
                .IsNotNull(input, "Car", "Car Id does not exists"));
        }
    }
}
