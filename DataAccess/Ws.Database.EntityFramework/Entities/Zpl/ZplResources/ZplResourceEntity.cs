namespace Ws.Database.EntityFramework.Entities.Zpl.ZplResources;

[Table(SqlTables.ZplResources, Schema = SqlSchemas.Zpl)]
[Index(nameof(Name), Name = $"UQ_{SqlTables.ZplResources}_NAME", IsUnique = true)]
public sealed class ZplResourceEntity : EfEntityBase
{
    [Column(SqlColumns.Name)]
    [StringLength(64, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 64 characters")]
    public string Name { get; private set; } = string.Empty;

    [Column("ZPL")]
    [StringLength(2048, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 2048 characters")]
    public string Zpl { get; private set; } = string.Empty;

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion
}