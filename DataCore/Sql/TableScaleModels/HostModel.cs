// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "Hosts".
/// </summary>
[Serializable]
public class HostModel : TableModel, ISerializable, ITableModel
{
	#region Public and private fields, properties, constructor

	[XmlElement] public virtual DateTime AccessDt { get; set; }
	[XmlElement] public virtual string Name { get; set; }
	[XmlElement] public virtual string HostName { get; set; }
	[XmlElement] public virtual string Ip { get; set; }
	[XmlElement] public virtual FieldMacAddressModel MacAddress { get; set; }

	[XmlElement] public virtual string MacAddressValue
	{
		get => MacAddress.Value;
		set => MacAddress.Value = value;
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	public HostModel() : base(ColumnName.Id)
	{
		AccessDt = DateTime.MinValue;
		Name = string.Empty;
		HostName = string.Empty;
		Ip = string.Empty;
		MacAddress = new();
	}

    #endregion

    #region Public and private methods

    public override string ToString()
    {
        return
	        $"{nameof(IsMarked)}: {IsMarked}. " +
			$"{nameof(HostName)}: {HostName}. ";
    }

    public virtual bool Equals(HostModel item)
    {
        if (ReferenceEquals(this, item)) return true;
        if (!MacAddress.Equals(item.MacAddress))
            return false;
        return base.Equals(item) &&
               Equals(AccessDt, item.AccessDt) &&
               Equals(Name, item.Name) &&
               Equals(HostName, item.HostName) &&
               Equals(Ip, item.Ip);
    }

    public override bool Equals(object obj)
    {
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((HostModel)obj);
    }

	public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        if (!MacAddress.EqualsDefault())
            return false;
        return base.EqualsDefault() &&
               Equals(AccessDt, DateTime.MinValue) &&
               Equals(Name, string.Empty) &&
               Equals(HostName, string.Empty) &&
               Equals(Ip, string.Empty);
    }

    public new virtual object Clone()
    {
        HostModel item = new();
        item.AccessDt = AccessDt;
        item.Name = Name;
        item.HostName = HostName;
        item.Ip = Ip;
        item.MacAddress = MacAddress.CloneCast();
        item.Setup(((TableModel)this).CloneCast());
        return item;
    }

    public new virtual HostModel CloneCast() => (HostModel)Clone();

    #endregion
}
