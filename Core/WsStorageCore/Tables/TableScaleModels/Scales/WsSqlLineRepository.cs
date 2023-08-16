// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleModels.Scales;

/// <summary>
/// Контроллер таблицы SCALES.
/// </summary>
public sealed class WsSqlLineRepository : WsSqlTableRepositoryBase<WsSqlScaleModel>
{
    #region Public and private fields, properties, constructor

    private WsSqlDeviceLineFkRepository DeviceLineFkRepository { get; } = new();

    #endregion

    #region Public and private methods

    public WsSqlScaleModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlScaleModel>();

    public WsSqlScaleModel GetItemById(long id) => SqlCore.GetItemById<WsSqlScaleModel>(id);

    public WsSqlScaleModel GetItemByDevice(WsSqlDeviceModel device)
    {
        return DeviceLineFkRepository.GetItemByDevice(device).Scale;
    }

    public IEnumerable<WsSqlScaleModel> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(nameof(WsSqlTableBase.Description));
        return SqlCore.GetEnumerableNotNullable<WsSqlScaleModel>(sqlCrudConfig);
    }

    public void Update(WsSqlScaleModel line) => SqlCore.Update(line);

    #endregion
}