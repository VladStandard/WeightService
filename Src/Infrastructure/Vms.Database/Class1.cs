using Ws.Shared.Constants;
using Ws.Shared.ValueTypes;

namespace Vms.Database;

public class ZplPrinter;
public class Warehouse;

public class Tablet
{
    public string AndroidId { get; set; } = string.Empty;
    public ZplPrinter ZplPrinter { get; set; } = new();
}

public class Tsd
{
    public string AndroidId { get; set; } = string.Empty;
}

public class Plu {
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
}

public class User
{
    public Fio Fio { get; set; } = DefaultTypes.Fio;
    public string Password { get; set; } = string.Empty;
    public Warehouse Warehouse { get; set; } = new();
}

public class Document
{
    public User User { get; set; } = new();
    public Warehouse Warehouse { get; set; } = new();
}

public class Pallet {
    public string Barcode { get; set; } = string.Empty;
    public Warehouse Warehouse { get; set; } = new();
}

public class Part {
    public Guid PluId { get; set; }
    public Guid PalletId { get; set; }
    public DateOnly Date { get; set; }
    public decimal Weight { get; set; }
}