﻿using System.Reflection;
using ClaimApplication.Application.Commons.Interfaces;
using ClaimApplication.Domain.Entities;
using ClaimApplication.Domain.Memberships;
using ClaimApplication.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace ClaimApplication.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

        public DbSet<AppealPredmet> AppealPredmets { get; set; }
        public DbSet<AppealType> AppealTypes { get; set; }
        public DbSet<ResponsiblePerson> ResponsiblePeople { get; set; }
        public DbSet<TypeOfResponsiblePerson> TypeOfResponsiblePeople { get; set; }
        public DbSet<Domain.Entities.Application> Applications { get; set; }
        public DbSet<MembershipApplication> MembershipApplications { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


        public ApplicationDbContext(
          DbContextOptions<ApplicationDbContext> options,
          AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
          : base(options)
        {
            _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
        }
    }
}
