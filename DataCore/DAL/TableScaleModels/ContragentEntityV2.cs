// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using System;

namespace DataCore.DAL.TableScaleModels
{
    /// <summary>
    /// Таблица "Контрагенты".
    /// </summary>
    public class ContragentEntityV2 : BaseEntity
    {
        #region Public and private fields and properties

        public virtual DateTime CreateDt { get; set; } = default;
        public virtual DateTime ChangeDt { get; set; } = default;
        public virtual bool IsMarked { get; set; } = false;
        public virtual string Name { get; set; } = string.Empty;
        public virtual string FullName { get; set; } = string.Empty;
        public virtual Guid? IdRRef { get; set; } = null;
        public virtual string IdRRefAsString
        {
            get => IdRRef.ToString();
            set => IdRRef = Guid.Parse(value);
        }
        public virtual int DwhId { get; set; } = 0;
        public virtual string Xml { get; set; } = string.Empty;

        #endregion

        #region Constructor and destructor

        public ContragentEntityV2()
        {
            PrimaryColumn = new PrimaryColumnEntity(ColumnName.Uid);
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            return base.ToString() +
                $"{nameof(CreateDt)}: {CreateDt}. " +
                $"{nameof(ChangeDt)}: {ChangeDt}. " +
                $"{nameof(IsMarked)}: {IsMarked}." +
                $"{nameof(Name)}: {Name}. " +
                $"{nameof(FullName)}: {FullName}. " +
                $"{nameof(IdRRef)}: {IdRRef}. " +
                $"{nameof(DwhId)}: {DwhId}. " +
                $"{nameof(Xml)}.Length: {Xml?.Length ?? 0}. ";
        }

        public virtual bool Equals(ContragentEntityV2 entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                   Equals(CreateDt, entity.CreateDt) &&
                   Equals(ChangeDt, entity.ChangeDt) &&
                   Equals(IsMarked, entity.IsMarked) &&
                   Equals(Name, entity.Name) &&
                   Equals(FullName, entity.FullName) &&
                   Equals(IdRRef, entity.IdRRef) &&
                   Equals(DwhId, entity.DwhId) &&
                   Equals(Xml, entity.Xml);
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
                   Equals(CreateDt, default(DateTime)) &&
                   Equals(ChangeDt, default(DateTime)) &&
                   Equals(IsMarked, false) &&
                   Equals(Name, string.Empty) &&
                   Equals(FullName, string.Empty) &&
                   Equals(IdRRef, null) &&
                   Equals(DwhId, 0) &&
                   Equals(Xml, string.Empty);
        }

        public override object Clone()
        {
            return new ContragentEntityV2
            {
                PrimaryColumn = (PrimaryColumnEntity)PrimaryColumn.Clone(),
                Uid = Uid,
                CreateDt = CreateDt,
                ChangeDt = ChangeDt,
                IsMarked = IsMarked,
                Name = Name,
                FullName = FullName,
                IdRRef = IdRRef,
                DwhId = DwhId,
                Xml = Xml,
            };
        }

        #endregion
    }
}
