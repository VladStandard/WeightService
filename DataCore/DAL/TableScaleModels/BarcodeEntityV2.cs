// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;

namespace DataCore.DAL.TableScaleModels
{
    /// <summary>
    /// Таблица "ШК".
    /// </summary>
    public class BarCodeEntityV2 : BaseEntity
    {
        #region Public and private fields and properties

        public virtual string Value { get; set; } = string.Empty;
        public virtual BarCodeTypeEntityV2? BarcodeType { get; set; } = new();
        public virtual ContragentEntityV2? Contragent { get; set; } = new();
        public virtual NomenclatureEntity? Nomenclature { get; set; } = new();

        #endregion

        #region Constructor and destructor

        public BarCodeEntityV2()
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
                $"{nameof(Value)}: {Value}. " +
                $"{nameof(BarcodeType)}: {strBarcodeType}. " + 
                $"{nameof(Contragent)}: {strContragent}. " + 
                $"{nameof(Nomenclature)}: {strNomenclature}. ";
        }

        public virtual bool Equals(BarCodeEntityV2 entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
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
            return base.EqualsDefault() &&
                Equals(Value, string.Empty);
        }

        public override object Clone()
        {
            return new BarCodeEntityV2
            {
                PrimaryColumn = (PrimaryColumnEntity)PrimaryColumn.Clone(),
                CreateDt = CreateDt,
                ChangeDt = ChangeDt,
                IsMarked = IsMarked,
                Value = Value,
                BarcodeType = BarcodeType != null ? (BarCodeTypeEntityV2)BarcodeType.Clone() : null,
                Contragent = Contragent != null ? (ContragentEntityV2)Contragent.Clone() : null,
                Nomenclature = Nomenclature != null ? (NomenclatureEntity)Nomenclature.Clone() : null,
            };
        }

        #endregion
    }
}
