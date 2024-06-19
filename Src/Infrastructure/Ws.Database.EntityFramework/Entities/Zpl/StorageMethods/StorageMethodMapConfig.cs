using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ws.Database.EntityFramework.Entities.Zpl.StorageMethods;

public class StorageMethodMapConfig : IEntityTypeConfiguration<StorageMethodEntity>
{
    public void Configure(EntityTypeBuilder<StorageMethodEntity> builder)
    {
        #region Base

        builder.ToTable(SqlTables.StorageMethods, SqlSchemas.Zpl);

        builder.HasIndex(e => e.Name)
            .HasDatabaseName($"UQ_{SqlTables.StorageMethods}__NAME")
            .IsUnique();

        #endregion

        builder.Property(e => e.Name)
            .HasColumnName(SqlColumns.Name)
            .HasColumnType("varchar(32)")
            .IsRequired();

        builder.Property(e => e.Zpl)
            .HasColumnName("ZPL")
            .HasColumnType("varchar(1024)")
            .IsRequired();
    }
}