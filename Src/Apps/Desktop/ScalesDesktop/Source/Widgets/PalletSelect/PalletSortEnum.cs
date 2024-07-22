using System.ComponentModel;

namespace ScalesDesktop.Source.Widgets.PalletSelect;

public enum PalletFilterEnum
{
    [Description("PalletFilterNoFilter")]
    NoFilter,
    [Description("PalletFilterShipped")]
    Shipped,
    [Description("PalletFilterDeleted")]
    Deleted
}