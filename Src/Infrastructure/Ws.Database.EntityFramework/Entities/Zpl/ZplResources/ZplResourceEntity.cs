using Ws.Shared.Enums;

namespace Ws.Database.EntityFramework.Entities.Zpl.ZplResources;

public sealed class ZplResourceEntity : EfEntityBase
{
    public string Name { get; private set; } = string.Empty;
    public string Zpl { get; private set; } = string.Empty;
    public ZplResourceType Type { get; set; } = ZplResourceType.Text;

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion
}