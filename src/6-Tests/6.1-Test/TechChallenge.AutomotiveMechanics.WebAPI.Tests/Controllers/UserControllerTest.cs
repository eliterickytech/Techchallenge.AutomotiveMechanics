using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Presentation.API.Controllers;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;
using TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services;
using TechChallenge.AutomotiveMechanics.Services.Business.Result;
using TechChallenge.AutomotiveMechanics.Services.Business.Services;

namespace TechChallenge.AutomotiveMechanics.WebAPI.Tests.Controllers
{
    public class UserControllerTest
    {
        private readonly IUserService _userService;
        private readonly UserController controller;
        private readonly UserResult _userResult;

        public UserControllerTest(IUserService userService,
            UserController controller, UserResult userResult)
        {
            _userService = userService;
            this.controller = controller;
            _userResult = userResult;
        }
    }
}
