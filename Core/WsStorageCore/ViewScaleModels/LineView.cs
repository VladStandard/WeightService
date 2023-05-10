// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.ViewScaleModels;

[DebuggerDisplay("{ToString()}")]
public class LineView : WsSqlTableBase
{
    #region Public and private fields, properties, constructor
    public virtual int Number { get; set; }
    public virtual string HostName { get; set; }
    public virtual string Printer { get; set; }
    public virtual string WorkShop { get; set; }
    
    /// <summary>
	/// Constructor.
	/// </summary>
	public LineView() : base(WsSqlFieldIdentity.Id)
	{
        Number = 0;
        HostName = string.Empty;
        Printer = string.Empty;
        WorkShop = string.Empty;
    }

	#endregion

	#region Public and private methods - override

	public override string ToString() =>
		$"{nameof(Name)}: {Name}. " +
		$"{nameof(Number)}: {Number}. " +
		$"{nameof(HostName)}: {HostName}. " +
		$"{nameof(Printer)}: {Printer}. " +
		$"{nameof(WorkShop)}: {WorkShop}.";
    
	#endregion
    
}