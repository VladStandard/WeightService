// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DeviceControl.Settings;
using WsStorageCore.Tables.TableDiagModels.Logs;
using WsStorageCore.Tables.TableScaleModels.Scales;

namespace DeviceControl.Pages.Menu.Logs.MainLogs;

public sealed partial class ItemLog : ItemBase<WsSqlLogModel>
{
    #region Public and private fields, properties, constructor

    private WsSqlLineRepository LineRepository { get; } = new();

    public ItemLog() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticItem();
    }

    #endregion
}