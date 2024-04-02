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
                "LINES_PLUS_FK",
                l => l.HasOne(typeof(PluEntity))
                    .WithMany()
                    .HasForeignKey("PLU_UID")
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasPrincipalKey(nameof(PluEntity.Id)),
                r => r.HasOne(typeof(LineEntity))
                    .WithMany()
                    .HasForeignKey("LINE_UID")
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasPrincipalKey(nameof(LineEntity.Id)),
                j => j.HasKey("PLU_UID", "LINE_UID"));
        });
    }
}