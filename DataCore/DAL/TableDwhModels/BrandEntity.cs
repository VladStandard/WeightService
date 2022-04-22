﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using DataCore.DAL.Utils;

namespace DataCore.DAL.TableDwhModels
{
    public class BrandEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual string Name { get; set; }
        public virtual string Code { get; set; }
        public virtual int StatusId { get; set; }
        public virtual InformationSystemEntity InformationSystem { get; set; }
        public virtual byte[] CodeInIs { get; set; }

        #endregion

        #region Constructor and destructor

        public BrandEntity() : this(0)
        {
            //
        }

        public BrandEntity(long id) : base(id)
        {
            Name = string.Empty;
            Code = string.Empty;
            StatusId = 0;
            InformationSystem = new();
            CodeInIs = new byte[0];
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            string strInformationSystem = InformationSystem != null ? InformationSystem.IdentityId.ToString() : "null";
            return base.ToString() +
                   $"{nameof(Code)}: {Code}. " +
                   $"{nameof(Name)}: {Name}. " +
                   $"{nameof(StatusId)}: {StatusId}. " +
                   $"{nameof(InformationSystem)}: {strInformationSystem}. " +
                   $"{nameof(CodeInIs)}.Length: {CodeInIs?.Length ?? 0}. ";
        }

        public virtual bool Equals(BrandEntity item)
        {
            if (item is null) return false;
            if (ReferenceEquals(this, item)) return true;
            return base.Equals(item) &&
                   Equals(Code, item.Code) &&
                   Equals(Name, item.Name) &&
                   Equals(StatusId, item.StatusId) &&
                   Equals(InformationSystem, item.InformationSystem) &&
                   Equals(CodeInIs, item.CodeInIs);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((BrandEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new BrandEntity());
        }

        public new virtual bool EqualsDefault()
        {
            if (InformationSystem != null && !InformationSystem.EqualsDefault())
                return false;
            return base.EqualsDefault() &&
                   Equals(Code, string.Empty) &&
                   Equals(Name, string.Empty) &&
                   Equals(StatusId, 0) &&
                   Equals(CodeInIs, new byte[0]);
        }

        public override object Clone()
        {
            BrandEntity item = (BrandEntity)base.Clone();
            item.Code = Code;
            item.Name = Name;
            item.StatusId = StatusId;
            item.InformationSystem = (InformationSystemEntity)InformationSystem.Clone();
            item.CodeInIs = DataUtils.CloneBytes(CodeInIs);
            return item;
        }

        #endregion
    }
}
