// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;

namespace DataCore.Sql.TableScaleModels
{
    /// <summary>
    /// Table "PrinterResources".
    /// </summary>
    public class PrinterResourceEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual PrinterEntity Printer { get; set; }
        public virtual TemplateResourceEntity Resource { get; set; }
        public virtual string Description { get; set; }

        #endregion

        #region Constructor and destructor

        public PrinterResourceEntity() : this(0)
        {
            //
        }

        public PrinterResourceEntity(long id) : base(id)
        {
            Printer = new();
            Resource = new();
            Description = string.Empty;
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            string strPrinter = Printer != null ? Printer.IdentityId.ToString() : "null";
            string strResource = Resource != null ? Resource.IdentityId.ToString() : "null";
            return base.ToString() +
                   $"{nameof(Printer)}: {strPrinter}. " +
                   $"{nameof(Resource)}: {strResource}. " +
                   $"{nameof(Description)}: {Description}. ";
        }

        public virtual bool Equals(PrinterResourceEntity item)
        {
            if (item is null) return false;
            if (ReferenceEquals(this, item)) return true;
            if (Printer != null && item.Printer != null && !Printer.Equals(item.Printer))
                return false;
            if (Resource != null && item.Resource != null && !Resource.Equals(item.Resource))
                return false;
            return base.Equals(item) &&
                   Equals(Description, item.Description);
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
            return base.EqualsDefault(IdentityName) &&
                   Equals(Description, string.Empty);
        }

        public new virtual object Clone()
        {
            PrinterResourceEntity item = new();
            item.Printer = Printer.CloneCast();
            item.Resource = Resource.CloneCast();
            item.Description = Description;
            item.Setup(((BaseEntity)this).CloneCast());
            return item;
        }

        public new virtual PrinterResourceEntity CloneCast() => (PrinterResourceEntity)Clone();

        #endregion
    }
}
