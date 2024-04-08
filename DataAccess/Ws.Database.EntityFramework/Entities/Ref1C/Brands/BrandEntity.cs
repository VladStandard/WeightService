namespace Ws.Database.EntityFramework.Entities.Ref1C.Brands;

[Table(SqlTables.Brands, Schema = SqlSchemas.Ref1C)]
[Index(nameof(Name), Name = $"UQ_{SqlTables.Brands}_NAME", IsUnique = true)]
public sealed class BrandEntity : EfEntityBase
{
    [Column(SqlColumns.Name)]
    [StringLength(32, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 32 characters")]
    public string Name { get; set; } = string.Empty;

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion

    public BrandEntity() { }

    public BrandEntity(Guid uid, string name, DateTime updateDate)
    {
        Id = uid;
        Name = name;
        ChangeDt = updateDate;
    }
    // public virtual ICollection<PluEntity> Plus { get; set; } = new List<PluEntity>();
}