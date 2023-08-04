using ClaimApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClaimApplication.Infrastructure.Persistence.Configurations
{

    public class AppealPredmetConfiguration : IEntityTypeConfiguration<AppealPredmet>
    {
        public void Configure(EntityTypeBuilder<AppealPredmet> builder)
        {
            builder.Property(t => t.Name)
              .HasMaxLength(200)
              .IsRequired();
        }
    }
}
