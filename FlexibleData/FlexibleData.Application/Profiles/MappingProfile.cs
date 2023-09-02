using AutoMapper;
using FlexibleData.Application.Features.FlexibleData.Commands.CreateFlexibleData;
using FlexibleData.Application.Features.FlexibleData.Queries.GetKeyCount;
using FlexibleData.Domain.Entities;
using Newtonsoft.Json;

namespace FlexibleData.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Entities.FlexibleData, CreateFlexibleDataCommandVm>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(opt => JsonConvert.DeserializeObject<Dictionary<string, string>>(opt.Data)));
            
            CreateMap<Statistics, GetKeyCountQueryVm>();
        }
    }
}
