using ClaimApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClaimApplication.Infrastructure.Persistence.Configurations
{
    public class TypeOfResponsiblePersonConfiguration : IEntityTypeConfiguration<TypeOfResponsiblePerson>
    {
        public void Configure(EntityTypeBuilder<TypeOfResponsiblePerson> builder)
        {
            builder.Property(t => t.Name)
              .HasMaxLength(200)
              .IsRequired();
        }
    }
}
