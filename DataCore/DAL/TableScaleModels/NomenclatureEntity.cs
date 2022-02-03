// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using System;

namespace DataCore.DAL.TableScaleModels
{
    /// <summary>
    /// Таблица "Продукты".
    /// </summary>
    public class NomenclatureEntity : BaseEntity
    {
        #region Public and private fields and properties

        /// <summary>
        /// Дата создания.
        /// </summary>
        public virtual DateTime? CreateDate { get; set; }
        /// <summary>
        /// Дата изменения.
        /// </summary>
        public virtual DateTime? ModifiedDate { get; set; }
        /// <summary>
        /// Наименование.
        /// </summary>
        public virtual string Name { get; set; } = string.Empty;
        public virtual string Code { get; set; } = string.Empty;
        public virtual string SerializedRepresentationObject { get; set; } = string.Empty;

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
                $"{nameof(CreateDate)}: {CreateDate}. " +
                $"{nameof(ModifiedDate)}: {ModifiedDate}. " +
                $"{nameof(Code)}: {Code}. " +
                $"{nameof(SerializedRepresentationObject)}.Length: {SerializedRepresentationObject?.Length ?? 0}.";
        }

        public virtual bool Equals(NomenclatureEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                   Equals(CreateDate, entity.CreateDate) &&
                   Equals(ModifiedDate, entity.ModifiedDate) &&
                   Equals(Code, entity.Code) &&
                   Equals(Name, entity.Name) &&
                   Equals(SerializedRepresentationObject, entity.SerializedRepresentationObject);
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
                   Equals(CreateDate, default(DateTime?)) &&
                   Equals(ModifiedDate, default(DateTime?)) &&
                   Equals(Code, default(string)) &&
                   Equals(Name, default(string)) &&
                   Equals(SerializedRepresentationObject, default(string));
        }

        public override object Clone()
        {
            return new NomenclatureEntity
            {
                PrimaryColumn = (PrimaryColumnEntity)PrimaryColumn.Clone(),
                Id = Id,
                CreateDate = CreateDate,
                ModifiedDate = ModifiedDate,
                Code = Code,
                Name = Name,
                SerializedRepresentationObject = SerializedRepresentationObject,
            };
        }

        #endregion
    }
}
