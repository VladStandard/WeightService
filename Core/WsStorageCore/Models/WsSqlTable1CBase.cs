// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Models;

/// <summary>
/// SQL table 1C model.
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlTable1CBase : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlIgnore] public virtual Guid Uid1C { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlTable1CBase() : base()
    {
        Uid1C = Guid.Empty;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlTable1CBase(WsSqlFieldIdentity identityName) : base(identityName)
    {
        Uid1C = Guid.Empty;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    protected WsSqlTable1CBase(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Uid1C = info.GetValue(nameof(Uid1C), typeof(Guid)) is Guid uid1C ? uid1C : Guid.Empty;
    }

    #endregion

    #region Public and private methods - override

    public virtual bool Equals(WsSqlTable1CBase item) =>
        ReferenceEquals(this, item) || base.Equals(item) && Equals(Uid1C, item.Uid1C);

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlTable1CBase)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Uid1C), Uid1C);
    }

    #endregion

    #region Public and private methods - virtual

    public override bool EqualsNew() => Equals(new WsSqlTable1CBase());

    public override bool EqualsDefault() =>
        base.EqualsDefault() && Equals(Uid1C, Guid.Empty);

    public override object Clone()
    {
        WsSqlTable1CBase item = new();
        item.CloneSetup(base.CloneCast());
        item.Uid1C = Uid1C;
        return item;
    }

    public new virtual WsSqlTable1CBase CloneCast() =>
        (WsSqlTable1CBase)Clone();

    public virtual void CloneSetup(WsSqlTable1CBase item)
    {
        if (item is WsSqlTableBase sqlTable)
            base.CloneSetup(sqlTable);
    }

    public virtual void UpdateProperties(WsSqlTable1CBase item)
    {
        base.UpdateProperties(item);
        Uid1C = item.Uid1C;
    }

    #endregion
}