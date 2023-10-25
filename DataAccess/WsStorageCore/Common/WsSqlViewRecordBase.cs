namespace WsStorageCore.Common;

[DebuggerDisplay("{ToString()}")]
public record WsSqlViewRecordBase
{
    #region Public and private fields, properties, constructor

    public WsSqlViewIdentityModel Identity { get; init; }

    protected WsSqlViewRecordBase(Guid uid)
    {
        Identity = new(uid);
    }

    public WsSqlViewRecordBase(long id)
    {
        Identity = new(id);
    }

    protected WsSqlViewRecordBase(SerializationInfo info, StreamingContext context)
    {
        Identity = (WsSqlViewIdentityModel)info.GetValue(nameof(Identity), typeof(WsSqlViewIdentityModel));
    }

    #endregion

    #region Public and private methods - override

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue(nameof(Identity), Identity);
    }

    #endregion
}