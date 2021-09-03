// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore.DAL.Models;
using System;
using System.Text;

namespace DataProjectsCore.DAL.TableScaleModels
{
    public class TemplateResourceEntity : BaseIdEntity
    {
        #region Public and private fields and properties

        public virtual DateTime? CreateDate { get; set; }
        public virtual DateTime? ModifiedDate { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string Type { get; set; }
        public virtual byte[] ImageData { get; set; }
        public virtual string ImageDataString
        {
            get => ImageData == null ? string.Empty : Encoding.Default.GetString(ImageData);
            set => ImageData = Encoding.Default.GetBytes(value);
        }
        public virtual string ImageDataInfo
        {
            get => GetBytesLength(ImageData);
            set => _ = value;
        }
        public virtual Guid? IdRRef { get; set; }
        public virtual bool Marked { get; set; }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            return base.ToString() +
                   $"{nameof(CreateDate)}: {CreateDate}. " +
                   $"{nameof(ModifiedDate)}: {ModifiedDate}. " +
                   $"{nameof(Name)}: {Name}. " +
                   $"{nameof(Description)}: {Description}. " +
                   $"{nameof(Type)}: {Type}. " +
                   $"{nameof(ImageDataString)}: {ImageDataString}. " +
                   $"{nameof(IdRRef)}: {IdRRef}. " +
                   $"{nameof(Marked)}: {Marked}. ";
        }

        public virtual bool Equals(TemplateResourceEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                   Equals(CreateDate, entity.CreateDate) &&
                   Equals(ModifiedDate, entity.ModifiedDate) &&
                   Equals(Name, entity.Name) &&
                   Equals(Description, entity.Description) &&
                   Equals(Name, entity.Name) &&
                   Equals(Type, entity.Type) &&
                   Equals(IdRRef, entity.IdRRef) &&
                   Equals(ImageData, entity.ImageData) &&
                   Equals(Marked, entity.Marked);
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
                   Equals(CreateDate, default(DateTime?)) &&
                   Equals(ModifiedDate, default(DateTime?)) &&
                   Equals(Name, default(string)) &&
                   Equals(Description, default(string)) &&
                   Equals(Type, default(string)) &&
                   Equals(ImageData, default(byte[])) &&
                   Equals(IdRRef, default(Guid?)) &&
                   Equals(Marked, default(bool));
        }

        public override object Clone()
        {
            return new TemplateResourceEntity
            {
                Id = Id,
                CreateDate = CreateDate,
                ModifiedDate = ModifiedDate,
                Name = Name,
                Description = Description,
                Type = Type,
                ImageData = CloneBytes(ImageData),
                IdRRef = IdRRef,
                Marked = Marked,
            };
        }

        #endregion
    }
}
