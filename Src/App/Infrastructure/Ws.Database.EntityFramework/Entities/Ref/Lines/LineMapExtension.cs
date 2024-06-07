using Ws.Database.EntityFramework.Entities.Ref1C.Plus;

namespace Ws.Database.EntityFramework.Entities.Ref.Lines;

internal static class LineMapExtension
{
    public static void MapLine(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LineEntity>(entity =>
        {
            entity.HasOne(l => l.Warehouse)
                .WithMany()
                .HasForeignKey("WAREHOUSE_UID")
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(l => l.Printer)
                .WithMany()
                .HasForeignKey("PRINTER_UID")
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(e => e.Plus)
                .WithMany()
                .UsingEntity(
                "ARMS_PLUS_FK",
                l => l.HasOne(typeof(PluEntity))
                    .WithMany()
                    .HasForeignKey("PLU_UID")
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasPrincipalKey(nameof(PluEntity.Id)),
                r => r.HasOne(typeof(LineEntity))
                    .WithMany()
                    .HasForeignKey("ARM_UID")
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasPrincipalKey(nameof(LineEntity.Id)),
                j => j.HasKey("PLU_UID", "ARM_UID"));
        });
    }
}