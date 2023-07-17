// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleModels.Apps;

/// <summary>
/// Barcode helper.
/// </summary>
public sealed class WsSqlAppRepository : WsSqlTableRepositoryBase<WsSqlAppModel>
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlAppRepository _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlAppRepository Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private methods

    public WsSqlAppModel GetItemAppOrCreateNew(string appName) => SqlCore.GetItemAppOrCreateNew(appName);

    #endregion
}