namespace Ws.StorageCore.Common;

[DebuggerDisplay("{ToString()}")]
public class SqlViewBase : ViewModelBase
{
    public SqlViewIdentityModel Identity { get; init; }

    protected SqlViewBase(Guid uid)
    {
        Identity = new(uid);
    }
}