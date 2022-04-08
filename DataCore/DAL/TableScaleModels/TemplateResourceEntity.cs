// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using System;

namespace DataCore.DAL.TableScaleModels
{
    /// <summary>
    /// Table "TemplateResources".
    /// </summary>
    public class TemplateResourceEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string Type { get; set; }
        public virtual ImageDataEntity ImageData { get; set; }
        public virtual byte[] ImageDataValue { get => ImageData.Value; set => ImageData.Value = value; }
        public virtual Guid IdRRef { get; set; }

        #endregion

        #region Constructor and destructor

        public TemplateResourceEntity() : this(0)
        {
            //
        }

        public TemplateResourceEntity(long id) : base(id)
        {
            Name = string.Empty;
            Description = string.Empty;
            Type = string.Empty;
            ImageData = new();
            IdRRef = Guid.Empty;
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
                   Equals(Name, string.Empty) &&
                   Equals(Description, string.Empty) &&
                   Equals(Type, string.Empty) &&
                   Equals(ImageData, new()) &&
                   Equals(IdRRef, Guid.Empty);
        }

        public override object Clone()
        {
            TemplateResourceEntity item = (TemplateResourceEntity)base.Clone();
            item.Name = Name;
            item.Description = Description;
            item.Type = Type;
            item.ImageData = new(ImageData.Value);
            item.IdRRef = IdRRef;
            return item;
        }

        #endregion
    }
}
