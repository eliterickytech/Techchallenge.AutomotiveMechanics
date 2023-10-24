using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;
using TechChallenge.AutomotiveMechanics.Services.Business.Shared;

namespace TechChallenge.AutomotiveMechanics.Services.Business.Contract.Model
{
    public class AddModelContract : BaseContract<ModelInsertInput>
    {
        public AddModelContract(ModelInsertInput input)
        {
            Validate(input);
        }
        protected override void Validate(ModelInsertInput input)
        {
            AddNotifications(new Flunt.Br.Contract()
                .Requires()
                .IsNotNull(input, "Model", "Parameters is null")
                .IsNotNullOrWhiteSpace(input.Name, "Name", "The Name field cannot be empty")
                .IsGreaterThan(input.Name, 3, "Name", "The Name field must be greater than 3")
                .IsGreaterThan(input.ManufacturerId, 0, "Manufacturer", "The manufacturer field must be greater than 0"));

        }
    }
}
