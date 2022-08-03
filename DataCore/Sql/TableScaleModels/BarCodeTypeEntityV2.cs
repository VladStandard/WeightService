// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using System;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "BARCODE_TYPES_V2".
/// </summary>
public class BarCodeTypeEntityV2 : BaseEntity
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// Identity name.
    /// </summary>
    public static ColumnName IdentityName => ColumnName.Uid;
    public virtual string Name { get; set; }

    public BarCodeTypeEntityV2() : this(Guid.Empty)
    {
        //
    }

    public BarCodeTypeEntityV2(Guid uid) : base(uid)
    {
        Name = string.Empty;
    }

    #endregion

    #region Public and private methods

    public override string ToString() =>
	    $"{nameof(IdentityUid)}: {IdentityUid}. " +
		base.ToString() +
        $"{nameof(Name)}: {Name}. ";

    public virtual bool Equals(BarCodeTypeEntityV2 item)
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
        return Equals((BarCodeTypeEntityV2)obj);
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
        BarCodeTypeEntityV2 item = new();
        item.Name = Name;
        item.Setup(((BaseEntity)this).CloneCast());
        return item;
    }

    public new virtual BarCodeTypeEntityV2 CloneCast() => (BarCodeTypeEntityV2)Clone();

    #endregion
}
