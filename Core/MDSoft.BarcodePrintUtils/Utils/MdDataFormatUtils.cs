using WsStorageCore.OrmUtils;

namespace MDSoft.BarcodePrintUtils.Utils;

public static class MdDataFormatUtils
{
    private static List<WsSqlTemplateResourceModel> _templateResources = new();

    private static void LoadTemplatesResources(bool isForceUpdate)
    {
        if (!isForceUpdate && _templateResources.Any())
            return;

        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        
        sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        sqlCrudConfig.AddFilter(
            SqlRestrictions.Equal(nameof(WsSqlTemplateResourceModel.Type), "ZPL")
        );
        _templateResources = new WsSqlTemplateResourceRepository().GetList(sqlCrudConfig);
    }

    /// <summary>
    /// Заменить zpl-ресурсы из таблицы ресурсов шаблонов.
    /// </summary>
    public static string PrintCmdReplaceZplResources(string zpl, Action<string> actionReplaceStorageMethod)
    {
        if (string.IsNullOrEmpty(zpl))
            throw new ArgumentException("Value must be fill!", nameof(zpl));

        LoadTemplatesResources(false);
        foreach (WsSqlTemplateResourceModel resource in _templateResources)
        {
            string name = $"[{resource.Name}]";
            if (!zpl.Contains(name))
                continue;
            zpl = zpl.Replace(name, ZplUtils.ConvertStringToHex(resource.Data.ValueUnicode));
        }

        // Patch for table `PLUS_STORAGE_METHODS_FK`.
        actionReplaceStorageMethod(zpl);

        return zpl;
    }
}