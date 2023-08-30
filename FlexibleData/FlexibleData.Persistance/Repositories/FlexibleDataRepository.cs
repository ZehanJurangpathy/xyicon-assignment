using FlexibleData.Application.Contracts.Persistence;

namespace FlexibleData.Persistence.Repositories
{
    public class FlexibleDataRepository : Repository<Domain.Entities.FlexibleData>, IFlexibleDataRepository
    {
        #region Fields
        private readonly FlexibleDataContext _context;
        #endregion

        #region Constructor
        /// <summary>Initializes a new instance of the <see cref="FlexibleDataRepository" /> class.</summary>
        /// <param name="context">The context.</param>
        public FlexibleDataRepository(FlexibleDataContext context) : base(context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        /// <summary>Creates flexible data in the database</summary>
        /// <param name="flexibleData">The data to save</param>
        public async Task CreateAsync(Domain.Entities.FlexibleData flexibleData)
        {
            await InsertAsync(flexibleData);
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
