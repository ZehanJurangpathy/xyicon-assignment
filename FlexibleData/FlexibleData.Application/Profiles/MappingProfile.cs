using AutoMapper;
using FlexibleData.Application.Features.FlexibleData.Commands.CreateFlexibleData;

namespace FlexibleData.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Entities.FlexibleData, CreateFlexibleDataCommandVm>();
        }
    }
}
