// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore.DAL.Models;
using System;

namespace DataProjectsCore.DAL.TableScaleModels
{
    /// <summary>
    /// Таблица "Ресурсы принтеров".
    /// </summary>
    public class PrinterResourceEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual PrinterEntity Printer { get; set; } = new PrinterEntity();
        public virtual TemplateResourceEntity Resource { get; set; } = new TemplateResourceEntity();
        public virtual string Description { get; set; } = string.Empty;
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime ModifiedDate { get; set; }

        #endregion

        #region Constructor and destructor

        public PrinterResourceEntity()
        {
            PrimaryColumn = new PrimaryColumnEntity(ColumnName.Id);
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            string? strPrinter = Printer != null ? Printer.Id.ToString() : "null";
            string? strResource = Resource != null ? Resource.Id.ToString() : "null";
            return base.ToString() +
                   $"{nameof(Printer)}: {strPrinter}. " +
                   $"{nameof(Resource)}: {strResource}. " +
                   $"{nameof(Description)}: {Description}. " +
                   $"{nameof(CreateDate)}: {CreateDate}. " +
                   $"{nameof(ModifiedDate)}: {ModifiedDate}. ";
        }

        public virtual bool Equals(PrinterResourceEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                   Printer.Equals(entity.Printer) &&
                   Resource.Equals(entity.Resource) &&
                   Equals(Description, entity.Description);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((PrinterResourceEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new PrinterResourceEntity());
        }

        public new virtual bool EqualsDefault()
        {
            if (Printer != null && !Printer.EqualsDefault())
                return false;
            if (Resource != null && !Resource.EqualsDefault())
                return false;
            return base.EqualsDefault() &&
                   Equals(Description, default(string)) &&
                   Equals(CreateDate, default(DateTime)) &&
                   Equals(ModifiedDate, default(DateTime));
        }

        public override object Clone()
        {
            return new PrinterResourceEntity
            {
                PrimaryColumn = (PrimaryColumnEntity)PrimaryColumn.Clone(),
                Id = Id,
                Printer = (PrinterEntity)Printer.Clone(),
                Resource = (TemplateResourceEntity)Resource.Clone(),
                Description = Description,
                CreateDate = CreateDate,
                ModifiedDate = ModifiedDate,
            };
        }

        #endregion
    }
}
