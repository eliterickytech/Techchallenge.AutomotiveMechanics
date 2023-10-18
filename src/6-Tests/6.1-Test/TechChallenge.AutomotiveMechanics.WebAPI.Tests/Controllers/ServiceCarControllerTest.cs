using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Presentation.API.Controllers;
using TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services;
using TechChallenge.AutomotiveMechanics.Services.Business.Result;

namespace TechChallenge.AutomotiveMechanics.WebAPI.Tests.Controllers
{
    public class ServiceCarControllerTest
    {
        private readonly IServiceCarService _serviceCarService;
        private readonly ServiceCarController controller;

        public ServiceCarControllerTest(IServiceCarService serviceCarService,
            ServiceCarController controller)
        {
            _serviceCarService = serviceCarService;
            this.controller = controller;
        }


    }
}
