// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;

namespace DataCore.DAL.TableScaleModels
{
    /// <summary>
    /// Таблица "Продукты".
    /// </summary>
    public class NomenclatureEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual string Name { get; set; } = string.Empty;
        public virtual string Code { get; set; } = string.Empty;
        public virtual string SerializedRepresentationObject { get; set; } = string.Empty;
        /// <summary>
        /// Весовая или штучка.
        /// </summary>
        public virtual bool Weighted { get; set; } = default;

        #endregion

        #region Constructor and destructor

        public NomenclatureEntity()
        {
            PrimaryColumn = new PrimaryColumnEntity(ColumnName.Id);
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            return base.ToString() +
                $"{nameof(Code)}: {Code}. " +
                $"{nameof(SerializedRepresentationObject)}.Length: {SerializedRepresentationObject?.Length ?? 0}. " + 
                $"{nameof(Weighted)}: {Weighted}. ";
        }

        public virtual bool Equals(NomenclatureEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                   Equals(Code, entity.Code) &&
                   Equals(Name, entity.Name) &&
                   Equals(SerializedRepresentationObject, entity.SerializedRepresentationObject) &&
                   Equals(Weighted, entity.Weighted);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((NomenclatureEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new NomenclatureEntity());
        }

        public new virtual bool EqualsDefault()
        {
            return base.EqualsDefault() &&
                   Equals(Code, default(string)) &&
                   Equals(Name, default(string)) &&
                   Equals(SerializedRepresentationObject, default(string)) &&
                   Equals(Weighted, default(bool));
        }

        public override object Clone()
        {
            return new NomenclatureEntity
            {
                PrimaryColumn = (PrimaryColumnEntity)PrimaryColumn.Clone(),
                CreateDt = CreateDt,
                ChangeDt = ChangeDt,
                IsMarked = IsMarked,
                Code = Code,
                Name = Name,
                SerializedRepresentationObject = SerializedRepresentationObject,
                Weighted = Weighted,
            };
        }

        #endregion
    }
}
