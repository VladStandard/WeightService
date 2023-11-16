using System.Diagnostics.CodeAnalysis;

namespace Ws.StorageCore.Entities.SchemaScale.BarCodes;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public enum SqlBarcodeType
{
    Default,
    Codabar,
    Code11,
    Code128,
    Code128A,
    Code128B,
    Code128C,
    Code39,
    Code39E,
    Code93,
    EAN13,
    EAN8
}