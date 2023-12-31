﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Services.Business.Input;
using TechChallenge.AutomotiveMechanics.Services.Business.Result;

namespace TechChallenge.AutomotiveMechanics.Services.Business.Interfaces.Services
{
    public interface IModelService
    {
        Task<ModelResult> AddAsync(ModelInsertInput input);
        Task<bool> DeleteAsync(int id);
        Task<ModelResult> FindByIdAsync(int id);
        Task<IList<ModelResult>> ListAsync();
        Task<ModelResult> UpdateAsync(ModelUpdateInput input);
    }
}
