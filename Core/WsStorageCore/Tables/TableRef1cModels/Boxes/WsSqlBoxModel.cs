namespace WsStorageCore.Tables.TableRef1cModels.Boxes;

[DebuggerDisplay("{ToString()}")]
public class WsSqlBoxModel : WsSqlTable1CBase
{
    #region Public and private fields, properties, constructor

    public virtual decimal Weight { get; set; }

    public WsSqlBoxModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        Weight = 0;
    }

    public WsSqlBoxModel(WsSqlBoxModel item) : base(item)
    {
        Weight = item.Weight;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{Uid1C} | {Name} | {Weight}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlBoxModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public new virtual bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(Weight, (decimal)0);

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlBoxModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Equals(Weight, item.Weight);

    #endregion
}