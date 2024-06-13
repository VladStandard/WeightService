namespace Ws.Database.EntityFramework.Entities.Zpl.StorageMethods;

[Table(SqlTables.StorageMethods, Schema = SqlSchemas.Zpl),
 Index(nameof(Name), Name = $"UQ_{SqlTables.StorageMethods}_NAME", IsUnique = true),
 Index(nameof(Zpl), Name = $"UQ_{SqlTables.StorageMethods}_ZPL", IsUnique = true)]
public sealed class StorageMethodEntity : EfEntityBase
{
    [Column(SqlColumns.Name), StringLength(32)]
    public string Name { get; set; } = string.Empty;

    [Column("ZPL"), StringLength(1024)]
    public string Zpl { get; set; } = string.Empty;

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion
}