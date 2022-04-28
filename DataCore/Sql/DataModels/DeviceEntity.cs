// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels;
using DataCore.Sql.Models;

namespace DataCore.Sql.DataModels
{
    public class DeviceEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual ScaleEntity Scales { get; set; }

        #endregion

        #region Constructor and destructor

        public DeviceEntity() : this(0)
        {
            //
        }

        public DeviceEntity(long id) : base(id)
        {
            Scales = new();
        }

        #endregion

        #region Public and private methods - override

        public override string ToString() =>
            $"{nameof(Scales)}: {Scales}.";

        public override int GetHashCode()
        {
            return Scales.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is DeviceEntity item)
            {
                return
                   Scales == null && item.Scales == null || Scales != null && Scales.Equals(item.Scales);
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
            return base.EqualsDefault(IdentityName);
        }

        public new virtual object Clone()
        {
            DeviceEntity item = new()
            {
                Scales = Scales.CloneCast,
            };
            item.Setup(((BaseEntity)this).CloneCast);
            return item;
        }

        public new virtual DeviceEntity CloneCast => (DeviceEntity)Clone();

        #endregion
    }
}
