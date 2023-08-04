using ClaimApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClaimApplication.Infrastructure.Persistence.Configurations
{
    public class AppealTypeConfiguration : IEntityTypeConfiguration<AppealType>
    {
        public void Configure(EntityTypeBuilder<AppealType> builder)
        {
            builder.Property(t => t.Name)
              .HasMaxLength(200)
              .IsRequired();
        }
    }
}
