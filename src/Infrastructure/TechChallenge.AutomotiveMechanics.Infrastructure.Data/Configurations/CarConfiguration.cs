using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Domain.Entities;

namespace TechChallenge.AutomotiveMechanics.Infrastructure.Data.Configurations
{
    public class CarConfiguration
    {
        public class ManufacturerConfiguration : IEntityTypeConfiguration<Car>
        {
            public void Configure(EntityTypeBuilder<Car> builder)
            {
                builder.ToTable("Car");
                builder.Property(e => e.Id).UseIdentityColumn().IsRequired();
                builder.Property(e => e.YearManufactured).IsRequired();
                builder.Property(e => e.Plate).IsRequired().HasMaxLength(10);
                builder.Property(e => e.Enabled).IsRequired().HasDefaultValueSql("(1)");
                builder.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
                builder.Property(e => e.LastModifiedDate);
                builder.HasOne(e => e.Model)
                    .WithMany(e => e.Cars)
                    .HasForeignKey(e => e.ModelId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Car_Model");
            }
        }
    }
}
