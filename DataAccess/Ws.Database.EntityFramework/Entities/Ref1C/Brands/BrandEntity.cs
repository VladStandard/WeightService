namespace Ws.Database.EntityFramework.Entities.Ref1C.Brands;

[Table(SqlTables.Brands)]
[Index(nameof(Name), Name = $"UQ_{SqlTables.Brands}_NAME", IsUnique = true)]
[Index(nameof(Uid1C), Name = $"UQ_{SqlTables.Brands}_UID_1C", IsUnique = true)]
public sealed class BrandEntity : EfEntityBase
{
    [Column(SqlColumns.Name)]
    [StringLength(32, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 32 characters")]
    public string Name { get; set; } = string.Empty;

    [Column("UID_1C")]
    public Guid Uid1C { get; set; }
    
    #region Date
    
    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion
    
    // public virtual ICollection<PluEntity> Plus { get; set; } = new List<PluEntity>();
}
