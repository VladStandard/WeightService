namespace Ws.StorageCore.Common;

[DebuggerDisplay("{ToString()}")]
public class SqlViewBase : ViewModelBase
{
    #region Public and private fields, properties, constructor

    public SqlViewIdentityModel Identity { get; init; }

    protected SqlViewBase(Guid uid)
    {
        Identity = new(uid);
    }

    protected SqlViewBase(long id)
    {
        Identity = new(id);
    }

    protected SqlViewBase(SerializationInfo info, StreamingContext context)
    {
        Identity = (SqlViewIdentityModel)info.GetValue(nameof(Identity), typeof(SqlViewIdentityModel));
    }

    public SqlViewBase(SqlViewBase item) : base(item)
    {
        Identity = new(item.Identity);
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