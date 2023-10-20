
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using TechChallenge.AutomotiveMechanics.Domain.Entities;

namespace TechChallenge.AutomotiveMechanics.Infrastructure.Data.SeedData
{
    public static class SeedDataExternsion
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
 
            modelBuilder.Entity<Manufacturer>().HasData(
                new Manufacturer
                {
                    Id = 1,
                    Name = "BMW"
                },
                new Manufacturer
                {
                    Id = 2,
                    Name = "VW"
                },
                new Manufacturer
                {
                    Id = 3,
                    Name = "Hyundai"
                }
            );
            #region BMW
            modelBuilder.Entity<Model>().HasData(

                new Model
                {
                    Id = 1,
                    ManufacturerId = 1,
                    Name = "X5",                    
                },
                new Model
                {
                    Id = 2,
                    ManufacturerId = 1,
                    Name = "X6",
                },
                new Model
                {
                    Id = 3,
                    ManufacturerId = 1,
                    Name = "X1",
                },
                new Model
                {
                    Id = 4,
                    ManufacturerId = 1,
                    Name = "X2",
                },
                new Model
                {
                    Id = 5,
                    ManufacturerId = 1,
                    Name = "X3",
                },
                new Model
                {
                    Id = 6,
                    ManufacturerId = 1,
                    Name = "320I",
                },
                new Model
                {
                    Id = 7,
                    ManufacturerId = 1,
                    Name = "330I",
                },
                new Model
                {
                    Id = 8,
                    ManufacturerId = 1,
                    Name = "M3",
                }


            );
            #endregion

            #region VW
            modelBuilder.Entity<Model>().HasData(
                new Model
                {
                    Id = 9,
                    ManufacturerId = 2,
                    Name = "Golf",
                },
                new Model
                {
                    Id = 10,
                    ManufacturerId = 2,
                    Name = "Polo",
                },
                new Model
                {
                    Id = 11,
                    ManufacturerId = 2,
                    Name = "Passat",
                },
                new Model
                {
                    Id = 12,
                    ManufacturerId = 2,
                    Name = "Tiguan",
                },
                new Model
                {
                    Id = 13,
                    ManufacturerId = 2,
                    Name = "Touareg",
                },
                new Model
                {
                    Id = 14,
                    ManufacturerId = 2,
                    Name = "Arteon",
                },
                new Model
                {
                    Id = 15,
                    ManufacturerId = 2,
                    Name = "T-Roc",
                },
                new Model
                {
                    Id = 16,
                    ManufacturerId = 2,
                    Name = "T-Cross",
                },
                new Model
                {
                    Id = 17,
                    ManufacturerId = 2,
                    Name = "Up",
                },
                new Model
                {
                    Id = 18,
                    ManufacturerId = 2,
                    Name = "Amarok",
                },
                new Model
                {
                    Id = 19,
                    ManufacturerId = 2,
                    Name = "Caddy",
                },
                new Model
                {
                    Id = 20,
                    ManufacturerId = 2,
                    Name = "Transporter",
                }
            );
            #endregion

            #region Hyundai
            modelBuilder.Entity<Model>().HasData(
                new Model
                {
                    Id = 21,
                    ManufacturerId = 3,
                    Name = "i30",
                },
                new Model
                {
                    Id = 22,
                    ManufacturerId = 3,
                    Name = "Elantra",
                },
                new Model
                {
                    Id = 23,
                    ManufacturerId = 3,
                    Name = "Kona",
                },
                new Model
                {
                    Id = 24,
                    ManufacturerId = 3,
                    Name = "Tucson",
                },
                new Model
                {
                    Id = 25,
                    ManufacturerId = 3,
                    Name = "Santa Fe",
                },
                new Model
                {
                    Id = 26,
                    ManufacturerId = 3,
                    Name = "Ioniq",
                },
                new Model
                {
                    Id = 27,
                    ManufacturerId = 3,
                    Name = "Veloster",
                }
            );
            #endregion
        }
    }
}
