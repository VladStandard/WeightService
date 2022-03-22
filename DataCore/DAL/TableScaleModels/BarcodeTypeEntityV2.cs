// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using System;

namespace DataCore.DAL.TableScaleModels
{
    /// <summary>
    /// Таблица "Типы ШК".
    /// </summary>
    public class BarcodeTypeEntityV2 : BaseEntity
    {
        #region Public and private fields and properties

        public virtual DateTime CreateDt { get; set; } = default;
        public virtual DateTime ChangeDt { get; set; } = default;
        public virtual bool IsMarked { get; set; } = false;
        public virtual string Name { get; set; } = string.Empty;

        #endregion

        #region Constructor and destructor

        public BarcodeTypeEntityV2()
        {
            PrimaryColumn = new PrimaryColumnEntity(ColumnName.Uid);
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            return base.ToString() +
                $"{nameof(CreateDt)}: {CreateDt}. " +
                $"{nameof(ChangeDt)}: {ChangeDt}. " +
                $"{nameof(IsMarked)}: {IsMarked}." +
                $"{nameof(Name)}: {Name}. ";
        }

        public virtual bool Equals(BarcodeTypeEntityV2 entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                Equals(CreateDt, entity.CreateDt) &&
                Equals(ChangeDt, entity.ChangeDt) &&
                Equals(IsMarked, entity.IsMarked) &&
                Equals(Name, entity.Name);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((BarcodeTypeEntityV2)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new BarcodeTypeEntityV2());
        }

        public new virtual bool EqualsDefault()
        {
            return base.EqualsDefault() &&
                Equals(CreateDt, default(DateTime)) &&
                Equals(ChangeDt, default(DateTime)) &&
                Equals(IsMarked, false) &&
                Equals(Name, string.Empty);
        }

        public override object Clone()
        {
            return new BarcodeTypeEntityV2
            {
                PrimaryColumn = (PrimaryColumnEntity)PrimaryColumn.Clone(),
                Uid = Uid,
                CreateDt = CreateDt,
                ChangeDt = ChangeDt,
                IsMarked = IsMarked,
                Name = Name,
            };
        }

        #endregion
    }
}
