// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Common;

namespace MDSoft.BarcodePrintUtils.Utils;

#nullable enable
public static class DataFormatUtils
{
    private static List<WsSqlTemplateResourceModel> _templateResources = new();

    public static List<WsSqlTemplateResourceModel> LoadTemplatesResources(bool isForceUpdate)
    {
        if (!isForceUpdate && _templateResources.Any()) return _templateResources;
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(WsSqlCrudConfigModel.GetFilters(nameof(WsSqlTemplateResourceModel.Type), "ZPL"),
            new WsSqlFieldOrderModel() { Name = nameof(WsSqlTemplateResourceModel.Name), Direction = WsSqlEnumOrder.Asc }, WsSqlEnumIsMarked.ShowAll, false);
        WsSqlTemplateResourceModel[]? templateResources = WsSqlAccessManagerHelper.Instance.AccessList.GetArrayNullable<WsSqlTemplateResourceModel>(sqlCrudConfig);
        return _templateResources = templateResources is not null ? templateResources.ToList() : new();
    }

    public static List<string> LoadTemplatesResourcesNames(bool isForceUpdate) =>
        LoadTemplatesResources(isForceUpdate).Select(item => item.Name).ToList();

    /// <summary>
    /// Заменить zpl-ресурсы из таблицы ресурсов шаблонов.
    /// </summary>
    /// <param name="zpl"></param>
    /// <param name="actionReplaceStorageMethod"></param>
    /// <returns></returns>
    public static string PrintCmdReplaceZplResources(string zpl, Action<string> actionReplaceStorageMethod)
    {
        if (string.IsNullOrEmpty(zpl))
            throw new ArgumentException("Value must be fill!", nameof(zpl));

        LoadTemplatesResources(false);
        foreach (WsSqlTemplateResourceModel resource in _templateResources)
        {
            if (zpl.Contains($"[{resource.Name}]"))
            {
                string resourceHex = ZplUtils.ConvertStringToHex(resource.Data.ValueUnicode);
                zpl = zpl.Replace($"[{resource.Name}]", resourceHex);
            }
        }

        // Patch for table `PLUS_STORAGE_METHODS_FK`.
        actionReplaceStorageMethod(zpl);

        return zpl;
    }

    public static void CmdConvertZpl(TscDriverHelper tscDriver, bool isUsePicReplace)
    {
        tscDriver.Cmd = ZplUtils.ConvertStringToHex(tscDriver.TextPrepare);
        if (isUsePicReplace)
            // Заменить zpl-ресурсы из таблицы ресурсов шаблонов.
            tscDriver.Cmd = PrintCmdReplaceZplResources(tscDriver.Cmd, _ => { });
    }
}