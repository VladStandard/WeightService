using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ws.Database.EntityFramework.Entities.Print.Pallets;

namespace Ws.Database.EntityFramework.Entities.Print.Labels;

internal class LabelMapConfig : IEntityTypeConfiguration<LabelEntity>
{
    public void Configure(EntityTypeBuilder<LabelEntity> builder)
    {
        #region Base

        builder.ToTable(SqlTables.Labels, SqlSchemas.Print);

        builder.HasIndex(e => e.BarcodeTop)
            .HasDatabaseName($"UQ_{SqlTables.Plus}__BARCODE_TOP")
            .IsUnique();

        #endregion

        #region FK

        builder.Property(e => e.PalletEntityId)
            .HasColumnName("PALLET_UID");

        builder.HasOne<PalletEntity>()
            .WithMany()
            .HasForeignKey(e => e.PalletEntityId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName($"FK_{SqlTables.Plus}__PALLET")
            .IsRequired(false);

        //

        const string pluId = "PluUid";

        builder.Property<Guid?>(pluId)
            .HasColumnName("PLU_UID");

        builder.HasOne(e => e.Plu)
            .WithMany()
            .HasForeignKey(pluId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName($"FK_{SqlTables.Plus}__PLU")
            .IsRequired(false);

        //

        const string armId = "ArmUid";

        builder.Property<Guid>(armId)
            .HasColumnName("ARM_UID");

        builder.HasOne(e => e.Line)
            .WithMany()
            .HasForeignKey(armId)
            .HasConstraintName($"FK_{SqlTables.Plus}__ARM")
            .IsRequired();

        #endregion

        #region Dates

        builder.Property(e => e.ProductDt)
            .HasColumnName("PRODUCT_DT")
            .IsRequired();

        builder.Property(e => e.ExpirationDt)
            .HasColumnName("EXPIRATION_DT")
            .IsRequired();

        #endregion

        #region Barcodes

        builder.Property(e => e.BarcodeTop)
            .HasColumnName("BARCODE_TOP")
            .HasColumnType("varchar(128)")
            .IsRequired();

        builder.Property(e => e.BarcodeRight)
            .HasColumnName("BARCODE_RIGHT")
            .HasColumnType("varchar(128)")
            .IsRequired();

        builder.Property(e => e.BarcodeBottom)
            .HasColumnName("BARCODE_BOTTOM")
            .HasColumnType("varchar(128)")
            .IsRequired();

        #endregion

        #region Weight

        builder.Property(e => e.WeightNet)
            .HasColumnName("WEIGHT_NET")
            .IsRequired()
            .HasPrecision(5, 3);

        builder.Property(e => e.WeightTare)
            .HasColumnName("WEIGHT_TARE")
            .IsRequired()
            .HasPrecision(5, 3);

        #endregion

        #region Other

        builder.Property(e => e.BundleCount)
            .HasColumnName("BUNDLE_COUNT")
            .IsRequired();

        builder.Property(e => e.Zpl)
            .HasColumnName("ZPL")
            .IsUnicode(false)
            .IsRequired();

        builder.Property(e => e.Kneading)
            .HasColumnName("KNEADING")
            .IsRequired();

        #endregion
    }
}