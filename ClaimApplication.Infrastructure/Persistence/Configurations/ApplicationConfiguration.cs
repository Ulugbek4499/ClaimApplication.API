using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaimApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ClaimApplication.Infrastructure.Persistence.Configurations
{
    public class ApplicationConfiguration : IEntityTypeConfiguration<Domain.Entities.Application>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Application> builder)
        {
            builder.Property(t => t.Inn)
              .HasMaxLength(200)
              .IsRequired();
        }
    }
}
