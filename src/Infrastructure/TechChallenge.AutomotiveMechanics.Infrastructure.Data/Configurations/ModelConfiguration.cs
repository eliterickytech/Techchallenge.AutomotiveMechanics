using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Model = TechChallenge.AutomotiveMechanics.Domain.Entities.Model;

namespace TechChallenge.AutomotiveMechanics.Infrastructure.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Model>
    {
        public void Configure(EntityTypeBuilder<Model> builder)
        {
            builder.ToTable("Model");
            builder.Property(e => e.Id).UseIdentityColumn().IsRequired();
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(getdate())");
            builder.Property(e => e.Enabled).IsRequired().HasDefaultValueSql("(1)");
            builder.Property(e => e.LastModifiedDate);

            builder.HasMany(e => e.Cars)
                .WithOne(e => e.Model)
                .HasForeignKey(e => e.ModelId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Car_Model");
        }
    }
}
