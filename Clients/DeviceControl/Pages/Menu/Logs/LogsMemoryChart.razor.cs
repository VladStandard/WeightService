// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Tables.TableDiagModels.LogsMemories;

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
    }

    //private WsChartDataItemModel[] _valuesMaximum = new[] {
    //    new WsChartDataItemModel(new (2019,01, 01), 234000),
    //    new WsChartDataItemModel(new (2019,02, 01), 269000),
    //    new WsChartDataItemModel(new (2019,03, 01), 233000),
    //    new WsChartDataItemModel(new (2019,04, 01), 244000),
    //    new WsChartDataItemModel(new (2019,05, 01), 214000),
    //    new WsChartDataItemModel(new (2019,06, 01), 253000),
    //    new WsChartDataItemModel(new (2019,07, 01), 274000),
    //    new WsChartDataItemModel(new (2019,08, 01), 284000),
    //    new WsChartDataItemModel(new (2019,09, 01), 273000),
    //    new WsChartDataItemModel(new (2019,10, 01), 282000),
    //    new WsChartDataItemModel(new (2019,11, 01), 289000),
    //    new WsChartDataItemModel(new (2019,12, 01), 294000),
    //};

    #endregion

    #region Public and private methods

    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = LogMemoryRepository.GetList(SqlCrudConfigSection.SelectTopRowsCount);
    }

    #endregion
}