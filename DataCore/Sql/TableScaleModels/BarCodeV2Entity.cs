// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "BARCODES_V2".
/// </summary>
[Serializable]
public class BarCodeV2Entity : BaseEntity, ISerializable
{
	#region Public and private fields, properties, constructor

	/// <summary>
	/// Identity name.
	/// </summary>
	[XmlElement] public static ColumnName IdentityName => ColumnName.Uid;
	[XmlElement] public virtual string Value { get; set; } = string.Empty;
    [XmlElement] public virtual BarCodeTypeV2Entity? BarcodeType { get; set; } = new();
	[XmlElement] public virtual ContragentV2Entity? Contragent { get; set; } = new();
	[XmlElement] public virtual NomenclatureEntity? Nomenclature { get; set; } = new();

	/// <summary>
	/// Constructor.
	/// </summary>
    public BarCodeV2Entity() : this(Guid.Empty)
    {
        //
    }

	/// <summary>
	/// Constructor.
	/// </summary>
    public BarCodeV2Entity(Guid uid) : base(uid)
    {
        //
    }

	/// <summary>
	/// Constructor for serialization.
	/// </summary>
	/// <param name="info"></param>
	/// <param name="context"></param>
    protected BarCodeV2Entity(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Value = info.GetString(nameof(Value));
        BarcodeType = (BarCodeTypeV2Entity)info.GetValue(nameof(BarcodeType), typeof(BarCodeTypeV2Entity));
        Contragent = (ContragentV2Entity)info.GetValue(nameof(Contragent), typeof(ContragentV2Entity));
        Nomenclature = (NomenclatureEntity)info.GetValue(nameof(Nomenclature), typeof(NomenclatureEntity));
    }

    #endregion

    #region Public and private methods

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

    public virtual bool Equals(BarCodeV2Entity item)
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
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((BarCodeV2Entity)obj);
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
        BarCodeV2Entity item = new();
        item.Value = Value;
        item.BarcodeType = BarcodeType?.CloneCast();
        item.Contragent = Contragent?.CloneCast();
        item.Nomenclature = Nomenclature?.CloneCast();
        item.Setup(((BaseEntity)this).CloneCast());
        return item;
    }

    public new virtual BarCodeV2Entity CloneCast() => (BarCodeV2Entity)Clone();

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
