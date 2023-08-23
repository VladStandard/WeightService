// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Common;

[DebuggerDisplay("{ToString()}")]
public class WsSqlViewBase : WsViewModelBase
{
    #region Public and private fields, properties, constructor

    public WsSqlViewIdentityModel Identity { get; init; }

    protected WsSqlViewBase(Guid uid)
    {
        Identity = new(uid);
    }

    protected WsSqlViewBase(long id)
    {
        Identity = new(id);
    }

    protected WsSqlViewBase(SerializationInfo info, StreamingContext context)
    {
        Identity = (WsSqlViewIdentityModel)info.GetValue(nameof(Identity), typeof(WsSqlViewIdentityModel));
    }

    public WsSqlViewBase(WsSqlViewBase item) : base(item)
    {
        Identity = new(item.Identity);
    }

    #endregion

    #region Public and private methods - override

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue(nameof(Identity), Identity);
    }

    #endregion
}