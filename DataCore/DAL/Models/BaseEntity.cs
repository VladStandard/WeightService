﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace DataCore.DAL.Models
{
    public enum ColumnName
    {
        Default,
        Id,
        Uid,
    }

    public class BaseEntity : BaseSerializeEntity, ICloneable, ISerializable
    {
        #region Public and private fields and properties

        public virtual bool IsMarked { get; set; }
        [XmlIgnore] public virtual ColumnName IdentityName { get; private set; }
        public virtual DateTime ChangeDt { get; set; }
        public virtual DateTime CreateDt { get; set; }
        public virtual Guid IdentityUid { get; set; }
        public virtual long IdentityId { get; set; }
        [XmlIgnore] public virtual string IdentityUidStr { get => (IdentityUid.ToString() is string str) ? str : Guid.Empty.ToString(); set => IdentityUid = Guid.TryParse(value, out Guid uid) ? uid : Guid.Empty; }

        #endregion

        #region Constructor and destructor

        private BaseEntity()
        {
            IdentityName = ColumnName.Default;
            IdentityId = 0;
            IdentityUid = Guid.Empty;
            CreateDt = DateTime.MinValue;
            ChangeDt = DateTime.MinValue;
            IsMarked = false;
        }

        public BaseEntity(long identityId) : this()
        {
            IdentityName = ColumnName.Id;
            IdentityId = identityId;
        }

        public BaseEntity(Guid identityUid) : this()
        {
            IdentityName = ColumnName.Uid;
            IdentityUid = identityUid;
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            string strIdenttity = IdentityName switch
            {
                ColumnName.Id => $"{nameof(IdentityId)}: {IdentityId}. ",
                ColumnName.Uid => $"{nameof(IdentityUid)}: {IdentityUid}. ",
                _ => $"{nameof(IdentityName)}: {IdentityName}. ",
            };
            return
                $"{nameof(IdentityName)}: {strIdenttity}. " +
                $"{nameof(CreateDt)}: {(CreateDt != null ? CreateDt.ToString() : "null")}. " +
                $"{nameof(ChangeDt)}: {(ChangeDt != null ? ChangeDt.ToString() : "null")}. " +
                $"{nameof(IsMarked)}: {IsMarked}. ";
        }

        public override int GetHashCode() => IdentityName switch
        {
            ColumnName.Id => IdentityId.GetHashCode(),
            ColumnName.Uid => IdentityUid.GetHashCode(),
            _ => default,
        };

        public virtual bool EqualsEmpty()
        {
            bool isIdentityEmpty = IdentityName switch
            {
                ColumnName.Id => Equals(IdentityId, 0),
                ColumnName.Uid => Equals(IdentityUid, Guid.Empty),
                _ => Equals(IdentityName, ColumnName.Default),
            };
            return isIdentityEmpty;
        }

        public virtual bool Equals(BaseEntity item)
        {
            if (item is null) return false;
            if (ReferenceEquals(this, item)) return true;
            return
                IdentityName.Equals(item.IdentityName) &&
                IdentityId.Equals(item.IdentityId) &&
                IdentityUid.Equals(item.IdentityUid) &&
                Equals(CreateDt, item.CreateDt) &&
                Equals(ChangeDt, item.ChangeDt) &&
                Equals(IsMarked, item.IsMarked);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((BaseEntity)obj);
        }

        public new virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(IsMarked), IsMarked);
            info.AddValue(nameof(ChangeDt), ChangeDt);
            info.AddValue(nameof(CreateDt), CreateDt);
            info.AddValue(nameof(IdentityUid), IdentityUid);
            info.AddValue(nameof(IdentityId), IdentityId);
        }

        public virtual bool EqualsDefault() => Equals(IdentityName, ColumnName.Default) &&
                Equals(IdentityId, 0) &&
                Equals(IdentityUid, Guid.Empty) &&
                Equals(CreateDt, default) &&
                Equals(ChangeDt, default) &&
                Equals(IsMarked, false);

        public virtual object Clone() => new BaseEntity()
        {
            IdentityName = IdentityName,
            IdentityId = IdentityId,
            IdentityUid = IdentityUid,
            CreateDt = CreateDt,
            ChangeDt = ChangeDt,
            IsMarked = IsMarked,
        };

        #endregion
    }
}
