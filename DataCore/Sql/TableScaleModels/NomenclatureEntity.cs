// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "Nomenclature".
/// </summary>
[Serializable]
public class NomenclatureEntity : TableModel, ISerializable, ITableModel
{
	#region Public and private fields, properties, constructor

	[XmlElement] public virtual string Name { get; set; }
	[XmlElement] public virtual string Code { get; set; }
	[XmlElement(IsNullable = true)] public virtual string? Xml { get; set; }
	/// <summary>
	/// Is weighted or pcs.
	/// </summary>
	[XmlElement] public virtual bool Weighted { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
    public NomenclatureEntity() : base(ColumnName.Id, 0, false)
	{
		Init();
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="isSetupDates"></param>
	public NomenclatureEntity(long identityId, bool isSetupDates) : base(ColumnName.Id, identityId, isSetupDates)
    {
		Init();
	}

    #endregion

    #region Public and private methods

    public new virtual void Init()
    {
        base.Init();
        Name = string.Empty;
		Code = string.Empty;
		Xml = string.Empty;
	}

    public override string ToString() =>
	    $"{nameof(IdentityId)}: {IdentityId}. " +
	    $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Code)}: {Code}. " +
        $"{nameof(Xml)}.Length: {Xml?.Length ?? 0}. " +
        $"{nameof(Weighted)}: {Weighted}. ";

    public virtual bool Equals(NomenclatureEntity item)
    {
        if (item is null) return false;
        if (ReferenceEquals(this, item)) return true;
        return base.Equals(item) &&
               Equals(Code, item.Code) &&
               Equals(Name, item.Name) &&
               Equals(Xml, item.Xml) &&
               Equals(Weighted, item.Weighted);
    }

    public override bool Equals(object obj)
    {
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((NomenclatureEntity)obj);
    }

	public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        return base.EqualsDefault() &&
               Equals(Code, string.Empty) &&
               Equals(Name, string.Empty) &&
               Equals(Xml, string.Empty) &&
               Equals(Weighted, false);
    }

    public new virtual object Clone()
    {
        NomenclatureEntity item = new();
        item.Code = Code;
        item.Name = Name;
        item.Xml = Xml;
        item.Weighted = Weighted;
        item.Setup(((TableModel)this).CloneCast());
        return item;
    }

    public new virtual NomenclatureEntity CloneCast() => (NomenclatureEntity)Clone();

    #endregion
}
