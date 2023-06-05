// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Reflection;
using WsDataCore.Utils;

namespace DeviceControl.Pages.Menu.Admins;

public sealed partial class SystemInfo : RazorComponentBase
{
    #region Public and private fields, properties, constructor

    private string VerApp => AssemblyUtils.GetAppVersion(Assembly.GetExecutingAssembly());
    private string VerLibBlazorCore => BlazorCoreUtils.GetLibVersion();
    private string VerLibDataCore => AssemblyUtils.GetLibVersion();
    private uint DbFileSizeAll { get; set; }
    private List<WsSqlDbFileSizeInfoModel> DbSizeInfos { get; set; } = new();

    private string DbCurSizeAsString =>
        $"{LocaleCore.Sql.SqlDbCurSize}: {DbFileSizeAll:### ###} MB {LocaleCore.Strings.From} {DbMaxSize:### ###} MB";

    private uint DbMaxSize => 10_240;
    private uint DbFillSize => DbFileSizeAll == 0 ? 0 : DbFileSizeAll * 100 / DbMaxSize;
    private ulong CurrentRamSize => BlazorAppSettings.Memory.MemorySize.PhysicalAllocated.MegaBytes;
    private ulong TotalRamSize => BlazorAppSettings.Memory.MemorySize.PhysicalTotal.MegaBytes;

    #endregion

    #region Public and private methods

    protected override void OnAfterRender(bool firstRender)
    {
        if (!firstRender)
            return;
        DbSizeInfos = ContextManager.GetDbFileSizeInfos();
        DbFileSizeAll = ContextManager.GetDbFileSizeAll();
    }

    #endregion
}