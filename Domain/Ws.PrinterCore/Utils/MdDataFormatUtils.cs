using Ws.PrinterCore.Zpl;
using Ws.StorageCore.Entities.SchemaScale.TemplatesResources;
using Ws.StorageCore.OrmUtils;

namespace Ws.PrinterCore.Utils;

public static class MdDataFormatUtils
{
    private static List<SqlTemplateResourceEntity> _templateResources = new();

    private static void LoadTemplatesResources(bool isForceUpdate)
    {
        if (!isForceUpdate && _templateResources.Any())
            return;

        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigFactory.GetCrudAll();
        
        sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        sqlCrudConfig.AddFilter(
            SqlRestrictions.Equal(nameof(SqlTemplateResourceEntity.Type), "ZPL")
        );
        _templateResources = new SqlTemplateResourceRepository().GetList(sqlCrudConfig);
    }
    
    public static string PrintCmdReplaceZplResources(string zpl, Action<string> actionReplaceStorageMethod)
    {
        if (string.IsNullOrEmpty(zpl))
            throw new ArgumentException("Value must be fill!", nameof(zpl));

        LoadTemplatesResources(false);
        foreach (SqlTemplateResourceEntity resource in _templateResources)
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