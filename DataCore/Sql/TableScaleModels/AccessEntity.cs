// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using System;
using System.Runtime.Serialization;

namespace DataCore.Sql.TableScaleModels
{
    /// <summary>
    /// Table "Access".
    /// </summary>
    [Serializable]
    public class AccessEntity : BaseEntity, ISerializable
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

        protected AccessEntity(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            User = info.GetString(nameof(User));
            Rights = info.GetByte(nameof(Rights));
        }

        #endregion

        #region Public and private methods

        public override string ToString() =>
            base.ToString() +
            $"{nameof(User)}: {User}. " +
            $"{nameof(Rights)}: {Rights}. ";

        public virtual string ToStringShort() =>
            $"{nameof(User)}: {User}. " +
            $"{nameof(Rights)}: {Rights}. ";

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
            return Equals(new());
        }

        public new virtual bool EqualsDefault()
        {
            return base.EqualsDefault(IdentityName) &&
                   Equals(User, string.Empty) &&
                   Equals(Rights, (byte)0x00);
        }

        public new virtual object Clone()
        {
            AccessEntity item = new();
            item.User = User;
            item.Rights = Rights;
            item.Setup(((BaseEntity)this).CloneCast());
            return item;
        }

        public new virtual AccessEntity CloneCast() => (AccessEntity)Clone();

        public new virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(User), User);
            info.AddValue(nameof(Rights), Rights);
        }

        #endregion
    }
}
