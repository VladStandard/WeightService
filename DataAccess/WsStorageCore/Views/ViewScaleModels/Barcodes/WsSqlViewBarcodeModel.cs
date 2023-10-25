namespace WsStorageCore.Views.ViewScaleModels.Barcodes;

[DebuggerDisplay("{ToString()}")]
public class WsSqlViewBarcodeModel: WsSqlEntityBase
{
    #region Public and private fields, properties, constructor
    
    public virtual int PluNumber { get; init; }
    public virtual string ValueTop { get; init; }
    public virtual string ValueRight { get; init; }
    public virtual string ValueBottom { get; init; }
    
	public WsSqlViewBarcodeModel() : base(WsSqlEnumFieldIdentity.Uid)
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