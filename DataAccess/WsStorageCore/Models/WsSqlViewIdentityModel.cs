namespace WsStorageCore.Models;

[DebuggerDisplay("{ToString()}")]
public record WsSqlViewIdentityModel
{
    #region Public and private fields, properties, constructor

    public WsSqlEnumFieldIdentity Name { get; init; }
    public Guid Uid { get; init; }
    public long Id { get; init; }

    public WsSqlViewIdentityModel(Guid uid)
    {
        Name = WsSqlEnumFieldIdentity.Uid;
        Uid = uid;
        Id = default;
    }

    public WsSqlViewIdentityModel(long id)
    {
        Name = WsSqlEnumFieldIdentity.Id;
        Id = id;
        Uid = Guid.Empty;
    }

    protected WsSqlViewIdentityModel(SerializationInfo info, StreamingContext context)
    {
        Name = (WsSqlEnumFieldIdentity)info.GetValue(nameof(Name), typeof(WsSqlEnumFieldIdentity));
        Uid = Guid.Parse(info.GetString(nameof(Uid).ToUpper()));
        Id = info.GetInt64(nameof(Id));
    }

    public WsSqlViewIdentityModel(WsSqlViewIdentityModel item)
    {
        Name = item.Name;
        Uid = item.Uid;
        Id = item.Id;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        Name.Equals(WsSqlEnumFieldIdentity.Id) ? $"{Id}" : Name.Equals(WsSqlEnumFieldIdentity.Uid) ? $"{Uid}" : string.Empty;

    #endregion
}