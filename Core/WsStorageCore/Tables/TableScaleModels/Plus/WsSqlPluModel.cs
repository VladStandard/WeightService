// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable VirtualMemberCallInConstructor

using WsStorageCore.Tables.TableRef1cModels.Brands;
namespace WsStorageCore.Tables.TableScaleModels.Plus;

/// <summary>
/// Table "PLUS".
/// </summary>
[DebuggerDisplay("{ToString()}")]
public class WsSqlPluModel : WsSqlTable1CBase
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
    public virtual WsSqlBundleModel Bundle { get; set; }
    public virtual WsSqlBrandModel Brand { get; set; }
    public override string DisplayName => $"{Number} | {Name}";
    
    public WsSqlPluModel() : base(WsSqlEnumFieldIdentity.Uid)
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

    public WsSqlPluModel(WsSqlPluModel item) : base(item)
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

    public override string ToString() => $"{Number} | {Name} | {Uid1C} | {GetIsMarked()} | {GetIsGroup()} | {Code}";

    public virtual string GetIsGroup() => IsGroup? "Is group" : "Is not group";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlPluModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(IsGroup, default(bool)) &&
        Equals(ParentGuid, Guid.Empty) &&
        Equals(Code, string.Empty) &&
        Equals(Number, default(short)) &&
        Equals(FullName, string.Empty) &&
        Equals(ShelfLifeDays, default(byte)) &&
        Equals(Gtin, string.Empty) &&
        Equals(Ean13, string.Empty) &&
        Equals(Itf14, string.Empty) &&
        Equals(IsCheckWeight, false);

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(IsGroup), IsGroup);
        info.AddValue(nameof(ParentGuid), ParentGuid);
        info.AddValue(nameof(Number), Number);
        info.AddValue(nameof(FullName), FullName);
        info.AddValue(nameof(ShelfLifeDays), ShelfLifeDays);
        info.AddValue(nameof(Gtin), Gtin);
        info.AddValue(nameof(Ean13), Ean13);
        info.AddValue(nameof(Itf14), Itf14);
        info.AddValue(nameof(IsCheckWeight), IsCheckWeight);
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Code = WsLocaleCore.Sql.SqlItemFieldCode;
        Number = 100;
        FullName = WsLocaleCore.Sql.SqlItemFieldFullName;
        Gtin = WsLocaleCore.Sql.SqlItemFieldGtin;
        Ean13 = WsLocaleCore.Sql.SqlItemFieldEan13;
        Itf14 = WsLocaleCore.Sql.SqlItemFieldItf14;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlPluModel item) =>
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
    
    public virtual void UpdateGtin()
    {
        Gtin = IsCheckWeight ? "0" + Ean13 : Itf14;
    }

    #endregion
}