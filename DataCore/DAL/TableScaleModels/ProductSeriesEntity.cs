// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;

namespace DataCore.DAL.TableScaleModels
{
    /// <summary>
    /// Table "ProductSeries".
    /// </summary>
    public class ProductSeriesEntity : BaseEntity<ProductSeriesEntity>
    {
        #region Public and private fields and properties

        public virtual ScaleEntity Scale { get; set; }
        public virtual bool IsClose { get; set; }
        public virtual string Sscc { get; set; }

        #endregion

        #region Constructor and destructor

        public ProductSeriesEntity() : this(0)
        {
            //
        }

        public ProductSeriesEntity(long id) : base(id)
        {
            Scale = new ScaleEntity();
            IsClose = false;
            Sscc = string.Empty;
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            string? strScale = Scale != null ? Scale.IdentityId.ToString() : "null";
            return base.ToString() +
                   $"{nameof(Scale)}: {strScale}. " +
                   $"{nameof(IsClose)}: {IsClose}. " +
                   $"{nameof(Sscc)}: {Sscc}.";
        }

        public virtual bool Equals(ProductSeriesEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                   Equals(CreateDt, entity.CreateDt) &&
                   Equals(Scale, entity.Scale) &&
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
                   Equals(IsClose, false) &&
                   Equals(Sscc, string.Empty);
        }

        public override object Clone()
        {
            ProductSeriesEntity item = (ProductSeriesEntity)base.Clone();
            item.Scale = (ScaleEntity)Scale.Clone();
            item.IsClose = IsClose;
            item.Sscc = Sscc;
            return item;
        }

        #endregion
    }
}
