// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using System;

namespace DataCore.DAL.TableScaleModels
{
    /// <summary>
    /// Table "Contragents".
    /// </summary>
    public class ContragentEntityV2 : BaseEntity
    {
        #region Public and private fields and properties

        public virtual string Name { get; set; }
        public virtual string FullName { get; set; }
        public virtual Guid IdRRef { get; set; }
        public virtual string IdRRefAsString
        {
            get => IdRRef.ToString();
            set => IdRRef = Guid.Parse(value);
        }
        public virtual int DwhId { get; set; }
        public virtual string Xml { get; set; }

        #endregion

        #region Constructor and destructor

        public ContragentEntityV2() : this(Guid.Empty)
        {
            //
        }

        public ContragentEntityV2(Guid uid) : base(uid)
        {
            Name = string.Empty;
            FullName = string.Empty;
            IdRRef = Guid.Empty;
            DwhId = 0;
            Xml = string.Empty;
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            return base.ToString() +
                $"{nameof(Name)}: {Name}. " +
                $"{nameof(FullName)}: {FullName}. " +
                $"{nameof(IdRRef)}: {IdRRef}. " +
                $"{nameof(DwhId)}: {DwhId}. " +
                $"{nameof(Xml)}.Length: {Xml?.Length ?? 0}. ";
        }

        public virtual bool Equals(ContragentEntityV2 item)
        {
            if (item is null) return false;
            if (ReferenceEquals(this, item)) return true;
            return base.Equals(item) &&
                   Equals(Name, item.Name) &&
                   Equals(FullName, item.FullName) &&
                   Equals(IdRRef, item.IdRRef) &&
                   Equals(DwhId, item.DwhId) &&
                   Equals(Xml, item.Xml);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ContragentEntityV2)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new ContragentEntityV2());
        }

        public new virtual bool EqualsDefault()
        {
            return base.EqualsDefault() &&
                   Equals(Name, string.Empty) &&
                   Equals(FullName, string.Empty) &&
                   Equals(IdRRef, Guid.Empty) &&
                   Equals(DwhId, 0) &&
                   Equals(Xml, string.Empty);
        }

        public override object Clone()
        {
            ContragentEntityV2 item = (ContragentEntityV2)base.Clone();
            item.Name = Name;
            item.FullName = FullName;
            item.IdRRef = IdRRef;
            item.DwhId = DwhId;
            item.Xml = Xml;
            return item;
        }

        #endregion
    }
}
