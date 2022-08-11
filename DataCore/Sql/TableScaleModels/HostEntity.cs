// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "Hosts".
/// </summary>
[Serializable]
public class HostEntity : BaseEntity, ISerializable, IBaseEntity
{
	#region Public and private fields, properties, constructor

	/// <summary>
	/// Identity name.
	/// </summary>
	[XmlElement] public static ColumnName IdentityName => ColumnName.Id;
	[XmlElement] public virtual DateTime AccessDt { get; set; }
	[XmlElement] public virtual string Name { get; set; }
	[XmlElement] public virtual string HostName { get; set; }
	[XmlElement] public virtual string Ip { get; set; }
	[XmlElement] public virtual MacAddressEntity MacAddress { get; set; }

	[XmlElement] public virtual string MacAddressValue
	{
		get => MacAddress.Value;
		set => MacAddress.Value = value;
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	public HostEntity() : base(0, false)
	{
		Init();
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="identityId"></param>
	/// <param name="isSetupDates"></param>
	public HostEntity(long identityId, bool isSetupDates) : base(0, isSetupDates)
    {
		Init();
	}

    #endregion

    #region Public and private methods

    public new virtual void Init()
    {
	    base.Init();
		AccessDt = DateTime.MinValue;
        Name = string.Empty;
        HostName = string.Empty;
        Ip = string.Empty;
		MacAddress = new();
	}

    public override string ToString()
    {
        return
	        $"{nameof(IdentityId)}: {IdentityId}. " +
	        $"{nameof(IsMarked)}: {IsMarked}. " +
			$"{nameof(HostName)}: {HostName}. ";
    }

    public virtual bool Equals(HostEntity item)
    {
        if (item is null) return false;
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
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((HostEntity)obj);
    }

	public override int GetHashCode() => IdentityId.GetHashCode();

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
        HostEntity item = new();
        item.AccessDt = AccessDt;
        item.Name = Name;
        item.HostName = HostName;
        item.Ip = Ip;
        item.MacAddress = MacAddress.CloneCast();
        item.Setup(((BaseEntity)this).CloneCast());
        return item;
    }

    public new virtual HostEntity CloneCast() => (HostEntity)Clone();

    #endregion
}
