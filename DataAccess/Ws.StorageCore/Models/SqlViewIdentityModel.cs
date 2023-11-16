using Ws.StorageCore.Common;
namespace Ws.StorageCore.Models;

[DebuggerDisplay("{ToString()}")]
public record SqlViewIdentityModel
{
    #region Public and private fields, properties, constructor

    public SqlEnumFieldIdentity Name { get; init; }
    public Guid Uid { get; init; }
    public long Id { get; init; }

    public SqlViewIdentityModel(Guid uid)
    {
        Name = SqlEnumFieldIdentity.Uid;
        Uid = uid;
        Id = default;
    }

    public SqlViewIdentityModel(long id)
    {
        Name = SqlEnumFieldIdentity.Id;
        Id = id;
        Uid = Guid.Empty;
    }

    public SqlViewIdentityModel(SqlViewIdentityModel item)
    {
        Name = item.Name;
        Uid = item.Uid;
        Id = item.Id;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        Name.Equals(SqlEnumFieldIdentity.Id) ? $"{Id}" : Name.Equals(SqlEnumFieldIdentity.Uid) ? $"{Uid}" : string.Empty;

    #endregion
}