namespace ScalesDesktop.Source.Widgets.PalletSelect;

public enum PalletFilterType
{
    [Description("PalletFilterNotShipped")]
    NotShipped,
    [Description("PalletFilterShipped")]
    Shipped,
    [Description("PalletFilterDeleted")]
    Deleted,
    [Description("PalletFilterAll")]
    All
}