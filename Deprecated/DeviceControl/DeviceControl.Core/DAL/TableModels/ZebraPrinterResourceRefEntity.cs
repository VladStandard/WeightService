using System;

namespace DeviceControl.Core.DAL.TableModels
{
    public class ZebraPrinterResourceRefEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual ZebraPrinterEntity Printer { get; set; } = new ZebraPrinterEntity();
        public virtual TemplateResourcesEntity Resource { get; set; } = new TemplateResourcesEntity();
        public virtual string Description { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime ModifiedDate { get; set; }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            var strPrinter = Printer != null ? Printer.Id.ToString() : "null";
            var strResource = Resource != null ? Resource.Id.ToString() : "null";
            return base.ToString() +
                   $"{nameof(Printer)}: {strPrinter}. " + 
                   $"{nameof(Resource)}: {strResource}. " +
                   $"{nameof(Description)}: {Description}. " +
                   $"{nameof(CreateDate)}: {CreateDate}. " +
                   $"{nameof(ModifiedDate)}: {ModifiedDate}. ";
        }

        public virtual bool Equals(ZebraPrinterResourceRefEntity entity)
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
            return Equals((ZebraPrinterResourceRefEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new ZebraPrinterResourceRefEntity());
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
            return new ZebraPrinterResourceRefEntity
            {
                Id = Id,
                Printer = (ZebraPrinterEntity)Printer.Clone(),
                Resource = (TemplateResourcesEntity)Resource.Clone(),
                Description = Description,
                CreateDate = CreateDate,
                ModifiedDate = ModifiedDate,
            };
        }
        
        #endregion
    }
}
