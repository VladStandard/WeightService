// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableConfModels.DeviceSettings;

/// <summary>
/// Репозиторий таблицы "DEVICES_SETTINGS".
/// </summary>
public sealed class WsSqlDeviceSettingsRepository : WsSqlTableRepositoryBase<WsSqlDeviceSettingsModel>
{
    #region Public and private methods

    public WsSqlDeviceSettingsModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlDeviceSettingsModel>();

    public WsSqlDeviceSettingsModel GetItem(Guid? uid) => SqlCore.GetItemNotNullableByUid<WsSqlDeviceSettingsModel>(uid);

    public List<WsSqlDeviceSettingsModel> GetList() => GetList(SqlCrudConfig);
    
    public List<WsSqlDeviceSettingsModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Name) });
        List<WsSqlDeviceSettingsModel> list = SqlCore.GetListNotNullable<WsSqlDeviceSettingsModel>(sqlCrudConfig);
        if (sqlCrudConfig.IsResultOrder && list.Any())
            list = list.OrderBy(item => item.Name).ToList();
        return list;
    }

    #endregion
}