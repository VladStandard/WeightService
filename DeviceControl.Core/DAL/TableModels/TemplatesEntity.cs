using System;
using System.Text;

namespace DeviceControl.Core.DAL.TableModels
{
    public class TemplatesEntity : BaseEntity
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

        public virtual bool Equals(TemplatesEntity entity)
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
            return Equals((TemplatesEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new TemplatesEntity());
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
            return new TemplatesEntity
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
