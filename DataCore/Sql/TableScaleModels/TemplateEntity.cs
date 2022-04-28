// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using System;
using System.Xml.Serialization;

namespace DataCore.Sql.TableScaleModels
{
    /// <summary>
    /// Table "Templates".
    /// </summary>
    public class TemplateEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual string CategoryId { get; set; }
        public virtual Guid IdRRef { get; set; }
        public virtual string Title { get; set; }
        [XmlIgnore] public virtual ImageDataEntity ImageData { get; set; }
        public virtual byte[] ImageDataValue { get => ImageData.Value; set => ImageData.Value = value; }

        #endregion

        #region Constructor and destructor

        public TemplateEntity() : this(0)
        {
            //
        }

        public TemplateEntity(long id) : base(id)
        {
            CategoryId = string.Empty;
            IdRRef = Guid.Empty;
            Title = string.Empty;
            ImageData = new();
            ImageDataValue = new byte[0];
        }

        #endregion

        #region Public and private methods

        public override string ToString() =>
            base.ToString() +
            $"{nameof(CategoryId)}: {CategoryId}. " +
            $"{nameof(IdRRef)}: {IdRRef}. " +
            $"{nameof(Title)}: {Title}. " +
            $"{nameof(ImageData)}: {ImageData}. ";

        public virtual bool Equals(TemplateEntity item)
        {
            if (item is null) return false;
            if (ReferenceEquals(this, item)) return true;
            if (ImageData != null && item.ImageData != null && !ImageData.Equals(item.ImageData))
                return false;
            return base.Equals(item) &&
                   Equals(CategoryId, item.CategoryId) &&
                   Equals(IdRRef, item.IdRRef) &&
                   Equals(Title, item.Title);
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
            return base.EqualsDefault(IdentityName) &&
                   Equals(CategoryId, string.Empty) &&
                   Equals(IdRRef, Guid.Empty) &&
                   Equals(Title, string.Empty) &&
                   ImageData.EqualsDefault();
        }

        public new virtual object Clone()
        {
            TemplateEntity item = new()
            {
                CategoryId = CategoryId,
                IdRRef = IdRRef,
                Title = Title,
                ImageData = ImageData.CloneCast,
            };
            item.Setup(((BaseEntity)this).CloneCast);
            return item;
        }

        public new virtual TemplateEntity CloneCast => (TemplateEntity)Clone();

        #endregion
    }
}
