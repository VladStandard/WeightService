// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using System;

namespace DataCore.DAL.TableScaleModels
{
    /// <summary>
    /// Table "BarCodes".
    /// </summary>
    public class BarCodeEntityV2 : BaseEntity
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
            return base.Equals(item) &&
                Equals(Value, item.Value) &&
                BarcodeType != null && item.BarcodeType != null && BarcodeType.Equals(item.BarcodeType) &&
                Contragent != null && item.Contragent != null && Contragent.Equals(item.Contragent) &&
                Nomenclature != null && item.Nomenclature != null && Nomenclature.Equals(item.Nomenclature);
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

        public override object Clone()
        {
            BarCodeEntityV2 item = (BarCodeEntityV2)base.Clone();
            item.Value = Value;
            item.BarcodeType = BarcodeType != null ? (BarCodeTypeEntityV2)BarcodeType.Clone() : null;
            item.Contragent = Contragent != null ? (ContragentEntityV2)Contragent.Clone() : null;
            item.Nomenclature = Nomenclature != null ? (NomenclatureEntity)Nomenclature.Clone() : null;
            return item;
        }

        #endregion
    }
}
