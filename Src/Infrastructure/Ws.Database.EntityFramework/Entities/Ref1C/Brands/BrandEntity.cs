namespace Ws.Database.EntityFramework.Entities.Ref1C.Brands;

public sealed class BrandEntity : EfEntityBase
{

    public BrandEntity() { }

    public BrandEntity(Guid uid, string name, DateTime updateDate)
    {
        Id = uid;
        Name = name;
        ChangeDt = updateDate;
    }

    [Column(SqlColumns.Name), StringLength(32)]
    public string Name { get; set; } = string.Empty;

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion
}