// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "ORDERS".
/// </summary>
[Serializable]
public class OrderModel : TableModel, ISerializable, ITableModel
{
	#region Public and private fields, properties, constructor

	[XmlElement] public virtual string Name { get; set; }
	[XmlElement] public virtual DateTime BeginDt { get; set; }
	[XmlElement] public virtual DateTime EndDt { get; set; }
	[XmlElement] public virtual DateTime ProdDt { get; set; }
	[XmlElement] public virtual int BoxCount { get; set; }
	[XmlElement] public virtual int PalletCount { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
    public OrderModel()
	{
		Name = string.Empty;
		BeginDt = DateTime.MinValue;
		ProdDt = DateTime.MinValue;
		EndDt = DateTime.MinValue;
		BoxCount = default;
		PalletCount = default;
	}

	#endregion

	#region Public and private methods

	public new virtual string ToString() =>
		$"{nameof(IsMarked)}: {IsMarked}. " +
		$"{nameof(Name)}: {Name}. " + 
		$"{nameof(BeginDt)}: {BeginDt}. " +
		$"{nameof(EndDt)}: {EndDt}. " + 
		$"{nameof(ProdDt)}: {ProdDt}. " +
		$"{nameof(BoxCount)}: {BoxCount}. " +
		$"{nameof(PalletCount)}: {PalletCount}. ";

	public virtual bool Equals(OrderModel item)
    {
        if (ReferenceEquals(this, item)) return true;
        return 
	        base.Equals(item) &&
            Equals(Name, item.Name) &&
            Equals(BeginDt, item.BeginDt) &&
            Equals(EndDt, item.EndDt) && 
            Equals(ProdDt, item.ProdDt) &&
            Equals(BoxCount, item.BoxCount) &&
            Equals(PalletCount, item.PalletCount);
    }

	public new virtual bool Equals(object obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((OrderModel)obj);
    }

	public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        return 
	        base.EqualsDefault() &&
            Equals(Name, string.Empty) &&
            Equals(BeginDt, DateTime.MinValue) &&
            Equals(EndDt, DateTime.MinValue) &&
            Equals(ProdDt, DateTime.MinValue) &&
            Equals(BoxCount, 0) &&
            Equals(PalletCount, 0);
    }

    public new virtual int GetHashCode() => base.GetHashCode();

	public new virtual object Clone()
    {
        OrderModel item = new();
        item.Name = Name;
        item.BeginDt = BeginDt;
        item.EndDt = EndDt;
        item.ProdDt = ProdDt;
        item.BoxCount = BoxCount;
        item.PalletCount = PalletCount;
		item.CloneSetup(base.CloneCast());
		return item;
    }

    public new virtual OrderModel CloneCast() => (OrderModel)Clone();

    #endregion
}
