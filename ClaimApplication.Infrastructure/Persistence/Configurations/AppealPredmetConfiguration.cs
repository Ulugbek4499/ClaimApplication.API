using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ClaimApplication.Domain.Entities;

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
