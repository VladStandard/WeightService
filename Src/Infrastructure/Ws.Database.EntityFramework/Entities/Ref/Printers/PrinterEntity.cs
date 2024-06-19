using System.Net;
using TscZebra.Plugin.Abstractions.Enums;
using Ws.Database.EntityFramework.Entities.Ref.ProductionSites;

namespace Ws.Database.EntityFramework.Entities.Ref.Printers;

public sealed class PrinterEntity : EfEntityBase
{
    public string Name { get; set; } = string.Empty;
    public IPAddress Ip { get; set; } = IPAddress.Parse("127.0.0.1");
    public PrinterTypes Type { get; set; } = PrinterTypes.Tsc;
    public ProductionSiteEntity ProductionSite { get; set; } = new();

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion
}