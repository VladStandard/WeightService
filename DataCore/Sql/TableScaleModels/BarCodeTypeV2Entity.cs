// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "BARCODE_TYPES_V2".
/// </summary>
public class BarCodeTypeV2Entity : BaseEntity
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// Identity name.
    /// </summary>
    public static ColumnName IdentityName => ColumnName.Uid;
    public virtual string Name { get; set; }

    public BarCodeTypeV2Entity() : this(Guid.Empty)
    {
        //
    }

    public BarCodeTypeV2Entity(Guid uid) : base(uid)
    {
        Name = string.Empty;
    }

    #endregion

    #region Public and private methods

    public override string ToString() =>
	    $"{nameof(IdentityUid)}: {IdentityUid}. " +
		base.ToString() +
        $"{nameof(Name)}: {Name}. ";

    public virtual bool Equals(BarCodeTypeV2Entity item)
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
        return Equals((BarCodeTypeV2Entity)obj);
    }

	public override int GetHashCode() => IdentityUid.GetHashCode();

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
        BarCodeTypeV2Entity item = new();
        item.Name = Name;
        item.Setup(((BaseEntity)this).CloneCast());
        return item;
    }

    public new virtual BarCodeTypeV2Entity CloneCast() => (BarCodeTypeV2Entity)Clone();

    #endregion
}
