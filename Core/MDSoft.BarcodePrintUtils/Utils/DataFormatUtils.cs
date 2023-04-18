// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace MDSoft.BarcodePrintUtils.Utils;

#nullable enable
public static class DataFormatUtils
{
    private static List<TemplateResourceModel> _templateResources = new();

    public static List<TemplateResourceModel> LoadTemplatesResources(bool isForceUpdate)
    {
        if (!isForceUpdate && _templateResources.Any()) return _templateResources;
        SqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(SqlCrudConfigModel.GetFilters(nameof(TemplateResourceModel.Type), "ZPL"),
            new SqlFieldOrderModel() { Name = nameof(TemplateResourceModel.Name), Direction = WsSqlOrderDirection.Asc }, false, false);
        TemplateResourceModel[]? templateResources = WsStorageAccessManagerHelper.Instance.AccessList.GetArrayNullable<TemplateResourceModel>(sqlCrudConfig);
        return _templateResources = templateResources is not null ? templateResources.ToList() : new();
    }

    public static List<string> LoadTemplatesResourcesNames(bool isForceUpdate) =>
        LoadTemplatesResources(isForceUpdate).Select(item => item.Name).ToList();

    /// <summary>
    /// Replace zpl-resources from table `TEMPLATES_RESOURCES`.
    /// </summary>
    /// <param name="zpl"></param>
    /// <param name="actionReplaceStorageMethod"></param>
    /// <returns></returns>
    public static string PrintCmdReplaceZplResources(string zpl, Action<string> actionReplaceStorageMethod)
    {
        if (string.IsNullOrEmpty(zpl))
            throw new ArgumentException("Value must be fill!", nameof(zpl));

        LoadTemplatesResources(false);
        foreach (TemplateResourceModel resource in _templateResources)
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
            tscDriver.Cmd = PrintCmdReplaceZplResources(tscDriver.Cmd, _ => { });
    }
}