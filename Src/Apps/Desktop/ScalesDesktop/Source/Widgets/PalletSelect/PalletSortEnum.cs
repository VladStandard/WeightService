using System.ComponentModel;

namespace ScalesDesktop.Source.Widgets.PalletSelect;

public enum PalletSortEnum
{
    [Description("PalletSortDate")]
    Date,
    [Description("PalletSortNumber")]
    Number,
    [Description("PalletSortPlu")]
    Plu,
    [Description("PalletSortLabelsCount")]
    LabelsCount
}