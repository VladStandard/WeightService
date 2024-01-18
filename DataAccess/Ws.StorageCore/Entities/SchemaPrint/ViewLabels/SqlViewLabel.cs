namespace Ws.StorageCore.Entities.SchemaPrint.ViewLabels;

public class SqlViewLabel : SqlEntityBase
{
    public virtual DateTime ProductDate { get; set; }
    public virtual bool IsCheckWeight { get; set; }
    public virtual short PluNumber { get; set; }
    
    public virtual string LineName { get; set; } = string.Empty;
    public virtual string PluName { get; set; } = string.Empty;

    public virtual string Warehouse { get; set; } = string.Empty;
    public virtual string BarcodeTop { get; set; } = string.Empty;
    public virtual string BarcodeBottom { get; set; } = string.Empty;
    public virtual string BarcodeRight { get; set; } = string.Empty;

    public SqlViewLabel() : base(SqlEnumFieldIdentity.Uid)
    {
        
    }
}