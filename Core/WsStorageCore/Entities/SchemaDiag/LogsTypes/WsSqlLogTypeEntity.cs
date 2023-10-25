namespace WsStorageCore.Entities.SchemaDiag.LogsTypes;

[DebuggerDisplay("{ToString()}")]
public class WsSqlLogTypeEntity : WsSqlEntityBase
{
    #region Public and private fields, properties, constructor

    public virtual byte Number { get; set; }
    public virtual string Icon { get; set; }

    public WsSqlLogTypeEntity() : base(WsSqlEnumFieldIdentity.Uid)
    {
        Number = 0x00;
        Icon = string.Empty;
    }
    
    public WsSqlLogTypeEntity(WsSqlLogTypeEntity item) : base(item)
    {
        Number = item.Number;
        Icon = item.Icon;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{GetIsMarked()} | {Number} | {Icon}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlLogTypeEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(Number, (byte)0x00) &&
        Equals(Icon, string.Empty);

    public override void FillProperties()
    {
        base.FillProperties();
        Icon = WsLocaleCore.Sql.SqlItemFieldIcon;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlLogTypeEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(Number, item.Number) &&
        Equals(Icon, item.Icon);

    #endregion
}
