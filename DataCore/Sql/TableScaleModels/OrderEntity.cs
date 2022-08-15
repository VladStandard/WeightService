// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "ORDERS".
/// </summary>
[Serializable]
public class OrderEntity : BaseEntity, ISerializable, IBaseEntity
{
	#region Public and private fields, properties, constructor

	/// <summary>
	/// Identity name.
	/// </summary>
	[XmlElement] public static ColumnName IdentityName => ColumnName.Uid;
	[XmlElement] public virtual string Name { get; set; }
	[XmlElement] public virtual DateTime BeginDt { get; set; }
	[XmlElement] public virtual DateTime EndDt { get; set; }
	[XmlElement] public virtual DateTime ProdDt { get; set; }
	[XmlElement] public virtual int BoxCount { get; set; }
	[XmlElement] public virtual int PalletCount { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
    public OrderEntity() : base(0, false)
	{
		Init();
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="identityUid"></param>
	/// <param name="isSetupDates"></param>
	public OrderEntity(Guid identityUid, bool isSetupDates) : base(identityUid, isSetupDates)
	{
		Init();
	}

    #endregion

    public new virtual void Init()
    {
	    base.Init();
	    Name = string.Empty;
        BeginDt = DateTime.MinValue;
        ProdDt = DateTime.MinValue;
        EndDt = DateTime.MinValue;
        BoxCount = default;
        PalletCount = default;
    }

    #region Public and private methods

    public override string ToString()
    {
        return
			$"{nameof(IdentityUid)}: {IdentityUid}. " + 
			$"{nameof(IsMarked)}: {IsMarked}. " +
			$"{nameof(Name)}: {Name}. " + 
			$"{nameof(BeginDt)}: {BeginDt}. " +
			$"{nameof(EndDt)}: {EndDt}. " + 
			$"{nameof(ProdDt)}: {ProdDt}. " +
			$"{nameof(BoxCount)}: {BoxCount}. " +
			$"{nameof(PalletCount)}: {PalletCount}. ";
    }

    public virtual bool Equals(OrderEntity item)
    {
        //if (item is null) return false;
        if (ReferenceEquals(this, item)) return true;
        return base.Equals(item) &&
               Equals(Name, item.Name) &&
               Equals(BeginDt, item.BeginDt) &&
               Equals(EndDt, item.EndDt) && 
               Equals(ProdDt, item.ProdDt) &&
               Equals(BoxCount, item.BoxCount) &&
               Equals(PalletCount, item.PalletCount);
    }

    public override bool Equals(object obj)
    {
        //if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((OrderEntity)obj);
    }

	public override int GetHashCode() => IdentityUid.GetHashCode();

	public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        return base.EqualsDefault() &&
               Equals(Name, string.Empty) &&
               Equals(BeginDt, DateTime.MinValue) &&
               Equals(EndDt, DateTime.MinValue) &&
               Equals(ProdDt, DateTime.MinValue) &&
               Equals(BoxCount, 0) &&
               Equals(PalletCount, 0);
    }

    public new virtual object Clone()
    {
        OrderEntity item = new();
        item.Name = Name;
        item.BeginDt = BeginDt;
        item.EndDt = EndDt;
        item.ProdDt = ProdDt;
        item.BoxCount = BoxCount;
        item.PalletCount = PalletCount;
        item.Setup(((BaseEntity)this).CloneCast());
        return item;
    }

    public new virtual OrderEntity CloneCast() => (OrderEntity)Clone();

    #endregion
}
