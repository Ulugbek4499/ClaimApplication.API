using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ClaimApplication.Domain.Entities;

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
