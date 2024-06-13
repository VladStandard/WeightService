using Ws.Database.EntityFramework.Entities.Ref1C.Plus;

namespace Ws.Database.EntityFramework.Entities.Ref1C.Nestings;

internal static class NestingMapExtension
{
    public static void MapNesting(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NestingEntity>()
            .HasOne<PluEntity>()
            .WithOne()
            .HasForeignKey<NestingEntity>(n => n.Id)
            .HasPrincipalKey<PluEntity>(p => p.Id);
    }
}