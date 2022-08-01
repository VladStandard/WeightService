// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels;
using System;

namespace DataCore.Sql.Models;

/// <summary>
/// Table "Access".
/// </summary>
public class BaseDummyEntity : BaseEntity
{
    #region Public and private fields, properties, constructor

    public BaseDummyEntity() : this(Guid.Empty)
    {
        //
    }

    public BaseDummyEntity(Guid uid) : base(uid)
    {
        //
    }

    #endregion

    #region Public and private methods

    public override string ToString() =>
        base.ToString();

    public virtual bool Equals(BaseDummyEntity item)
    {
        if (item is null) return false;
        if (ReferenceEquals(this, item)) return true;
        return base.Equals(item);
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((BaseDummyEntity)obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public virtual bool EqualsNew()
    {
        return Equals(new AccessEntity());
    }

    public new virtual bool EqualsDefault()
    {
        return base.EqualsDefault(IdentityName);
    }

    public new virtual object Clone()
    {
        BaseDummyEntity item = new();
        item.Setup(((BaseEntity)this).CloneCast());
        return item;
    }

    public new virtual BaseDummyEntity CloneCast() => (BaseDummyEntity)Clone();

    #endregion
}
