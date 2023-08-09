// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Views.ViewScaleModels.Barcodes;

[DebuggerDisplay("{ToString()}")]
public class WsSqlViewBarcodeModel: WsSqlTableBase
{
    #region Public and private fields, properties, constructor
    
    public virtual int PluNumber { get; init; }
    public virtual string ValueTop { get; init; }
    public virtual string ValueRight { get; init; }
    public virtual string ValueBottom { get; init; }
    
    /// <summary>
	/// Constructor.
	/// </summary>
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