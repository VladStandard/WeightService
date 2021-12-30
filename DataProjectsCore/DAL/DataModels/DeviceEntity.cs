using DataProjectsCore.DAL.TableScaleModels;
using DataShareCore.DAL.Models;

namespace DataProjectsCore.DAL.DataModels
{
    public class DeviceEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual ScaleEntity Scales { get; set; } = new ScaleEntity();

        #endregion

        #region Public and private methods - override

        public override string ToString()
        {
            return $"{nameof(Scales)}: {Scales}.";
        }

        public override int GetHashCode()
        {
            return Scales.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is DeviceEntity entity)
            {
                return
                   Scales == null && entity.Scales == null || Scales != null && Scales.Equals(entity.Scales);
            }
            return false;
        }

        #endregion

        #region Public and private methods

        public virtual bool EqualsNew()
        {
            return Equals(new DeviceEntity());
        }

        public new virtual bool EqualsDefault()
        {
            if (Scales != null && !Scales.EqualsDefault())
                return false;
            return base.EqualsDefault();
        }

        public override object Clone()
        {
            return new DeviceEntity()
            {
                PrimaryColumn = (PrimaryColumnEntity)PrimaryColumn.Clone(),
                Id = Id,
                Scales = (ScaleEntity)Scales.Clone(),
            };
        }

        #endregion
    }
}
