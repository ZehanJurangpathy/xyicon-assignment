using AutoMapper;
using FlexibleData.Application.Features.FlexibleData.Commands.CreateFlexibleData;
using FlexibleData.Application.Features.FlexibleData.Queries.GetKeyCount;
using FlexibleData.Domain.Entities;

namespace FlexibleData.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Entities.FlexibleData, CreateFlexibleDataCommandVm>();
            CreateMap<Statistics, GetKeyCountQueryVm>();
        }
    }
}
