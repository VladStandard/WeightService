// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.Apps;

/// <summary>
/// Table "APPS".
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlAppModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlAppModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        //
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlAppModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        //
    }

    public WsSqlAppModel(WsSqlAppModel item) : base(item) { }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{GetIsMarked()} | " +
        $"{nameof(Name)}: {Name}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlAppModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() => base.EqualsDefault();

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlAppModel item) =>
        ReferenceEquals(this, item) || base.Equals(item);

    #endregion
}
