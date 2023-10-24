﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Services.Business.Shared;

namespace TechChallenge.AutomotiveMechanics.Services.Business.Contract.Model
{
    public class FindManufacturerModelContract : BaseContract<Domain.Entities.Manufacturer>
    {
        public FindManufacturerModelContract(Domain.Entities.Manufacturer input)
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
