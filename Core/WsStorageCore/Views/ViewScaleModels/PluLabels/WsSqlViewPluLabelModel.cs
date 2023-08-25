// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Views.ViewScaleModels.PluLabels;

[DebuggerDisplay("{ToString()}")]
public class WsSqlViewPluLabelModel: WsSqlTableBase
{
    #region Public and private fields, properties, constructor
    
    public DateTime ExpirationDate { get; set; }
    public DateTime ProductDate { get; set; }
    public DateTime WeightingDate { get; set; }
    public virtual int PluNumber { get; set; }
    public virtual string Line { get; set; }
    public virtual string PluName { get; set; }
    public virtual string WorkShop { get; set; }
    public string PluType => WeightingDate == DateTime.MinValue ? "Штучная" : "Весовая";
    
	public WsSqlViewPluLabelModel() : base(WsSqlEnumFieldIdentity.Uid)
	{
        PluNumber = 0;
        ExpirationDate = new();
        ProductDate = new();
        WeightingDate = new();
        Line = string.Empty;
        PluName = string.Empty;
        WorkShop = string.Empty;
    }

	#endregion

	#region Public and private methods - override

	public override string ToString() =>
        $"{nameof(PluNumber)}: {PluNumber}. " +
		$"{nameof(Line)}: {Line}. " +
		$"{nameof(PluName)}: {PluName}. " +
		$"{nameof(WorkShop)}: {WorkShop}.";

    #endregion
}