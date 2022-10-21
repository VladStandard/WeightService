// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "Hosts".
/// </summary>
[Serializable]
public class HostModel : SqlTableBase, ICloneable, ISqlDbBase, ISerializable
{
	#region Public and private fields, properties, constructor

	[XmlElement] public virtual DateTime LoginDt { get; set; }
	[XmlElement] public virtual string Name { get; set; }
	[XmlElement] public virtual string HostName { get; set; }
	[XmlElement] public virtual string Ip { get; set; }
	[XmlElement] public virtual SqlFieldMacAddressModel MacAddress { get; set; }

	[XmlElement] public virtual string MacAddressValue
	{
		get => MacAddress.Value;
		set => MacAddress.Value = value;
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	public HostModel() : base(SqlFieldIdentityEnum.Id)
	{
		LoginDt = DateTime.MinValue;
		Name = string.Empty;
		HostName = string.Empty;
		Ip = string.Empty;
		MacAddress = new();
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="info"></param>
	/// <param name="context"></param>
	private HostModel(SerializationInfo info, StreamingContext context) : base(info, context)
	{
		LoginDt = info.GetDateTime(nameof(LoginDt));
		Name = info.GetString(nameof(Name));
		HostName = info.GetString(nameof(HostName));
		Ip = info.GetString(nameof(Ip));
		MacAddress = (SqlFieldMacAddressModel)info.GetValue(nameof(MacAddress), typeof(SqlFieldMacAddressModel));
	}

	#endregion

	#region Public and private methods - override

	public override string ToString() =>
		$"{nameof(IsMarked)}: {IsMarked}. " +
	    $"{nameof(HostName)}: {HostName}. ";

    public override bool Equals(object obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((HostModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();
	
    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault()
    {
        if (!MacAddress.EqualsDefault())
            return false;
        return 
	        base.EqualsDefault() &&
            Equals(LoginDt, DateTime.MinValue) &&
            Equals(Name, string.Empty) &&
            Equals(HostName, string.Empty) &&
            Equals(Ip, string.Empty);
    }

	public override object Clone()
    {
        HostModel item = new();
        item.LoginDt = LoginDt;
        item.Name = Name;
        item.HostName = HostName;
        item.Ip = Ip;
        item.MacAddress = MacAddress.CloneCast();
		item.CloneSetup(base.CloneCast());
		return item;
    }

	/// <summary>
	/// Get object data for serialization info.
	/// </summary>
	/// <param name="info"></param>
	/// <param name="context"></param>
	public override void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		base.GetObjectData(info, context);
		info.AddValue(nameof(LoginDt), LoginDt);
		info.AddValue(nameof(Name), Name);
		info.AddValue(nameof(HostName), HostName);
		info.AddValue(nameof(Ip), Ip);
		info.AddValue(nameof(MacAddress), MacAddress);
	}

	public override void FillProperties()
	{
		base.FillProperties();
		Name = LocaleCore.Sql.SqlItemFieldName;
		Ip = LocaleCore.Sql.SqlItemFieldIp;
		MacAddressValue = LocaleCore.Sql.SqlItemFieldMac;
		HostName = LocaleCore.Sql.SqlItemFieldHostName;
		LoginDt = DateTime.Now;
	}

	#endregion

	#region Public and private methods - virtual

	public virtual bool Equals(HostModel item)
	{
		if (ReferenceEquals(this, item)) return true;
		if (!MacAddress.Equals(item.MacAddress))
			return false;
		return base.Equals(item) &&
		       Equals(LoginDt, item.LoginDt) &&
		       Equals(Name, item.Name) &&
		       Equals(HostName, item.HostName) &&
		       Equals(Ip, item.Ip);
	}

	public new virtual HostModel CloneCast() => (HostModel)Clone();

	#endregion
}
