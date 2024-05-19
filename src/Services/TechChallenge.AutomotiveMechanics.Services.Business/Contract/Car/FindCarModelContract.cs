using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Services.Business.Shared;

namespace TechChallenge.AutomotiveMechanics.Services.Business.Contract.Car
{
    public class FindCarModelContract : BaseContract<Domain.Entities.Model>
    {
        public FindCarModelContract(Domain.Entities.Model input)
        {
            Validate(input);
        }
        protected override void Validate(Domain.Entities.Model input)
        {
            AddNotifications(new Flunt.Br.Contract()
                .Requires()
                .IsNotNull(input, "Model", "Model Id does not exists"));
        }
    }
}
