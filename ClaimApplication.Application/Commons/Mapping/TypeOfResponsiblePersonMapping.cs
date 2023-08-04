using AutoMapper;
using ClaimApplication.Application.UseCases.TypeOfResponsiblePeople.Commands.CreateTypeOfResponsiblePerson;
using ClaimApplication.Application.UseCases.TypeOfResponsiblePeople.Commands.DeleteTypeOfResponsiblePerson;
using ClaimApplication.Application.UseCases.TypeOfResponsiblePeople.Commands.UpdateTypeOfResponsiblePerson;
using ClaimApplication.Application.UseCases.TypeOfResponsiblePeople.Response;
using ClaimApplication.Domain.Entities;

namespace ClaimApplication.Application.Commons.Mapping
{
    public class TypeOfResponsiblePersonMapping : Profile
    {
        public TypeOfResponsiblePersonMapping()
        {
            CreateMap<CreateTypeOfResponsiblePersonCommand, TypeOfResponsiblePerson>().ReverseMap();
            CreateMap<DeleteTypeOfResponsiblePersonCommand, TypeOfResponsiblePerson>().ReverseMap();
            CreateMap<UpdateTypeOfResponsiblePersonCommand, TypeOfResponsiblePerson>().ReverseMap();
            CreateMap<TypeOfResponsiblePersonResponse, TypeOfResponsiblePerson>().ReverseMap();
        }
    }
}
