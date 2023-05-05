// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Diagnostics.CodeAnalysis;

namespace WsStorageCore.TableScaleModels.BarCodes;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public enum WsSqlBarcodeType
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