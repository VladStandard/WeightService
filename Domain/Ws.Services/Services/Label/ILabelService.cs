using Ws.StorageCore.Entities.SchemaPrint.Labels;

namespace Ws.Services.Services.Label;

public interface ILabelService
{
    SqlLabelEntity GetLabelByBarcodeTop(string barcodeTop);
    SqlLabelEntity GetLabelByBarcodeRight(string barcodeRight);
    SqlLabelEntity GetLabelByBarcodeBottom(string barcodeBottom);
}