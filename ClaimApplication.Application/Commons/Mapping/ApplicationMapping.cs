using AutoMapper;
using ClaimApplication.Application.UseCases.Applications.Commands.CreateApplication;
using ClaimApplication.Application.UseCases.Applications.Commands.DeleteApplication;
using ClaimApplication.Application.UseCases.Applications.Commands.UpdateApplication;
using ClaimApplication.Application.UseCases.Applications.Response;

namespace ClaimApplication.Application.Commons.Mapping
{
    public class ApplicationMapping : Profile
    {
        public ApplicationMapping()
        {
            CreateMap<CreateApplicationCommand, Domain.Entities.Application>().ReverseMap();
            CreateMap<DeleteApplicationCommand, Domain.Entities.Application>().ReverseMap();
            CreateMap<UpdateApplicationCommand, Domain.Entities.Application>().ReverseMap();
            CreateMap<ApplicationResponse, Domain.Entities.Application>().ReverseMap();
        }
    }
}
