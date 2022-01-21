// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore.DAL.Models;
using System;

namespace DataProjectsCore.DAL.TableScaleModels
{
    /// <summary>
    /// Таблица "".
    /// </summary>
    public class ProductSeriesEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual DateTime? CreateDate { get; set; }
        public virtual ScaleEntity Scale { get; set; } = new ScaleEntity();
        //public virtual Guid? Uid { get; set; }
        public virtual string UidGui
        {
            get => Uid.ToString();
            set
            {
                try
                {
                    Uid = Guid.Parse(value);
                }
                catch (Exception)
                {
                    Uid = Guid.Empty;
                }
            }
        }
        public virtual bool? IsClose { get; set; }
        public virtual bool IsCloseGui
        {
            get => IsClose == true;
            set => IsClose = value;
        }
        public virtual string Sscc { get; set; }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            string? strScale = Scale != null ? Scale.Id.ToString() : "null";
            return base.ToString() +
                   $"{nameof(Scale)}: {strScale}. " +
                   $"{nameof(CreateDate)}: {CreateDate}. " +
                   $"{nameof(Uid)}: {Uid}. " +
                   $"{nameof(IsClose)}: {IsClose}. " +
                   $"{nameof(Sscc)}: {Sscc}.";
        }

        public virtual bool Equals(ProductSeriesEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                   Equals(CreateDate, entity.CreateDate) &&
                   Equals(Scale, entity.Scale) &&
                   Equals(Uid, entity.Uid) &&
                   Equals(IsClose, entity.IsClose) &&
                   Equals(Sscc, entity.Sscc);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ProductSeriesEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new ProductSeriesEntity());
        }

        public new virtual bool EqualsDefault()
        {
            if (Scale != null && !Scale.EqualsDefault())
                return false;
            return base.EqualsDefault() &&
                   Equals(CreateDate, default(DateTime?)) &&
                   Equals(Uid, default(Guid?)) &&
                   Equals(IsClose, default(bool?)) &&
                   Equals(Sscc, default(string));
        }

        public override object Clone()
        {
            return new ProductSeriesEntity
            {
                PrimaryColumn = (PrimaryColumnEntity)PrimaryColumn.Clone(),
                Id = Id,
                CreateDate = CreateDate,
                Scale = (ScaleEntity)Scale?.Clone(),
                Uid = Uid,
                IsClose = IsClose,
                Sscc = Sscc,
            };
        }

        #endregion
    }
}
