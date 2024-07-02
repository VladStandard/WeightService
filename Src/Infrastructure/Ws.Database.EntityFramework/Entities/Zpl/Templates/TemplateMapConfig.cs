using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ws.Database.EntityFramework.Entities.Zpl.Templates;

internal sealed class TemplateMapConfig : IEntityTypeConfiguration<TemplateEntity>
{
    public void Configure(EntityTypeBuilder<TemplateEntity> builder)
    {
        #region Base

        builder.ToTable(SqlTables.Templates, SqlSchemas.Zpl);

        builder.HasIndex(e => new { e.Name, e.IsWeight })
            .HasDatabaseName($"UQ_{SqlTables.Templates}__NAME__IS_WEIGHT")
            .IsUnique();

        #endregion

        builder.Property(e => e.Name)
            .HasColumnName(SqlColumns.Name)
            .HasColumnType("varchar(64)")
            .IsRequired();

        builder.Property(e => e.IsWeight)
            .HasColumnName("IS_WEIGHT")
            .IsRequired();

        builder.Property(e => e.Body)
            .HasColumnName("BODY")
            .IsRequired();

        builder.Property(e => e.Rotate)
            .HasColumnName("ROTATE")
            .IsRequired();

        builder.Property(e => e.Width)
            .HasColumnName("WIDTH")
            .IsRequired();

        builder.Property(e => e.Height)
            .HasColumnName("HEIGHT")
            .IsRequired();

        builder.Property(e => e.BarcodeTopBody)
            .HasColumnName("BARCODE_TOP_BODY")
            .HasColumnType("varchar(2048)")
            .IsRequired();

        builder.Property(e => e.BarcodeBottomBody)
            .HasColumnName("BARCODE_BOTTOM_BODY")
            .HasColumnType("varchar(2048)")
            .IsRequired();

        builder.Property(e => e.BarcodeRightBody)
            .HasColumnName("BARCODE_RIGHT_BODY")
            .HasColumnType("varchar(2048)")
            .IsRequired();
    }
}