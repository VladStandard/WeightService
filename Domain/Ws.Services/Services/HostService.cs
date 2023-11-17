using MDSoft.NetUtils;
using Ws.StorageCore.Entities.SchemaRef.Hosts;

namespace Ws.Services.Services;

public class HostService
{
    public SqlHostEntity GetCurrentHost()
    {
        string hostname = MdNetUtils.GetLocalDeviceName(false);
        SqlHostEntity host = new SqlHostRepository().GetItemByName(hostname);
        host.Ip = MdNetUtils.GetLocalIpAddress();
        host.LoginDt = DateTime.Now;
        return host;
    }
}