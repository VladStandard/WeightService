using System.Net;
using TscZebra.Plugin.Abstractions.Enums;
using Ws.Database.EntityFramework.Entities.Ref.ProductionSites;
using Ws.Shared.Constants;

namespace Ws.Database.EntityFramework.Entities.Ref.Printers;

public sealed class PrinterEntity : EfEntityBase
{
    public string Name { get; set; } = string.Empty;
    public PrinterTypes Type { get; set; } = PrinterTypes.Tsc;
    public IPAddress Ip { get; set; } = DefaultConsts.IpLocal;

    #region Fk

    public Guid ProductionSiteId { get; set; }
    public ProductionSiteEntity ProductionSite { get; set; } = new();

    #endregion

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion
}