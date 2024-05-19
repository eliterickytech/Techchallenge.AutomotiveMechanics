using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Services.Business.Shared;

namespace TechChallenge.AutomotiveMechanics.Services.Business.Contract.Service
{
    public class FindServiceContract : BaseContract<Domain.Entities.Service>
    {
        public FindServiceContract(Domain.Entities.Service input) 
        {
            Validate(input);
        }
        protected override void Validate(Domain.Entities.Service input)
        {
            AddNotifications(new Flunt.Br.Contract()
                .Requires()
                .IsNotNull(input, "Service", "Service Id does not exists"));
        }
    }
}
