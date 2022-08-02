// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using System;
using System.Runtime.Serialization;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "BARCODES_V2".
/// </summary>
[Serializable]
public class BarCodeEntityV2 : BaseEntity, ISerializable
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// Identity name.
    /// </summary>
    public static ColumnName IdentityName => ColumnName.Uid;
    public virtual string Value { get; set; }
    public virtual BarCodeTypeEntityV2? BarcodeType { get; set; }
    public virtual ContragentEntityV2? Contragent { get; set; }
    public virtual NomenclatureEntity? Nomenclature { get; set; }

    public BarCodeEntityV2() : this(Guid.Empty)
    {
        //
    }

    public BarCodeEntityV2(Guid uid) : base(uid)
    {
        Value = string.Empty;
        BarcodeType = new();
        Contragent = new();
        Nomenclature = new();
    }

    protected BarCodeEntityV2(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Value = info.GetString(nameof(Value));
        BarcodeType = (BarCodeTypeEntityV2)info.GetValue(nameof(BarcodeType), typeof(BarCodeTypeEntityV2));
        Contragent = (ContragentEntityV2)info.GetValue(nameof(Contragent), typeof(ContragentEntityV2));
        Nomenclature = (NomenclatureEntity)info.GetValue(nameof(Nomenclature), typeof(NomenclatureEntity));
    }

    #endregion

    #region Public and private methods

    public override string ToString()
    {
        string strBarcodeType = BarcodeType != null ? BarcodeType.IdentityUid.ToString() : "null";
        string strContragent = Contragent != null ? Contragent.IdentityUid.ToString() : "null";
        string strNomenclature = Nomenclature != null ? Nomenclature.IdentityId.ToString() : "null";
        return base.ToString() +
            $"{nameof(Value)}: {Value}. " +
            $"{nameof(BarcodeType)}: {strBarcodeType}. " +
            $"{nameof(Contragent)}: {strContragent}. " +
            $"{nameof(Nomenclature)}: {strNomenclature}. ";
    }

    public virtual bool Equals(BarCodeEntityV2 item)
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
        return Equals((BarCodeEntityV2)obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

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
        BarCodeEntityV2 item = new();
        item.Value = Value;
        item.BarcodeType = BarcodeType?.CloneCast();
        item.Contragent = Contragent?.CloneCast();
        item.Nomenclature = Nomenclature?.CloneCast();
        item.Setup(((BaseEntity)this).CloneCast());
        return item;
    }

    public new virtual BarCodeEntityV2 CloneCast() => (BarCodeEntityV2)Clone();

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
