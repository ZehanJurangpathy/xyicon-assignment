using FlexibleData.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlexibleData.Persistence.EntityConfiguration
{
    public class StatisticsEntityTypeConfiguration : IEntityTypeConfiguration<Statistics>
    {
        public void Configure(EntityTypeBuilder<Statistics> builder)
        {
            builder
                .ToTable("Statistics")
                .HasKey(t => t.Key);
        }
    }
}
