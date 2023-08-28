namespace WsStorageCore.Views.ViewScaleModels.Lines;

[DebuggerDisplay("{ToString()}")]
public class WsSqlViewLineModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor
    public virtual int Number { get; set; }
    public virtual string HostName { get; set; }
    public virtual string Printer { get; set; }
    public virtual string WorkShop { get; set; }
    public virtual int Counter { get; set; }
    
	public WsSqlViewLineModel() : base(WsSqlEnumFieldIdentity.Id)
	{
        Number = 0;
        HostName = string.Empty;
        Printer = string.Empty;
        WorkShop = string.Empty;
        Counter = 0;
    }

	#endregion

	#region Public and private methods - override

	public override string ToString() =>
		$"{nameof(Name)}: {Name}. " +
		$"{nameof(Number)}: {Number}. " +
		$"{nameof(HostName)}: {HostName}. " +
		$"{nameof(Printer)}: {Printer}. " +
        $"{nameof(Counter)}: {Counter}. " +
		$"{nameof(WorkShop)}: {WorkShop}.";
    
	#endregion
    
}