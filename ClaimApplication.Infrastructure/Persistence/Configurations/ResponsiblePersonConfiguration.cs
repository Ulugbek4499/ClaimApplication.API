using ClaimApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClaimApplication.Infrastructure.Persistence.Configurations
{
    public class ResponsiblePersonConfiguration : IEntityTypeConfiguration<ResponsiblePerson>
    {
        public void Configure(EntityTypeBuilder<ResponsiblePerson> builder)
        {
            builder.Property(t => t.Address)
              .HasMaxLength(200)
              .IsRequired();
        }
    }
}
