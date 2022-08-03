// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "OrderTypes".
/// </summary>
public class OrderTypeEntity : BaseEntity
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// Identity name.
    /// </summary>
    public static ColumnName IdentityName => ColumnName.Id;
    public virtual string Description { get; set; }

    public OrderTypeEntity() : this(0)
    {
        //
    }

    public OrderTypeEntity(long id) : base(id)
    {
        Description = string.Empty;
    }

    #endregion

    #region Public and private methods

    public override string ToString() =>
	    $"{nameof(IdentityId)}: {IdentityId}. " +
		base.ToString() +
        $"{nameof(Description)}: {Description}. ";

    public virtual bool Equals(OrderTypeEntity item)
    {
        if (item is null) return false;
        if (ReferenceEquals(this, item)) return true;
        return base.Equals(item) &&
               Equals(Description, item.Description);
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((OrderTypeEntity)obj);
    }

	public override int GetHashCode() => IdentityId.GetHashCode();

	public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        return base.EqualsDefault() &&
               Equals(Description, string.Empty);
    }

    public new virtual object Clone()
    {
        OrderTypeEntity item = new();
        item.Description = Description;
        item.Setup(((BaseEntity)this).CloneCast());
        return item;
    }

    public new virtual OrderTypeEntity CloneCast() => (OrderTypeEntity)Clone();

    #endregion
}
