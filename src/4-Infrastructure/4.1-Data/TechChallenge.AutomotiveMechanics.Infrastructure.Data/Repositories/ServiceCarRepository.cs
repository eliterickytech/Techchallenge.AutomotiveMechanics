using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Domain.Entities;
using TechChallenge.AutomotiveMechanics.Domain.Interfaces.Repositories;

namespace TechChallenge.AutomotiveMechanics.Infrastructure.Data.Repositories
{
    public class ServiceCarRepository : BaseRepository<Service>, IServiceCarRepository
    {
        public ServiceCarRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Service> AddServiceCarAsync(Service service) 
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var cars = service.Cars;

                    service.Cars = null;

                    var founded = await _context.Car.FindAsync(cars.FirstOrDefault().Id);

                    founded.Services.Add(service);

                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return service;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
