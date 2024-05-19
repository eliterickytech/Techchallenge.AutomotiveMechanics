using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Services.Business.Shared;

namespace TechChallenge.AutomotiveMechanics.Services.Business.Contract.Model
{
    public class FindModelContract : BaseContract<Domain.Entities.Model>
    {
        public FindModelContract(Domain.Entities.Model input)
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
