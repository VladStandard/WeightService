namespace Ws.StorageCore.Common;

[DebuggerDisplay("{ToString()}")]
public class SqlViewBase
{
    public SqlViewIdentityModel Identity { get; init; }

    protected SqlViewBase(Guid uid)
    {
        Identity = new(uid);
    }
    
    public override string ToString() => Identity.Uid.ToString();
    
}