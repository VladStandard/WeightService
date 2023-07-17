// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleModels.Devices;

public sealed class WsSqlDeviceRepository : WsSqlTableRepositoryBase<WsSqlDeviceModel>
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlDeviceRepository _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlDeviceRepository Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private methods

    public WsSqlDeviceModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlDeviceModel>();

    public WsSqlDeviceModel GetItem(Guid? uid) => SqlCore.GetItemNotNullableByUid<WsSqlDeviceModel>(uid);

    public List<WsSqlDeviceModel> GetList() => ContextList.GetListNotNullableDevices(SqlCrudConfig);

    #endregion
}