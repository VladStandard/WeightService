using MDSoft.NetUtils;

namespace ScalesHybrid.Pages;

public partial class Index
{
    private static string PcName => MdNetUtils.GetLocalDeviceName(false);
    private static string PcIp => MdNetUtils.GetLocalIpAddress();
}