using FlexibleData.Persistance.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace FlexibleData.Persistance
{
    public class FlexibleDataContext : DbContext
    {
        public DbSet<Domain.Entities.FlexibleData> FlexibleData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //register entity type configuration
            new FlexibleDataEntityTypeConfiguration()
                .Configure(modelBuilder.Entity<Domain.Entities.FlexibleData>());
        }
    }
}
