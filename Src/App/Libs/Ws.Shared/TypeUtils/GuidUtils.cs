namespace Ws.Shared.TypeUtils;

public static class GuidUtils
{
    public static readonly Guid MaxGuid = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff");
    public static bool IsMax(this Guid guid) =>
        guid.Equals(Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"));
}