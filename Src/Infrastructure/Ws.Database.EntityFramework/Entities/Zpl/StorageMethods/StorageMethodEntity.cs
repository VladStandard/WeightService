namespace Ws.Database.EntityFramework.Entities.Zpl.StorageMethods;

public sealed class StorageMethodEntity : EfEntityBase
{
    public string Name { get; set; } = string.Empty;
    public string Zpl { get; set; } = string.Empty;

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion
}