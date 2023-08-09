using AutoMapper;
using ClaimApplication.Application.UseCases.Applications.Response;
using ClaimApplication.Application.UseCases.Memberships.Commands.DeleteMembership;
using ClaimApplication.Application.UseCases.Memberships.Commands.UpdateMembership;
using ClaimMembershipApplication.MembershipApplication.UseCases.MembershipApplications.Commands.CreateMembershipApplication;

namespace ClaimMembershipApplication.MembershipApplication.Commons.Mapping
{
    public class MembershipApplicationMapping : Profile
    {
        public MembershipApplicationMapping()
        {
            CreateMap<CreateMembershipApplicationCommand, ClaimApplication.Domain.Memberships.MembershipApplication>().ReverseMap();
            CreateMap<DeleteMembershipApplicationCommand, ClaimApplication.Domain.Memberships.MembershipApplication>().ReverseMap();
            CreateMap<UpdateMembershipApplicationCommand, ClaimApplication.Domain.Memberships.MembershipApplication>().ReverseMap();
            CreateMap<MembershipApplicationResponse, ClaimApplication.Domain.Memberships.MembershipApplication>().ReverseMap();
        }
    }
}
