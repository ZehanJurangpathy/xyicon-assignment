using MediatR;

namespace FlexibleData.Application.Features.FlexibleData.Commands.CreateFlexibleData
{
    public class CreateFlexibleDataCommand : IRequest<CreateFlexibleDataCommandVm>
    {
        public Dictionary<string, string> Data { get; set; }
    }
}
