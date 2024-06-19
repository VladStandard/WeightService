using Ws.Database.EntityFramework.Entities.Ref.Printers;
using Ws.Database.EntityFramework.Entities.Ref.Warehouses;
using Ws.Database.EntityFramework.Entities.Ref1C.Plus;
using Ws.Shared.Enums;

namespace Ws.Database.EntityFramework.Entities.Ref.Lines;

public sealed class LineEntity : EfEntityBase
{
    public WarehouseEntity Warehouse { get; set; } = new();
    public PrinterEntity Printer { get; set; } = new();
    public ICollection<PluEntity> Plus { get; set; } = [];

    public string Name { get; set; } = string.Empty;
    public int Counter { get; set; }
    public int Number { get; set; }
    public string PcName { get; set; } = string.Empty;
    public ArmType Type { get; set; } = ArmType.Tablet;
    public string Version { get; set; } = string.Empty;

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion
}