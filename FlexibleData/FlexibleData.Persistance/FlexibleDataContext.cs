using FlexibleData.Domain.Entities;
using FlexibleData.Persistence.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace FlexibleData.Persistence
{
    public class FlexibleDataContext : DbContext
    {
        public DbSet<Domain.Entities.FlexibleData> FlexibleData { get; set; }

        public DbSet<Statistics> Statistics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //register entity type configuration
            new FlexibleDataEntityTypeConfiguration()
                .Configure(modelBuilder.Entity<Domain.Entities.FlexibleData>());

            new StatisticsEntityTypeConfiguration()
                .Configure(modelBuilder.Entity<Statistics>());
        }

        public FlexibleDataContext(DbContextOptions options): base(options)
        {
            
        }
    }
}
