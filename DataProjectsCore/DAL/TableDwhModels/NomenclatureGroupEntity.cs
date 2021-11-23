// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore.DAL.Models;
using System;

namespace DataProjectsCore.DAL.TableDwhModels
{
    public class NomenclatureGroupEntity : BaseIdEntity
    {
        #region Public and private fields and properties

        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime Dlm { get; set; }
        public virtual string Name { get; set; }
        public virtual int StatusId { get; set; }
        public virtual InformationSystemEntity InformationSystem { get; set; } = new InformationSystemEntity();
        public virtual byte[] CodeInIs { get; set; }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            var strInformationSystem = InformationSystem != null ? InformationSystem.Id.ToString() : "null";
            return base.ToString() +
                   $"{nameof(CreateDate)}: {CreateDate}. " +
                   $"{nameof(Dlm)}: {Dlm}. " +
                   $"{nameof(Name)}: {Name}. " +
                   $"{nameof(StatusId)}: {StatusId}. " +
                   $"{nameof(InformationSystem)}: {strInformationSystem}. " +
                   $"{nameof(CodeInIs)}.Length: {CodeInIs?.Length ?? 0}. ";
        }

        public virtual bool Equals(NomenclatureGroupEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                   Equals(CreateDate, entity.CreateDate) &&
                   Equals(Dlm, entity.Dlm) &&
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
            return Equals((NomenclatureGroupEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new NomenclatureGroupEntity());
        }

        public new virtual bool EqualsDefault()
        {
            if (InformationSystem != null && !InformationSystem.EqualsDefault())
                return false;
            return base.EqualsDefault() &&
                   Equals(CreateDate, default(DateTime)) &&
                   Equals(Dlm, default(DateTime)) &&
                   Equals(Name, default(string)) &&
                   Equals(StatusId, default(int)) &&
                   Equals(CodeInIs, default(byte[]));
        }

        public override object Clone()
        {
            return new NomenclatureGroupEntity
            {
                CreateDate = CreateDate,
                Dlm = Dlm,
                Id = Id,
                Name = Name,
                StatusId = StatusId,
                InformationSystem = (InformationSystemEntity)InformationSystem.Clone(),
                CodeInIs = CloneBytes(CodeInIs),
            };
        }

        #endregion
    }
}
