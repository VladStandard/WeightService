namespace Ws.Database.EntityFramework.Entities.Zpl.StorageMethods;

[Table(SqlTables.StorageMethods, Schema = SqlSchemas.Zpl)]
[Index(nameof(Name), Name = $"UQ_{SqlTables.StorageMethods}_NAME", IsUnique = true)]
[Index(nameof(Zpl), Name = $"UQ_{SqlTables.StorageMethods}_ZPL", IsUnique = true)]
public sealed class StorageMethodEntity : EfEntityBase
{
    [Column(SqlColumns.Name)]
    [StringLength(32, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 32 characters")]
    public string Name { get; set; } = string.Empty;

    [Column("ZPL")]
    [StringLength(1024, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 1024 characters")]
    public string Zpl { get; set; } = string.Empty;

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion
    // public virtual ICollection<PluEntity> Plus { get; set; } = new List<PluEntity>();
}