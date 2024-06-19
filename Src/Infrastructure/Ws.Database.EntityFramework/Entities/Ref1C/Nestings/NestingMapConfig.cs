using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ws.Database.EntityFramework.Entities.Ref1C.Plus;

namespace Ws.Database.EntityFramework.Entities.Ref1C.Nestings;

internal class NestingMapConfig : IEntityTypeConfiguration<NestingEntity>
{
    public void Configure(EntityTypeBuilder<NestingEntity> builder)
    {
        builder.HasOne<PluEntity>()
            .WithOne()
            .HasForeignKey<NestingEntity>(n => n.Id)
            .HasPrincipalKey<PluEntity>(p => p.Id);
    }
}