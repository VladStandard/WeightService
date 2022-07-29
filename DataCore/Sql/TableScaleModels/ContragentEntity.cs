// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using System;

namespace DataCore.Sql.TableScaleModels
{
    /// <summary>
    /// Table "Contragents".
    /// </summary>
    [Obsolete(@"Use ContragentEntityV2")]
    public class ContragentEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual string Name { get; set; }
        public virtual string SerializedRepresentationObject { get; set; }

        #endregion

        #region Constructor and destructor

        public ContragentEntity() : this(0)
        {
            //
        }

        public ContragentEntity(long id) : base(id)
        {
            Name = string.Empty;
            SerializedRepresentationObject = string.Empty;
        }

        #endregion

        #region Public and private methods

        public override string ToString() =>
            base.ToString() +
            $"{nameof(Name)}: {Name}. " +
            $"{nameof(SerializedRepresentationObject)}.Length: {SerializedRepresentationObject?.Length ?? 0}. ";

        public virtual bool Equals(ContragentEntity item)
        {
            if (item is null) return false;
            if (ReferenceEquals(this, item)) return true;
            return base.Equals(item) &&
                   Equals(Name, item.Name) &&
                   Equals(SerializedRepresentationObject, item.SerializedRepresentationObject);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ContragentEntity)obj);
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
                   Equals(Name, string.Empty) &&
                   Equals(SerializedRepresentationObject, string.Empty);
        }

        public new virtual object Clone()
        {
            ContragentEntity item = new();
            item.Name = Name;
            item.SerializedRepresentationObject = SerializedRepresentationObject;
            item.Setup(((BaseEntity)this).CloneCast());
            return item;
        }

        public new virtual ContragentEntity CloneCast() => (ContragentEntity)Clone();

        #endregion
    }
}
