using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ws.Database.EntityFramework.Entities.Ref.Printers;

public class PrinterMapConfig : IEntityTypeConfiguration<PrinterEntity>
{
    public void Configure(EntityTypeBuilder<PrinterEntity> builder)
    {
        #region Base

        builder.ToTable(SqlTables.Printers, SqlSchemas.Ref);

        builder.HasIndex(e => e.Name)
            .HasDatabaseName($"UQ_{SqlTables.Printers}__NAME")
            .IsUnique();

        builder.HasIndex(e => e.Ip)
            .HasDatabaseName($"UQ_{SqlTables.PalletMen}__IP")
            .IsUnique();

        #endregion

        #region FK

        builder.HasOne(e => e.ProductionSite)
            .WithMany()
            .HasForeignKey("PRODUCTION_SITE_UID")
            .HasConstraintName($"FK_{SqlTables.PalletMen}__PRODUCTION_SITE")
            .IsRequired();

        #endregion

        builder.Property(e => e.Name)
            .HasColumnName(SqlColumns.Name)
            .HasColumnType("varchar(16)")
            .IsRequired();

        builder.Property(e => e.Ip)
            .HasColumnName("IP")
            .IsRequired();

        builder.Property(e => e.Type)
            .HasColumnName("TYPE")
            .IsRequired();
    }
}