namespace Ws.Database.EntityFramework.Entities.Zpl.ZplResources;

[Table(SqlTables.ZplResources, Schema = SqlSchemas.Zpl)]
[Index(nameof(Name), Name = $"UQ_{SqlTables.ZplResources}_NAME", IsUnique = true)]
public sealed class ZplResourceEntity : EfEntityBase
{
    [Column(SqlColumns.Name)]
    [StringLength(64)]
    public string Name { get; private set; } = string.Empty;

    [Column("ZPL")]
    [StringLength(2048)]
    public string Zpl { get; private set; } = string.Empty;

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion
}