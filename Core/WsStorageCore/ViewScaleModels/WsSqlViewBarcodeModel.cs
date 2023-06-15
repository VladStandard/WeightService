// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.ViewScaleModels;

[DebuggerDisplay("{ToString()}")]
public class WsSqlViewBarcodeModel: WsSqlTableBase
{
    #region Public and private fields, properties, constructor
    
    public virtual int PluNumber { get; set; }
    public virtual string ValueTop { get; set; }
    public virtual string ValueRight { get; set; }
    public virtual string ValueBottom { get; set; }
    
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

	public override string ToString() =>
        $"{nameof(PluNumber)}: {PluNumber}. " +
		$"{nameof(ValueTop)}: {ValueTop}. " +
		$"{nameof(ValueRight)}: {ValueRight}. " +
		$"{nameof(ValueBottom)}: {ValueBottom}.";

    #endregion
}