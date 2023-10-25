using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Services.Business.Shared;

namespace TechChallenge.AutomotiveMechanics.Services.Business.Contract.Service
{
    public class FindServiceCarContract : BaseContract<Domain.Entities.Car>
    {
        public FindServiceCarContract(Domain.Entities.Car input)
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
