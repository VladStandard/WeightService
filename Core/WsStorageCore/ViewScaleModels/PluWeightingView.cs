// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.ViewScaleModels;
public class PluWeightingView: WsSqlTableBase
{
    #region Public and private fields, properties, constructor
    
    public virtual int PluNumber { get; set; }
    public virtual string PluName { get; set; }
    public virtual string Line { get; set; }
    public virtual decimal TareWeight { get; set; }
    public virtual decimal NettoWeight { get; set; }
    
    /// <summary>
	/// Constructor.
	/// </summary>
	public PluWeightingView() : base(WsSqlFieldIdentity.Uid)
	{
        PluNumber = 0;
        PluName = string.Empty;
        Line = string.Empty;
        TareWeight = 0;
        NettoWeight = 0;
    }

	#endregion

	#region Public and private methods - override

	public override string ToString() =>
        $"{nameof(PluNumber)}: {PluNumber}. " +
		$"{nameof(PluName)}: {PluName}. " +
		$"{nameof(Line)}: {Line}. " +
        $"{nameof(NettoWeight)}: {NettoWeight}.";
    

	#endregion

}