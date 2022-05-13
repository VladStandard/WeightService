// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using System;
using System.Runtime.Serialization;

namespace DataCore.Sql.TableScaleModels
{
    /// <summary>
    /// Table "BarCodes".
    /// </summary>
    [Serializable]
    public class BarCodeEntityV2 : BaseEntity, ISerializable
    {
        #region Public and private fields and properties

        public virtual string Value { get; set; }
        public virtual BarCodeTypeEntityV2? BarcodeType { get; set; }
        public virtual ContragentEntityV2? Contragent { get; set; }
        public virtual NomenclatureEntity? Nomenclature { get; set; }

        #endregion

        #region Constructor and destructor

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

        public BarCodeEntityV2(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Value = info.GetString(nameof(Value));
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
            return Equals(new BarCodeEntityV2());
        }

        public new virtual bool EqualsDefault()
        {
            if (BarcodeType != null && !BarcodeType.EqualsDefault())
                return false;
            if (Contragent != null && !Contragent.EqualsDefault())
                return false;
            if (Nomenclature != null && !Nomenclature.EqualsDefault())
                return false;
            return base.EqualsDefault(IdentityName) &&
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

        #endregion
    }
}
