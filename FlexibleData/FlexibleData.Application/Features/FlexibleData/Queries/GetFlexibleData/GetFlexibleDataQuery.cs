using MediatR;

namespace FlexibleData.Application.Features.FlexibleData.Queries.GetFlexibleData
{
    public class GetFlexibleDataQuery : IRequest<IEnumerable<GetFlexibleDataQueryVm>>
    {
        public Guid? Id { get; set; }
    }
}
