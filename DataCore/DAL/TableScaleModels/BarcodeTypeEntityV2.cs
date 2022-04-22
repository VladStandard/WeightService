// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using System;

namespace DataCore.DAL.TableScaleModels
{
    /// <summary>
    /// Table "BarCodeTypes".
    /// </summary>
    public class BarCodeTypeEntityV2 : BaseEntity
    {
        #region Public and private fields and properties

        public virtual string Name { get; set; }

        #endregion

        #region Constructor and destructor

        public BarCodeTypeEntityV2() : this(Guid.Empty)
        {
            //
        }

        public BarCodeTypeEntityV2(Guid uid) : base(uid)
        {
            Name = string.Empty;
        }

        #endregion

        #region Public and private methods

        public override string ToString() =>
            base.ToString() +
            $"{nameof(Name)}: {Name}. ";

        public virtual bool Equals(BarCodeTypeEntityV2 item)
        {
            if (item is null) return false;
            if (ReferenceEquals(this, item)) return true;
            return base.Equals(item) &&
                Equals(Name, item.Name);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((BarCodeTypeEntityV2)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new BarCodeTypeEntityV2());
        }

        public new virtual bool EqualsDefault()
        {
            return base.EqualsDefault() &&
                Equals(Name, string.Empty);
        }

        public override object Clone()
        {
            BarCodeTypeEntityV2 item = (BarCodeTypeEntityV2)base.Clone();
            item.Name = Name;
            return item;
        }

        #endregion
    }
}
