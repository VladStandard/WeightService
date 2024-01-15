using Ws.StorageCore.Entities.SchemaPrint.Labels;
using Ws.StorageCore.Models;
using Ws.StorageCore.OrmUtils;

namespace Ws.Services.Services.Label;

public class LabelService : ILabelService
{
    public SqlLabelEntity GetLabelByBarcodeTop(string barcodeTop)
    {
        SqlCrudConfigModel crud = new();
        crud.AddFilter(SqlRestrictions.Equal(nameof(SqlLabelEntity.BarcodeTop), barcodeTop));
        return new SqlLabelRepository().GetItem(crud);
    }
    
    public SqlLabelEntity GetLabelByBarcodeRight(string barcodeRight)
    {
        SqlCrudConfigModel crud = new();
        crud.AddFilter(SqlRestrictions.Equal(nameof(SqlLabelEntity.BarcodeRight), barcodeRight));
        return new SqlLabelRepository().GetItem(crud);
    }
    
    public SqlLabelEntity GetLabelByBarcodeBottom(string barcodeBottom)
    {
        SqlCrudConfigModel crud = new();
        crud.AddFilter(SqlRestrictions.Equal(nameof(SqlLabelEntity.BarcodeBottom), barcodeBottom));
        return new SqlLabelRepository().GetItem(crud);
    }
}