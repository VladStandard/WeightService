namespace Ws.Barcodes.Models;

public sealed record BarcodeVarInfo(Type Type, string Property, string Mask, bool IsRepeatable);