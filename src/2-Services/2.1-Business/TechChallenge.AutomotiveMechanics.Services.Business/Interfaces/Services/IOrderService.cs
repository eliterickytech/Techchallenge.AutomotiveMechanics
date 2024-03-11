using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;
using TechChallenge.AutomotiveMechanics.Services.Business.Result;

namespace TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services
{
    public interface IOrderService
    {
        Task<OrderResult> AddAsync(OrderInsertInput input);
    }
}
