// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleModels.Scales;

/// <summary>
/// Контроллер таблицы SCALES.
/// </summary>
public sealed class WsSqlLineRepository : WsSqlTableRepositoryBase<WsSqlScaleModel>
{
    private WsSqlDeviceLineFkRepository DeviceLineFkRepository { get; } = new();
    
    #region Item

    public WsSqlScaleModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlScaleModel>();

    public WsSqlScaleModel GetItemById(long id) => SqlCore.GetItemById<WsSqlScaleModel>(id);

    public WsSqlScaleModel GetItemByDevice(WsSqlDeviceModel device)
    {
        return DeviceLineFkRepository.GetItemByDevice(device).Scale;
    }

    #endregion

    #region List

    public List<WsSqlScaleModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.Description) });
        return SqlCore.GetListNotNullable<WsSqlScaleModel>(sqlCrudConfig);
    }

    #endregion

    #region CRUD

    public void Update(WsSqlScaleModel line) => SqlCore.Update(line);

    #endregion
}