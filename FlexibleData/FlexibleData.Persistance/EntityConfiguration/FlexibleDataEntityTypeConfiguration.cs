using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlexibleData.Persistance.EntityConfiguration
{
    public class FlexibleDataEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Entities.FlexibleData>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.FlexibleData> builder)
        {
            builder
                .ToTable("FlexibleData")
                .HasKey(t => t.Id);
        }
    }
}
