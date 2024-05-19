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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.Property(e => e.Id).UseIdentityColumn().IsRequired();
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(60);
            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(80);
            builder.Property(e => e.PasswordHash);
            builder.Property(e => e.PasswordSalt);
            builder.Property(e => e.Enabled)
                .IsRequired()
                .HasDefaultValueSql("(1)");
            builder.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.LastModifiedDate);
        }
    }
}
