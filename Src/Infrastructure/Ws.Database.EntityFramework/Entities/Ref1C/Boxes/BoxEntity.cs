namespace Ws.Database.EntityFramework.Entities.Ref1C.Boxes;

public sealed class BoxEntity : EfEntityBase
{
    public BoxEntity() { }

    public BoxEntity(Guid uid, string name, decimal weight, DateTime updateDate)
    {
        Id = uid;
        Name = name;
        Weight = weight;
        ChangeDt = updateDate;
    }

    public string Name { get; set; } = string.Empty;
    public decimal Weight { get; set; }

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion
}