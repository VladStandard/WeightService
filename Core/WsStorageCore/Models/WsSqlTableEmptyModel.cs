// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Models;

/// <summary>
/// SQL empty table model.
/// </summary>
[DebuggerDisplay("{ToString()}")]
public class WsSqlTableEmptyModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    private WsSqlTableEmptyModel() { }

    private WsSqlTableEmptyModel(SerializationInfo info, StreamingContext context) : base(info, context) { }

    public WsSqlTableEmptyModel(WsSqlTableEmptyModel item) : base(item) { }

    #endregion

    #region Public and private methods - override

    public override string ToString() => base.ToString();

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlTableEmptyModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() => base.EqualsDefault();

    #endregion

    #region Public and private methods - virtual

    protected virtual bool Equals(WsSqlTableEmptyModel item) =>
        ReferenceEquals(this, item) || base.Equals(item);

    #endregion
}