// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.ServiceProcess;
// ReSharper disable IdentifierTypo

namespace ZabbixStubService
{
    internal static class Program
    {
        private static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new ZabbixStubService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
