using System.Diagnostics.CodeAnalysis;
using Ws.Labels.Service.Generate.Common;
using Ws.Shared.Extensions;

namespace Ws.Labels.Service.Generate.Models.Variables;

/// <summary>
/// DON'T TOUCH (THIS IS VARIABLES FOR TEMPLATES) BE CAREFUL
/// </summary>
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class TemplateVars(
    PluVars plu,
    ArmVars arm,
    PalletVars pallet,
    BarcodeReadyModel barcodeTop,
    BarcodeReadyModel barcodeRight,
    BarcodeReadyModel barcodeBottom,
    ushort bundleCount,
    ushort kneading,
    decimal weightNet,
    decimal weightGross,
    DateTime productDt,
    DateTime expirationDt)
{

    #region Barcodes

    public readonly BarcodeReadyModel BarcodeTop = barcodeTop;
    public readonly BarcodeReadyModel BarcodeRight = barcodeRight;
    public readonly BarcodeReadyModel BarcodeBottom = barcodeBottom;

    #endregion

    #region Vars

    public PalletVars Pallet = pallet;
    public readonly PluVars Plu = plu;
    public readonly ArmVars Arm = arm;

    #endregion


    #region Other

    public readonly string WeightNet =  weightNet.ToSepStr(",");
    public readonly string WeightGross =  weightGross.ToSepStr(",");

    public readonly ushort BundleCount = bundleCount;
    public readonly string Kneading = $"{kneading:D3}";

    public readonly string ProductDate = $"{productDt:dd.MM.yyyy}";
    public readonly string ExpirationDate = $"{expirationDt:dd.MM.yyyy}";

    #endregion
};