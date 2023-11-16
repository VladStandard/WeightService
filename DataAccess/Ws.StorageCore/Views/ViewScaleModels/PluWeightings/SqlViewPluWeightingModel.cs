namespace Ws.StorageCore.Views.ViewScaleModels.PluWeightings;

[DebuggerDisplay("{ToString()}")]
public class SqlViewPluWeightingModel: SqlEntityBase
{
    #region Public and private fields, properties, constructor
    
    public virtual int PluNumber { get; set; }
    public virtual string PluName { get; set; }
    public virtual string Line { get; set; }
    public virtual decimal TareWeight { get; set; }
    public virtual decimal NettoWeight { get; set; }
    
	public SqlViewPluWeightingModel() : base(SqlEnumFieldIdentity.Uid)
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