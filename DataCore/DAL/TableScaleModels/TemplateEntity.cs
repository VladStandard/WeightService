// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.DAL;
using System;
using System.Text;

namespace DataCore.DAL.TableModels
{
    public class TemplateEntity : BaseIdEntity
    {
        #region Public and private fields and properties

        public virtual DateTime? CreateDate { get; set; }
        public virtual DateTime? ModifiedDate { get; set; }
        public virtual string CategoryId { get; set; }
        public virtual Guid? IdRRef { get; set; }
        public virtual string Title { get; set; }
        public virtual byte[] ImageData { get; set; }
        public virtual string ImageDataStringAscii
        {
            get => ImageData == null ? string.Empty : Encoding.Default.GetString(ImageData);
            set => ImageData = Encoding.Default.GetBytes(value);
        }
        public virtual string ImageDataStringUnicode
        {
            get => ImageData == null ? string.Empty : Encoding.Unicode.GetString(ImageData);
            set => ImageData = Encoding.Unicode.GetBytes(value);
        }
        public virtual string ImageDataInfo
        {
            get => GetBytesLength(ImageData);
            set => _ = value;
        }
        public virtual bool Marked { get; set; }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            return base.ToString() + 
                   $"{nameof(CreateDate)}: {CreateDate}. " + 
                   $"{nameof(ModifiedDate)}: {ModifiedDate}. " + 
                   $"{nameof(CategoryId)}: {CategoryId}. " + 
                   $"{nameof(IdRRef)}: {IdRRef}. " + 
                   $"{nameof(Title)}: {Title}. " + 
                   $"{nameof(ImageDataInfo)}: {ImageDataInfo}. " + 
                   $"{nameof(Marked)}: {Marked}. ";
        }

        public virtual bool Equals(TemplateEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                   Equals(CreateDate, entity.CreateDate) &&
                   Equals(ModifiedDate, entity.ModifiedDate) &&
                   Equals(CategoryId, entity.CategoryId) &&
                   Equals(IdRRef, entity.IdRRef) &&
                   Equals(Title, entity.Title) &&
                   Equals(ImageData, entity.ImageData) &&
                   Equals(Marked, entity.Marked);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((TemplateEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new TemplateEntity());
        }

        public new virtual bool EqualsDefault()
        {
            return base.EqualsDefault() &&
                   Equals(CreateDate, default(DateTime?)) &&
                   Equals(ModifiedDate, default(DateTime?)) && 
                   Equals(CategoryId, default(string)) &&
                   Equals(IdRRef, default(Guid?)) &&
                   Equals(Title, default(string)) &&
                   Equals(ImageData, default(byte[])) &&
                   Equals(Marked, default(bool));
        }

        public override object Clone()
        {
            return new TemplateEntity
            {
                Id = Id,
                CreateDate = CreateDate,
                ModifiedDate = ModifiedDate,
                CategoryId = CategoryId,
                IdRRef = IdRRef,
                Title = Title,
                ImageData = CloneBytes(ImageData),
                Marked = Marked,
            };
        }
        
        #endregion
    }
}
