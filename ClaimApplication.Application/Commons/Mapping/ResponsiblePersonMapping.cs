using AutoMapper;
using ClaimApplication.Application.UseCases.ResponsiblePeople.Commands.CreateResponsiblePerson;
using ClaimApplication.Application.UseCases.ResponsiblePeople.Commands.DeleteResponsiblePerson;
using ClaimApplication.Application.UseCases.ResponsiblePeople.Commands.UpdateResponsiblePerson;
using ClaimApplication.Application.UseCases.ResponsiblePeople.Response;
using ClaimApplication.Domain.Entities;

namespace ClaimApplication.Application.Commons.Mapping
{
    public class ResponsiblePersonMapping : Profile
    {
        public ResponsiblePersonMapping()
        {
            CreateMap<CreateResponsiblePersonCommand, ResponsiblePerson>().ReverseMap();
            CreateMap<DeleteResponsiblePersonCommand, ResponsiblePerson>().ReverseMap();
            CreateMap<UpdateResponsiblePersonCommand, ResponsiblePerson>().ReverseMap();
            CreateMap<ResponsiblePersonResponse, ResponsiblePerson>().ReverseMap();
        }
    }
}
