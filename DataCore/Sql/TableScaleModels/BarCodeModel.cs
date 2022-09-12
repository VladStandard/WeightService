// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "BARCODES_V2".
/// </summary>
[Serializable]
public class BarCodeModel : SqlTableBase, ICloneable, ISqlDbBase, ISerializable
{
	#region Public and private fields, properties, constructor

	[XmlElement] public virtual string Value { get; set; }
    [XmlElement(IsNullable = true)] public virtual BarCodeTypeModel? BarcodeType { get; set; }
	[XmlElement(IsNullable = true)] public virtual ContragentModel? Contragent { get; set; }
	[XmlElement(IsNullable = true)] public virtual NomenclatureModel? Nomenclature { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
    public BarCodeModel() : base(SqlFieldIdentityEnum.Uid)
	{
		Value = string.Empty;
		BarcodeType = new();
		Contragent = new();
		Nomenclature = new();
	}

	/// <summary>
	/// Constructor for serialization.
	/// </summary>
	/// <param name="info"></param>
	/// <param name="context"></param>
    protected BarCodeModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Value = info.GetString(nameof(Value));
        BarcodeType = (BarCodeTypeModel?)info.GetValue(nameof(BarcodeType), typeof(BarCodeTypeModel));
        Contragent = (ContragentModel?)info.GetValue(nameof(Contragent), typeof(ContragentModel));
        Nomenclature = (NomenclatureModel?)info.GetValue(nameof(Nomenclature), typeof(NomenclatureModel));
    }

	#endregion

	#region Public and private methods - override

	public override string ToString() =>
		$"{nameof(IsMarked)}: {IsMarked}. " +
	    $"{nameof(Value)}: {Value}. " +
	    $"{nameof(BarcodeType)}: {BarcodeType?.ToString() ?? "null"}. " +
	    $"{nameof(Contragent)}: {Contragent?.ToString() ?? "null"}. " +
	    $"{nameof(Nomenclature)}: {Nomenclature?.ToString() ?? "null"}. ";

    public override bool Equals(object obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((BarCodeModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

	public override bool EqualsNew() => Equals(new());

	public override bool EqualsDefault()
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

	public override object Clone()
    {
        BarCodeModel item = new();
        item.Value = Value;
        item.BarcodeType = BarcodeType?.CloneCast();
        item.Contragent = Contragent?.CloneCast();
        item.Nomenclature = Nomenclature?.CloneCast();
		item.CloneSetup(base.CloneCast());
		return item;
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
	    base.GetObjectData(info, context);
	    info.AddValue(nameof(Value), Value);
	    info.AddValue(nameof(BarcodeType), BarcodeType);
	    info.AddValue(nameof(Contragent), Contragent);
	    info.AddValue(nameof(Nomenclature), Nomenclature);
    }

	#endregion

	#region Public and private methods - virtual

	public virtual bool Equals(BarCodeModel item)
	{
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

	public new virtual BarCodeModel CloneCast() => (BarCodeModel)Clone();

    #endregion
}
