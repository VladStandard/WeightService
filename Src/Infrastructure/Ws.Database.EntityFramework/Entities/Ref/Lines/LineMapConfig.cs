using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ws.Database.EntityFramework.Entities.Ref1C.Plus;

namespace Ws.Database.EntityFramework.Entities.Ref.Lines;

internal class LineMapConfig : IEntityTypeConfiguration<LineEntity>
{
    public void Configure(EntityTypeBuilder<LineEntity> builder)
    {
        builder.HasOne(l => l.Warehouse)
            .WithMany()
            .HasForeignKey("WAREHOUSE_UID")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(l => l.Printer)
            .WithMany()
            .HasForeignKey("PRINTER_UID")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.Plus)
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
    }
}