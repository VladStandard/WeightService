// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "ZebraPrinterType".
/// </summary>
public class PrinterTypeEntity : BaseEntity
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// Identity name.
    /// </summary>
    public static ColumnName IdentityName => ColumnName.Id;
    public virtual string Name { get; set; }

    public PrinterTypeEntity() : this(0)
    {
        //
    }

    public PrinterTypeEntity(long id) : base(id)
    {
        Name = string.Empty;
    }

    #endregion

    #region Public and private methods

    public override string ToString() =>
	    $"{nameof(IdentityId)}: {IdentityId}. " +
		base.ToString() +
        $"{nameof(Name)}: {Name}. ";

    public virtual bool Equals(PrinterTypeEntity item)
    {
        if (item is null) return false;
        if (ReferenceEquals(this, item)) return true;
        return base.Equals(item) &&
               Equals(Name, item.Name);
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((PrinterTypeEntity)obj);
    }

	public override int GetHashCode() => IdentityId.GetHashCode();

	public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        return base.EqualsDefault() &&
               Equals(Name, string.Empty);
    }

    public new virtual object Clone()
    {
        PrinterTypeEntity item = new();
        item.Name = Name;
        item.Setup(((BaseEntity)this).CloneCast());
        return item;
    }

    public new virtual PrinterTypeEntity CloneCast() => (PrinterTypeEntity)Clone();

    #endregion
}
