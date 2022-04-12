// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace DataCore.DAL.Models
{
    public enum ColumnName
    {
        Default,
        Id,
        Uid,
    }

    public class BaseEntity : ICloneable
    {
        #region Public and private fields and properties

        public virtual bool IsMarked { get; set; }
        public virtual ColumnName IdentityName { get; private set; }
        public virtual DateTime ChangeDt { get; set; }
        public virtual DateTime CreateDt { get; set; }
        public virtual Guid IdentityUid { get; set; }
        public virtual long IdentityId { get; set; }
        public virtual string IdentityUidStr { get => IdentityUid.ToString(); set => IdentityUid = Guid.TryParse(value, out Guid uid) ? uid : Guid.Empty; }

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
            string strCreateDt = CreateDt != null ? CreateDt.ToString() : "null";
            string strChangeDt = ChangeDt != null ? ChangeDt.ToString() : "null";
            return
                $"{nameof(IdentityName)}: {strIdenttity}. " +
                $"{nameof(CreateDt)}: {strCreateDt}. " +
                $"{nameof(ChangeDt)}: {strChangeDt}. " +
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

        public virtual bool Equals(BaseEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return
                IdentityName.Equals(entity.IdentityName) &&
                IdentityId.Equals(entity.IdentityId) &&
                IdentityUid.Equals(entity.IdentityUid) &&
                Equals(CreateDt, entity.CreateDt) &&
                Equals(ChangeDt, entity.ChangeDt) &&
                Equals(IsMarked, entity.IsMarked);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((BaseEntity)obj);
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
