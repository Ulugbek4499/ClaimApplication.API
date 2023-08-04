using AutoMapper;
using ClaimApplication.Application.UseCases.AppealPredmets.Commands.CreateAppealPredmet;
using ClaimApplication.Application.UseCases.AppealPredmets.Commands.DeleteAppealPredmet;
using ClaimApplication.Application.UseCases.AppealPredmets.Commands.UpdateAppealPredmet;
using ClaimApplication.Application.UseCases.AppealPredmets.Response;
using ClaimApplication.Domain.Entities;

namespace ClaimApplication.Application.Commons.Mapping
{
    public class AppealPredmetMapping : Profile
    {
        public AppealPredmetMapping()
        {
            CreateMap<CreateAppealPredmetCommand, AppealPredmet>().ReverseMap();
            CreateMap<DeleteAppealPredmetCommand, AppealPredmet>().ReverseMap();
            CreateMap<UpdateAppealPredmetCommand, AppealPredmet>().ReverseMap();
            CreateMap<AppealPredmetResponse, AppealPredmet>().ReverseMap();
        }
    }
}
