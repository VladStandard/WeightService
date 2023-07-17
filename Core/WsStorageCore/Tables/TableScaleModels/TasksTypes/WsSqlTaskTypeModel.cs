// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleModels.TasksTypes;

/// <summary>
/// Table "TASKS_TYPES".
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlTaskTypeModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    public WsSqlTaskTypeModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        //
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlTaskTypeModel(SerializationInfo info, StreamingContext context) : base(info, context) { }

    public WsSqlTaskTypeModel(WsSqlTaskTypeModel item) : base(item) { }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{GetIsMarked()} | " +
        $"{nameof(Name)}: {Name}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlTaskTypeModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() => base.EqualsDefault();

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlTaskTypeModel item) =>
        ReferenceEquals(this, item) || base.Equals(item);

    #endregion
}