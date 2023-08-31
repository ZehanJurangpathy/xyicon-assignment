using FlexibleData.Domain.Entities;

namespace FlexibleData.Application.Contracts.Persistence
{
    public interface IStatisticsRepository : IRepository<Statistics>
    {
        Task CreateAsync(Statistics statistics);

        Task UpdateAsync(Statistics statistics);
    }
}
