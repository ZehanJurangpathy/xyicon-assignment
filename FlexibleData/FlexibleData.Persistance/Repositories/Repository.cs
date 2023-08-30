using FlexibleData.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FlexibleData.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        #region Fields
        internal FlexibleDataContext _context = null;
        internal DbSet<T> _table = null;
        #endregion

        #region Constructor
        public Repository(FlexibleDataContext context)
        {
            _context = context;
            _table = context.Set<T>();
        }
        #endregion

        #region Methods
        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = _table;

            if (filter is not null)
            {
                query = query.Where(filter);
            }

            foreach (var includedProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includedProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await _table.FindAsync(id);
        }

        public async Task InsertAsync(T obj)
        {
            await _table.AddAsync(obj);
        }

        public void Update(T obj)
        {
            _table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        public async Task DeleteAsync(object id)
        {
            T entityToDelete = await _table.FindAsync(id);
            DeleteAsync(entityToDelete);
        }

        public void DeleteAsync(T entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _table.Attach(entityToDelete);
            }
            _table.Remove(entityToDelete);
        }
        #endregion
    }
}
