// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "ProductSeries".
/// </summary>
[Serializable]
public class ProductSeriesEntity : TableModel, ISerializable, ITableModel
{
	#region Public and private fields, properties, constructor

	[XmlElement] public virtual ScaleEntity Scale { get; set; }
	[XmlElement] public virtual bool IsClose { get; set; }
	[XmlElement] public virtual string Sscc { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
    public ProductSeriesEntity() : base(ColumnName.Id, 0, false)
    {
		Init();
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="identityId"></param>
	/// <param name="isSetupDates"></param>
	public ProductSeriesEntity(long identityId, bool isSetupDates) : base(ColumnName.Id, identityId, isSetupDates)
	{
		Init();
	}

    #endregion

    #region Public and private methods

    public new virtual void Init()
    {
        base.Init();
        Scale = new();
        IsClose = false;
        Sscc = string.Empty;
    }

    public override string ToString()
    {
        string strScale = Scale != null ? Scale.IdentityId.ToString() : "null";
        return
			$"{nameof(IdentityId)}: {IdentityId}. " + 
			$"{nameof(IsMarked)}: {IsMarked}. " +
			$"{nameof(Scale)}: {strScale}. " +
			$"{nameof(IsClose)}: {IsClose}. " +
			$"{nameof(Sscc)}: {Sscc}.";
    }

    public virtual bool Equals(ProductSeriesEntity item)
    {
        if (item is null) return false;
        if (ReferenceEquals(this, item)) return true;
        if (Scale != null && item.Scale != null && !Scale.Equals(item.Scale))
            return false;
        return base.Equals(item) &&
               Equals(CreateDt, item.CreateDt) &&
               Equals(IsClose, item.IsClose) &&
               Equals(Sscc, item.Sscc);
    }

    public override bool Equals(object obj)
    {
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((ProductSeriesEntity)obj);
    }

	public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        if (Scale != null && !Scale.EqualsDefault())
            return false;
        return base.EqualsDefault() &&
               Equals(IsClose, false) &&
               Equals(Sscc, string.Empty);
    }

    public new virtual object Clone()
    {
        ProductSeriesEntity item = new();
        item.Scale = Scale.CloneCast();
        item.IsClose = IsClose;
        item.Sscc = Sscc;
        item.Setup(((TableModel)this).CloneCast());
        return item;
    }

    public new virtual ProductSeriesEntity CloneCast() => (ProductSeriesEntity)Clone();

    #endregion
}
