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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");
            builder.Property(e => e.Id).UseIdentityColumn().IsRequired();
            builder.Property(e => e.VehicleName)
                .IsRequired()
                .HasMaxLength(60);
            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(80);
            builder.Property(e => e.ServicePrice);
            builder.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.LastModifiedDate);
        }
    }
}
