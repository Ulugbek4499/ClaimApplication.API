using ClaimApplication.Domain.Entities;
using ClaimApplication.Domain.Memberships;
using Microsoft.EntityFrameworkCore;

namespace ClaimApplication.Application.Commons.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<AppealPredmet> AppealPredmets { get; set; }
        DbSet<AppealType> AppealTypes { get; set; }
        DbSet<ResponsiblePerson> ResponsiblePeople { get; set; }
        DbSet<TypeOfResponsiblePerson> TypeOfResponsiblePeople { get; set; }
        DbSet<Domain.Entities.Application> Applications { get; set; }
        DbSet<MembershipApplication> MembershipApplications { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
