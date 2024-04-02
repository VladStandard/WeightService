using Ws.Database.EntityFramework.Entities.Ref1C.Plus;

namespace Ws.Database.EntityFramework.Entities.Ref.PluResources;

internal static class PluResourceMapExtension
{
    public static void MapPluResource(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PluResourceEntity>(entity =>
        {
            entity.HasOne<PluEntity>()
                .WithOne(p => p.Resource)
                .HasForeignKey<PluResourceEntity>(pr => pr.Id);
        });
    }
}