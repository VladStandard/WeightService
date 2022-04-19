// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using System;

namespace DataCore.DAL.TableScaleModels
{
    /// <summary>
    /// Table "Access".
    /// </summary>
    public class AccessEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual string User { get; set; }
        public virtual byte Rights { get; set; }

        #endregion

        #region Constructor and destructor

        public AccessEntity() : this(Guid.Empty)
        {
            //
        }

        public AccessEntity(Guid uid) : base(uid)
        {
            User = string.Empty;
            Rights = 0x00;
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            return base.ToString() +
                   $"{nameof(User)}: {User}. " +
                   $"{nameof(Rights)}: {Rights}. ";
        }

        public virtual bool Equals(AccessEntity item)
        {
            if (item is null) return false;
            if (ReferenceEquals(this, item)) return true;
            return base.Equals(item) &&
                   Equals(User, item.User) &&
                   Equals(Rights, item.Rights);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((AccessEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new AccessEntity());
        }

        public new virtual bool EqualsDefault()
        {
            return base.EqualsDefault() &&
                   Equals(User, string.Empty) &&
                   Equals(Rights, 0x00);
        }

        public override object Clone()
        {
            AccessEntity item = (AccessEntity)base.Clone();
            item.User = User;
            item.Rights = Rights;
            return item;
        }

        #endregion
    }
}
