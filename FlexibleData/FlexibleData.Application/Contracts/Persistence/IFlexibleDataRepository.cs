namespace FlexibleData.Application.Contracts.Persistence
{
    public interface IFlexibleDataRepository : IRepository<Domain.Entities.FlexibleData>
    {
        Task CreateAsync(Domain.Entities.FlexibleData flexibleData);
    }
}
