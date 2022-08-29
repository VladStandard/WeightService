// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "BARCODES_V2".
/// </summary>
[Serializable]
public class BarCodeEntity : TableModel, ISerializable, ITableModel
{
	#region Public and private fields, properties, constructor

	/// <summary>
	/// Identity name.
	/// </summary>
	[XmlElement] public static ColumnName IdentityName => ColumnName.Uid;
	[XmlElement] public virtual string Value { get; set; }
    [XmlElement] public virtual BarCodeTypeEntity? BarcodeType { get; set; }
	[XmlElement] public virtual ContragentEntity? Contragent { get; set; }
	[XmlElement] public virtual NomenclatureEntity? Nomenclature { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
    public BarCodeEntity() : base(Guid.Empty, false)
	{
		Init();
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="identityUid"></param>
	/// <param name="isSetupDates"></param>
	public BarCodeEntity(Guid identityUid, bool isSetupDates) : base(identityUid, isSetupDates)
    {
		Init();
	}

	/// <summary>
	/// Constructor for serialization.
	/// </summary>
	/// <param name="info"></param>
	/// <param name="context"></param>
    protected BarCodeEntity(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Value = info.GetString(nameof(Value));
        BarcodeType = (BarCodeTypeEntity)info.GetValue(nameof(BarcodeType), typeof(BarCodeTypeEntity));
        Contragent = (ContragentEntity)info.GetValue(nameof(Contragent), typeof(ContragentEntity));
        Nomenclature = (NomenclatureEntity)info.GetValue(nameof(Nomenclature), typeof(NomenclatureEntity));
    }

    #endregion

    #region Public and private methods

    public new virtual void Init()
    {
	    base.Init();
		Value = string.Empty;
		BarcodeType = new();
		Contragent = new();
		Nomenclature = new();
	}

    public override string ToString()
    {
        string strBarcodeType = BarcodeType != null ? BarcodeType.IdentityUid.ToString() : "null";
        string strContragent = Contragent != null ? Contragent.IdentityUid.ToString() : "null";
        string strNomenclature = Nomenclature != null ? Nomenclature.IdentityId.ToString() : "null";
        return
			$"{nameof(IdentityUid)}: {IdentityUid}. " +
            $"{nameof(IsMarked)}: {IsMarked}. " +
            $"{nameof(Value)}: {Value}. " +
            $"{nameof(BarcodeType)}: {strBarcodeType}. " +
            $"{nameof(Contragent)}: {strContragent}. " +
            $"{nameof(Nomenclature)}: {strNomenclature}. ";
    }

    public virtual bool Equals(BarCodeEntity item)
    {
        if (item is null) return false;
        if (ReferenceEquals(this, item)) return true;
        if (BarcodeType != null && item.BarcodeType != null && !BarcodeType.Equals(item.BarcodeType))
            return false;
        if (Contragent != null && item.Contragent != null && !Contragent.Equals(item.Contragent))
            return false;
        if (Nomenclature != null && item.Nomenclature != null && !Nomenclature.Equals(item.Nomenclature))
            return false;
        return base.Equals(item) &&
            Equals(Value, item.Value);
    }

    public override bool Equals(object obj)
    {
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((BarCodeEntity)obj);
    }

	public override int GetHashCode() => IdentityUid.GetHashCode();

	public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        if (BarcodeType != null && !BarcodeType.EqualsDefault())
            return false;
        if (Contragent != null && !Contragent.EqualsDefault())
            return false;
        if (Nomenclature != null && !Nomenclature.EqualsDefault())
            return false;
        return base.EqualsDefault() &&
            Equals(Value, string.Empty);
    }

    public new virtual object Clone()
    {
        BarCodeEntity item = new();
        item.Value = Value;
        item.BarcodeType = BarcodeType?.CloneCast();
        item.Contragent = Contragent?.CloneCast();
        item.Nomenclature = Nomenclature?.CloneCast();
        item.Setup(((TableModel)this).CloneCast());
        return item;
    }

    public new virtual BarCodeEntity CloneCast() => (BarCodeEntity)Clone();

    public new virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Value), Value);
        info.AddValue(nameof(BarcodeType), BarcodeType);
        info.AddValue(nameof(Contragent), Contragent);
        info.AddValue(nameof(Nomenclature), Nomenclature);
    }

    #endregion
}
