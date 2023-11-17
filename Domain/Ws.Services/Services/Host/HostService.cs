using MDSoft.NetUtils;
using Ws.StorageCore.Entities.SchemaRef.Hosts;

namespace Ws.Services.Services.Host;

public class HostService : IHostService
{
    public SqlHostEntity GetCurrentHostOrCreate()
    {
        string pcName = MdNetUtils.GetLocalDeviceName(false);
        SqlHostEntity host = new SqlHostRepository().GetItemByName(pcName);
        
        if (host.IsNew) 
            host.Name = pcName;
        host.Ip = MdNetUtils.GetLocalIpAddress();
        host.LoginDt = DateTime.Now;
        
        return new SqlHostRepository().SaveOrUpdate(host);
    }
}