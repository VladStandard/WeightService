// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using DataCore.DAL.Utils;
using System;
using System.Text;

namespace DataCore.DAL.TableScaleModels
{
    /// <summary>
    /// Таблица "Шаблоны этикеток".
    /// </summary>
    public class TemplateEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual string CategoryId { get; set; } = string.Empty;
        public virtual Guid? IdRRef { get; set; }
        public virtual string Title { get; set; } = string.Empty;
        public virtual ImageDataEntity ImageData { get; set; } = new();
        public virtual byte[] ImageDataValue { get => ImageData.Value; set => ImageData.Value = value; }

        #endregion

        #region Constructor and destructor

        public TemplateEntity()
        {
            PrimaryColumn = new PrimaryColumnEntity(ColumnName.Id);
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            return base.ToString() +
                   $"{nameof(CategoryId)}: {CategoryId}. " +
                   $"{nameof(IdRRef)}: {IdRRef}. " +
                   $"{nameof(Title)}: {Title}. " +
                   $"{nameof(ImageData)}: {ImageData}. ";
        }

        public virtual bool Equals(TemplateEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                   Equals(CategoryId, entity.CategoryId) &&
                   Equals(IdRRef, entity.IdRRef) &&
                   Equals(Title, entity.Title) &&
                   Equals(ImageData, entity.ImageData);
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
                   Equals(CategoryId, default(string)) &&
                   Equals(IdRRef, default(Guid?)) &&
                   Equals(Title, default(string)) &&
                   Equals(ImageData, new());
        }

        public override object Clone()
        {
            return new TemplateEntity
            {
                PrimaryColumn = (PrimaryColumnEntity)PrimaryColumn.Clone(),
                CreateDt = CreateDt,
                ChangeDt = ChangeDt,
                IsMarked = IsMarked,
                CategoryId = CategoryId,
                IdRRef = IdRRef,
                Title = Title,
                ImageData = new(ImageData.Value),
            };
        }

        #endregion
    }
}
