// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DeviceControl.Pages.Menu.Logs;

public sealed partial class LogsMemoryChart : SectionBase<WsSqlViewLogMemoryModel>
{
    #region Public and private fields, properties, constructor

    private WsSqlViewLogMemoryRepository LogMemoryRepository { get; } = new();
    private Interpolation WsInterpolation { get; set; } = Interpolation.Line;
    private WsChartDataItemModel[] ArraySizeApp { get; set; } = Array.Empty<WsChartDataItemModel>();
    private WsChartDataItemModel[] ArraySizeFree { get; set; } = Array.Empty<WsChartDataItemModel>();

    public LogsMemoryChart() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticItem();
        // Лог памяти.
        new WsSqlLogMemoryRepository().Save();
    }

    #endregion

    #region Public and private methods

    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = LogMemoryRepository.GetListToday();
    }

    #endregion
}