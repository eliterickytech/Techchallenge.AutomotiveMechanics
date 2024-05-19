using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.AutomotiveMechanics.Domain.Entities;

namespace TechChallenge.AutomotiveMechanics.Infrastructure.Data.Configurations
{
    public class ManufacturerConfiguration : IEntityTypeConfiguration<Manufacturer>
    {
        public void Configure(EntityTypeBuilder<Manufacturer> builder)
        {
            builder.ToTable("Manufacturer");
            builder.Property(e => e.Id).UseIdentityColumn().IsRequired();
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.Enabled).IsRequired().HasDefaultValueSql("(1)");
            builder.Property(e => e.LastModifiedDate);

            builder.HasMany(e => e.Models)
                .WithOne(e => e.Manufacturer)
                .HasForeignKey(e => e.ManufacturerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Model_Manufacturer");
        }
    }
}
