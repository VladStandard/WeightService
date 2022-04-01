// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using DataCore.DAL.Utils;
using System;

namespace DataCore.DAL.TableDwhModels
{
    public class BrandEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual string Name { get; set; }
        public virtual string Code { get; set; }
        public virtual int StatusId { get; set; }
        public virtual InformationSystemEntity InformationSystem { get; set; } = new InformationSystemEntity();
        public virtual byte[] CodeInIs { get; set; }

        #endregion

        #region Constructor and destructor

        public BrandEntity()
        {
            PrimaryColumn = new PrimaryColumnEntity(ColumnName.Id);
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            var strInformationSystem = InformationSystem != null ? InformationSystem.Id.ToString() : "null";
            return base.ToString() +
                   $"{nameof(Code)}: {Code}. " +
                   $"{nameof(Name)}: {Name}. " +
                   $"{nameof(StatusId)}: {StatusId}. " +
                   $"{nameof(InformationSystem)}: {strInformationSystem}. " +
                   $"{nameof(CodeInIs)}.Length: {CodeInIs?.Length ?? 0}. ";
        }

        public virtual bool Equals(BrandEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                   Equals(Code, entity.Code) &&
                   Equals(Name, entity.Name) &&
                   Equals(StatusId, entity.StatusId) &&
                   Equals(InformationSystem, entity.InformationSystem) &&
                   Equals(CodeInIs, entity.CodeInIs);
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
                   Equals(Code, default(string)) &&
                   Equals(Name, default(string)) &&
                   Equals(StatusId, default(int)) &&
                   Equals(CodeInIs, default(byte[]));
        }

        public override object Clone()
        {
            return new BrandEntity
            {
                PrimaryColumn = (PrimaryColumnEntity)PrimaryColumn.Clone(),
                CreateDt = CreateDt,
                ChangeDt = ChangeDt,
                IsMarked = IsMarked,
                Code = Code,
                Name = Name,
                StatusId = StatusId,
                InformationSystem = (InformationSystemEntity)InformationSystem.Clone(),
                CodeInIs = DataUtils.CloneBytes(CodeInIs),
            };
        }

        #endregion
    }
}
