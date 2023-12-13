using System;

namespace Ws.StorageCore.Entities.SchemaRef1c.Plus;

/// <summary>
/// Table "PLUS".
/// </summary>
[DebuggerDisplay("{ToString()}")]
public class SqlPluEntity : SqlTable1CBase
{
    #region Public and private fields, properties, constructor
    
    public virtual bool IsGroup { get; set; }
    public virtual short Number { get; set; }
    public virtual string Code { get; set; }
    public virtual string FullName { get; set; }
    public virtual byte ShelfLifeDays { get; set; }
    public virtual string Gtin { get; set; }
    public virtual string Ean13 { get; set; }
    public virtual string Itf14 { get; set; }
    public virtual bool IsCheckWeight { get; set; } 
    public virtual Guid ParentGuid { get; set; }
    public virtual Guid CategoryGuid { get; set; }
    public virtual SqlBundleEntity Bundle { get; set; }
    public virtual SqlBrandEntity Brand { get; set; }
    public override string DisplayName => $"{Number} | {Name}";
    
    public SqlPluEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        CategoryGuid = Guid.Empty;
        Code = string.Empty;
        Ean13 = string.Empty;
        FullName = string.Empty;
        Gtin = string.Empty;
        IsCheckWeight = false;
        IsGroup = default;
        Itf14 = string.Empty;
        Number = default;
        ParentGuid = Guid.Empty;
        ShelfLifeDays = default;
        Brand = new();
        Bundle = new();
    }

    public SqlPluEntity(SqlPluEntity item) : base(item)
    {
        IsGroup = item.IsGroup;
        ParentGuid = item.ParentGuid;
        Code = item.Code;
        Number = item.Number;
        FullName = item.FullName;
        ShelfLifeDays = item.ShelfLifeDays;
        Gtin = item.Gtin;
        Ean13 = item.Ean13;
        Itf14 = item.Itf14;
        IsCheckWeight = item.IsCheckWeight;
        Brand = new(item.Brand);
        Bundle = new(item.Bundle);
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() => $"{Number} | {Name} | {Uid1C} | {GetIsGroup()} | {Code}";

    public virtual string GetIsGroup() => IsGroup? "Is group" : "Is not group";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SqlPluEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();
    
    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(SqlPluEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&//-V3130
        Equals(IsGroup, item.IsGroup) &&
        Equals(ParentGuid, item.ParentGuid) &&
        Equals(Code, item.Code) &&
        Equals(Number, item.Number) &&
        Equals(FullName, item.FullName) &&
        Equals(ShelfLifeDays, item.ShelfLifeDays) &&
        Equals(Gtin, item.Gtin) &&
        Equals(Ean13, item.Ean13) &&
        Equals(Itf14, item.Itf14) &&
        Equals(IsCheckWeight, item.IsCheckWeight);

    #endregion
}