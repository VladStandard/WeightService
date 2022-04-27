// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using System;
using System.Runtime.Serialization;

namespace DataCore.DAL.TableScaleModels
{
    /// <summary>
    /// Table "Apps".
    /// </summary>
    [Serializable]
    public class AppEntity : BaseEntity, ISerializable
    {
        #region Public and private fields and properties

        public virtual string Name { get; set; }

        #endregion

        #region Constructor and destructor

        public AppEntity() : this(Guid.Empty)
        {
            //
        }

        public AppEntity(Guid uid) : base(uid)
        {
            Name = string.Empty;
        }

        public AppEntity(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Name = info.GetString(nameof(Name));
        }

        public AppEntity(BaseEntity baseItem) : this()
        {
            base.Setup(baseItem);
        }

        #endregion

        #region Public and private methods

        public override string ToString() =>
            base.ToString() +
            $"{nameof(Name)}: {Name}. ";

        public virtual bool Equals(AppEntity item)
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
            return Equals((AppEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new AppEntity());
        }

        public new virtual bool EqualsDefault()
        {
            return base.EqualsDefault(IdentityName) &&
                Equals(Name, string.Empty);
        }

        public override object Clone()
        {
            AppEntity item = new((BaseEntity)base.Clone());
            item.Name = Name;
            return item;
        }

        public virtual AppEntity CloneCast() => (AppEntity)Clone();

        public new virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(Name), Name);
        }

        #endregion
    }
}
