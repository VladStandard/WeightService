// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using System;

namespace DataCore.DAL.TableScaleModels
{
    /// <summary>
    /// Таблица "ШК".
    /// </summary>
    public class BarcodeEntityV2 : BaseEntity
    {
        #region Public and private fields and properties

        public virtual DateTime CreateDt { get; set; } = default;
        public virtual DateTime ChangeDt { get; set; } = default;
        public virtual bool IsMarked { get; set; } = false;
        public virtual string Value { get; set; } = string.Empty;
        public virtual BarcodeTypeEntityV2? BarcodeType { get; set; } = new();
        public virtual ContragentEntityV2? Contragent { get; set; } = new();
        public virtual NomenclatureEntity? Nomenclature { get; set; } = new();

        #endregion

        #region Constructor and destructor

        public BarcodeEntityV2()
        {
            PrimaryColumn = new PrimaryColumnEntity(ColumnName.Uid);
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            string? strBarcodeType = BarcodeType != null ? BarcodeType.Uid.ToString() : "null";
            string? strContragent = Contragent != null ? Contragent.Uid.ToString() : "null";
            string? strNomenclature = Nomenclature != null ? Nomenclature.Id.ToString() : "null";
            return base.ToString() +
                $"{nameof(CreateDt)}: {CreateDt}. " +
                $"{nameof(ChangeDt)}: {ChangeDt}. " +
                $"{nameof(IsMarked)}: {IsMarked}." +
                $"{nameof(Value)}: {Value}. " +
                $"{nameof(BarcodeType)}: {strBarcodeType}. " + 
                $"{nameof(Contragent)}: {strContragent}. " + 
                $"{nameof(Nomenclature)}: {strNomenclature}. ";
        }

        public virtual bool Equals(BarcodeEntityV2 entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                Equals(CreateDt, entity.CreateDt) &&
                Equals(ChangeDt, entity.ChangeDt) &&
                Equals(IsMarked, entity.IsMarked) &&
                Equals(Value, entity.Value) &&
                BarcodeType != null && entity.BarcodeType != null && BarcodeType.Equals(entity.BarcodeType) &&
                Contragent != null && entity.Contragent != null && Contragent.Equals(entity.Contragent) &&
                Nomenclature != null && entity.Nomenclature != null && Nomenclature.Equals(entity.Nomenclature);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((BarcodeEntityV2)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new BarcodeEntityV2());
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
                Equals(CreateDt, default(DateTime)) &&
                Equals(ChangeDt, default(DateTime)) &&
                Equals(IsMarked, false) &&
                Equals(Value, string.Empty);
        }

        public override object Clone()
        {
            return new BarcodeEntityV2
            {
                PrimaryColumn = (PrimaryColumnEntity)PrimaryColumn.Clone(),
                Uid = Uid,
                CreateDt = CreateDt,
                ChangeDt = ChangeDt,
                IsMarked = IsMarked,
                Value = Value,
                BarcodeType = BarcodeType != null ? (BarcodeTypeEntityV2)BarcodeType.Clone() : null,
                Contragent = Contragent != null ? (ContragentEntityV2)Contragent.Clone() : null,
                Nomenclature = Nomenclature != null ? (NomenclatureEntity)Nomenclature.Clone() : null,
            };
        }

        #endregion
    }
}
