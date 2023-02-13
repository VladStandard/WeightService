// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Enums;
using DataCore.Sql.Core.Interfaces;

namespace DataCore.Sql.Tables;

/// <summary>
/// DB table model.
/// </summary>
[Serializable]
[DebuggerDisplay("{nameof(SqlTableBase1c)} | {nameof(Uid1C)} = {Uid1C}")]
// ReSharper disable once InconsistentNaming
public class SqlTableBase1c : SqlTableBase, ISqlTable1c
{
    #region Public and private fields, properties, constructor

    [XmlIgnore] public virtual Guid Uid1C { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public SqlTableBase1c() : base()
    {
        Uid1C = Guid.Empty;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    public SqlTableBase1c(SqlFieldIdentity identityName) : base(identityName)
    {
        Uid1C = Guid.Empty;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    protected SqlTableBase1c(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Uid1C = info.GetValue(nameof(Uid1C), typeof(Guid)) is Guid uid1C ? uid1C : Guid.Empty;
    }

    #endregion

    #region Public and private methods - override

    public virtual bool Equals(ISqlTable1c item) =>
        ReferenceEquals(this, item) || base.Equals(item) && Equals(Uid1C, item.Uid1C);

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SqlTableBase1c)obj);
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

    public override bool EqualsNew() => Equals(new SqlTableBase1c());

    public override bool EqualsDefault() =>
        base.EqualsDefault() && Equals(Uid1C, Guid.Empty);

    public override object Clone()
    {
        SqlTableBase1c item = new();
        item.CloneSetup(base.CloneCast());
        item.Uid1C = Uid1C;
        return item;
    }

    public new virtual SqlTableBase1c CloneCast() =>
        (SqlTableBase1c)Clone();

    public virtual void CloneSetup(SqlTableBase1c item)
    {
        if (item is SqlTableBase sqlTable)
            base.CloneSetup(sqlTable);
    }

    public virtual void UpdateProperties(ISqlTable1c item)
    {
        base.UpdateProperties(item);
        Uid1C = item.Uid1C;
    }

    #endregion
}