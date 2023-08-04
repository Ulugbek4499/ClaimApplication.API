using AutoMapper;
using ClaimApplication.Application.UseCases.AppealTypes.Commands.CreateAppealType;
using ClaimApplication.Application.UseCases.AppealTypes.Commands.DeleteAppealType;
using ClaimApplication.Application.UseCases.AppealTypes.Commands.UpdateAppealType;
using ClaimApplication.Application.UseCases.AppealTypes.Response;
using ClaimApplication.Domain.Entities;

namespace ClaimApplication.Application.Commons.Mapping
{
    public class AppealTypeMapping : Profile
    {
        public AppealTypeMapping()
        {
            CreateMap<CreateAppealTypeCommand, AppealType>().ReverseMap();
            CreateMap<DeleteAppealTypeCommand, AppealType>().ReverseMap();
            CreateMap<UpdateAppealTypeCommand, AppealType>().ReverseMap();
            CreateMap<AppealTypeResponse, AppealType>().ReverseMap();
        }
    }
}
