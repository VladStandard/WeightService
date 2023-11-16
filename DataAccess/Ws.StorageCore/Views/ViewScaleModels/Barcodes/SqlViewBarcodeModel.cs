using Ws.StorageCore.Common;
namespace Ws.StorageCore.Views.ViewScaleModels.Barcodes;

[DebuggerDisplay("{ToString()}")]
public class SqlViewBarcodeModel: SqlEntityBase
{
    #region Public and private fields, properties, constructor
    
    public virtual int PluNumber { get; init; }
    public virtual string ValueTop { get; init; }
    public virtual string ValueRight { get; init; }
    public virtual string ValueBottom { get; init; }
    
	public SqlViewBarcodeModel() : base(SqlEnumFieldIdentity.Uid)
	{
        PluNumber = 0;
        ValueTop = string.Empty;
        ValueRight = string.Empty;
        ValueBottom = string.Empty;
    }

	#endregion

	#region Public and private methods - override

	public override string ToString() => $"{PluNumber} | {ValueTop} | {ValueRight} | {ValueBottom}";

    #endregion
}