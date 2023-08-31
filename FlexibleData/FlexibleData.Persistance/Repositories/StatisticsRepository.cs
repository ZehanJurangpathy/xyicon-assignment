using FlexibleData.Application.Contracts.Persistence;
using FlexibleData.Domain.Entities;

namespace FlexibleData.Persistence.Repositories
{
    public class StatisticsRepository : Repository<Statistics>, IStatisticsRepository
    {
        #region Constructor
        public StatisticsRepository(FlexibleDataContext context) : base(context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        /// <summary>Creates the asynchronous.</summary>
        /// <param name="statistics">The statistics.</param>
        public async Task CreateAsync(Statistics statistics)
        {
            await InsertAsync(statistics);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        ///  update statistics object
        /// </summary>
        /// <param name="statistics"></param>
        public async Task UpdateAsync(Statistics statistics)
        {
            Update(statistics);
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
