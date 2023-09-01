using MediatR;

namespace FlexibleData.Application.Features.FlexibleData.Queries.GetKeyCount
{
    public class GetKeyCountQuery : IRequest<IEnumerable<GetKeyCountQueryVm>>
    {
        public string? Key { get; set; }
    }
}
