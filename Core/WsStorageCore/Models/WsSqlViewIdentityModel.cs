// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Models;

/// <summary>
/// SQL view identity model.
/// </summary>
[DebuggerDisplay("{ToString()}")]
public record WsSqlViewIdentityModel
{
    #region Public and private fields, properties, constructor

    public WsSqlEnumFieldIdentity Name { get; init; }
    public Guid Uid { get; init; }
    public long Id { get; init; }
    public bool IsUid => Equals(Name, WsSqlEnumFieldIdentity.Uid);
    public bool IsId => Equals(Name, WsSqlEnumFieldIdentity.Id);

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

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        Name.Equals(WsSqlEnumFieldIdentity.Id) ? $"{Id}" : Name.Equals(WsSqlEnumFieldIdentity.Uid) ? $"{Uid}" : string.Empty;

    #endregion
}