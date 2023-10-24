using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;
using TechChallenge.AutomotiveMechanics.Services.Business.Shared;

namespace TechChallenge.AutomotiveMechanics.Services.Business.Contract.Car
{
    public class UpdateCarContract : BaseContract<CarUpdateInput>
    {
        public UpdateCarContract(CarUpdateInput input) 
        {
            Validate(input);
        }
        protected override void Validate(CarUpdateInput input)
        {
            AddNotifications(new Flunt.Br.Contract()
                .Requires()
                .IsNotNull(input, "Car", "Parameters is null")
                .IsGreaterThan(input.Id, 0, "Car", "The Id field must be greater than 0")
                .IsGreaterThan(input.ModelId, 0, "Model", "The model field must be greater than 0")
                .IsGreaterThan(input.YearManufactured, 0, "YearManufactured", "The YearManufactured field must be greater than 0"));
        }
    }
}
