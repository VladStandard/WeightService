// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Reflection;
using WsDataCore.Utils;

namespace DeviceControl.Pages.Menu.Admins;

public sealed partial class SystemInfo : RazorComponentBase
{
    #region Public and private fields, properties, constructor

    private static string VerApp => WsAssemblyUtils.GetAppVersion(Assembly.GetExecutingAssembly());
    private static string VerLibBlazorCore => BlazorCoreUtils.GetLibVersion();
    private static string VerLibDataCore => WsAssemblyUtils.GetLibVersion();
    private ulong CurrentRamSize => BlazorAppSettings.Memory.MemorySize.PhysicalAllocated.MegaBytes;
    private ulong TotalRamSize => BlazorAppSettings.Memory.MemorySize.PhysicalTotal.MegaBytes;

    #endregion
    
}