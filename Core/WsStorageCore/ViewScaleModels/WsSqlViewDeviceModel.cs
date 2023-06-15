// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.ViewScaleModels;

[DebuggerDisplay("{ToString()}")]
public class WsSqlViewDeviceModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor
    
    public DateTime LoginDate { get; set; }
    public DateTime LogoutDate { get; set; }
    public virtual string TypeName { get; set; }
    public virtual string Ip { get; set; }
    public virtual string Mac { get; set; }
    private string GetValueAsString(char ch)
    {
        if (string.IsNullOrEmpty(Mac))
            return string.Empty;
        return Mac.Length switch
        {
            12 => $"{Mac[0]}{Mac[1]}{ch}{Mac[2]}{Mac[3]}{ch}{Mac[4]}{Mac[5]}{ch}" +
                  $"{Mac[6]}{Mac[7]}{ch}{Mac[8]}{Mac[9]}{ch}{Mac[10]}{Mac[11]}",
            17 => $"{Mac[0]}{Mac[1]}{ch}{Mac[3]}{Mac[4]}{ch}{Mac[6]}{Mac[7]}{ch}" +
                  $"{Mac[9]}{Mac[10]}{ch}{Mac[12]}{Mac[13]}{ch}{Mac[15]}{Mac[16]}",
            _ => Mac
        };
    }
    
    [XmlIgnore] public string ValuePrettyLookMinus => GetValueAsString('-');
    
    /// <summary>
	/// Constructor.
	/// </summary>
	public WsSqlViewDeviceModel() : base(WsSqlEnumFieldIdentity.Uid)
	{
        LoginDate = new();
        LogoutDate = new();
        TypeName = string.Empty;
        Ip = string.Empty;
        Mac = string.Empty;
    }

	#endregion

	#region Public and private methods - override

	public override string ToString() =>
        $"{nameof(TypeName)}: {TypeName}. " +
		$"{nameof(Ip)}: {Ip}. " +
		$"{nameof(Mac)}: {Mac}.";

    #endregion
}