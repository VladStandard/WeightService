using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ws.Database.EntityFramework.Entities.Print.Pallets;

public class PalletMapConfig : IEntityTypeConfiguration<PalletEntity>
{
    public void Configure(EntityTypeBuilder<PalletEntity> builder)
    {
        #region Base

        builder.ToTable(SqlTables.Pallets, SqlSchemas.Print);

        builder.HasIndex(e => e.Barcode)
            .HasDatabaseName($"UQ_{SqlTables.Pallets}__BARCODE")
            .IsUnique();

        builder.HasIndex(e => e.Counter)
            .HasDatabaseName($"UQ_{SqlTables.Pallets}__COUNTER")
            .IsUnique();

        #endregion

        #region FK

        builder.HasOne(e => e.Arm)
            .WithMany()
            .HasForeignKey("ARM_UID")
            .HasConstraintName($"FK_{SqlTables.Pallets}__ARM")
            .IsRequired();

        builder.HasOne(e => e.PalletMan)
            .WithMany()
            .HasForeignKey("PALLET_MAN_UID")
            .HasConstraintName($"FK_{SqlTables.Pallets}__PALLET_MAN")
            .IsRequired();

        #endregion

        builder.Property(e => e.Counter)
            .HasColumnName("COUNTER")
            .IsRequired();

        builder.Property(e => e.Number)
            .HasColumnName("NUMBER")
            .IsRequired();

        builder.Property(e => e.TrayWeight)
            .HasColumnName("WEIGHT_TRAY")
            .IsRequired()
            .HasPrecision(5, 3);

        builder.Property(e => e.Barcode)
            .HasColumnName("BARCODE")
            .HasColumnType("varchar(128)")
            .IsRequired();

        builder.Property(e => e.ProductDt)
            .HasColumnName("PRODUCT_DT")
            .IsRequired();
    }
}