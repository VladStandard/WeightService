// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using System;

namespace DataCore.DAL.TableScaleModels
{
    /// <summary>
    /// Таблица "Ресурсы шаблонов этикеток".
    /// </summary>
    public class TemplateResourceEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual string Name { get; set; } = string.Empty;
        public virtual string Description { get; set; } = string.Empty;
        public virtual string Type { get; set; } = string.Empty;
        public virtual ImageDataEntity ImageData { get; set; } = new();
        public virtual byte[] ImageDataValue { get => ImageData.Value; set => ImageData.Value = value; }
        public virtual Guid? IdRRef { get; set; }

        #endregion

        #region Constructor and destructor

        public TemplateResourceEntity()
        {
            PrimaryColumn = new PrimaryColumnEntity(ColumnName.Id);
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            return base.ToString() +
                   $"{nameof(Name)}: {Name}. " +
                   $"{nameof(Description)}: {Description}. " +
                   $"{nameof(Type)}: {Type}. " +
                   $"{nameof(ImageData)}: {ImageData}. " +
                   $"{nameof(IdRRef)}: {IdRRef}. ";
        }

        public virtual bool Equals(TemplateResourceEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                   Equals(Name, entity.Name) &&
                   Equals(Description, entity.Description) &&
                   Equals(Type, entity.Type) &&
                   Equals(IdRRef, entity.IdRRef) &&
                   Equals(ImageData, entity.ImageData);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((TemplateResourceEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new TemplateResourceEntity());
        }

        public new virtual bool EqualsDefault()
        {
            return base.EqualsDefault() &&
                   Equals(Name, default(string)) &&
                   Equals(Description, default(string)) &&
                   Equals(Type, default(string)) &&
                   Equals(ImageData, new()) &&
                   Equals(IdRRef, default(Guid?));
        }

        public override object Clone()
        {
            return new TemplateResourceEntity
            {
                PrimaryColumn = (PrimaryColumnEntity)PrimaryColumn.Clone(),
                CreateDt = CreateDt,
                ChangeDt = ChangeDt,
                IsMarked = IsMarked,
                Name = Name,
                Description = Description,
                Type = Type,
                ImageData = new(ImageData.Value),
                IdRRef = IdRRef,
            };
        }

        #endregion
    }
}
