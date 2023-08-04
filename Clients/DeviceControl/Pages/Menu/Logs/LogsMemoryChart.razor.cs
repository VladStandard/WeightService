// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DeviceControl.Pages.Menu.Logs;

public sealed partial class LogsMemoryChart : SectionBase<WsSqlViewLogMemoryModel>
{
    #region Public and private fields, properties, constructor

    private WsSqlViewLogMemoryRepository LogMemoryRepository { get; } = new();
    private Interpolation _interpolation = Interpolation.Line;

    public LogsMemoryChart() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticItem();
    }

    private WsChartDataItemModel[] _valuesMaximum = new[] {
        new WsChartDataItemModel
        {
            Date = ("2019-01-01"),
            Revenue = 234000
        },
        new WsChartDataItemModel
        {
            Date = ("2019-02-01"),
            Revenue = 269000
        },
        new WsChartDataItemModel
        {
            Date = ("2019-03-01"),
            Revenue = 233000
        },
        new WsChartDataItemModel
        {
            Date = ("2019-04-01"),
            Revenue = 244000
        },
        new WsChartDataItemModel
        {
            Date = ("2019-05-01"),
            Revenue = 214000
        },
        new WsChartDataItemModel
        {
            Date = ("2019-06-01"),
            Revenue = 253000
        },
        new WsChartDataItemModel
        {
            Date = ("2019-07-01"),
            Revenue = 274000
        },
        new WsChartDataItemModel
        {
            Date = ("2019-08-01"),
            Revenue = 284000
        },
        new WsChartDataItemModel
        {
            Date = ("2019-09-01"),
            Revenue = 273000
        },
        new WsChartDataItemModel
        {
            Date = ("2019-10-01"),
            Revenue = 282000
        },
        new WsChartDataItemModel
        {
            Date = ("2019-11-01"),
            Revenue = 289000
        },
        new WsChartDataItemModel
        {
            Date = ("2019-12-01"),
            Revenue = 294000
        }
    };

    private WsChartDataItemModel[] _valuesCurrent = new[] {
        new WsChartDataItemModel
        {
            Date = ("2019-01-01"),
            Revenue = 334000
        },
        new WsChartDataItemModel
        {
            Date = ("2019-02-01"),
            Revenue = 369000
        },
        new WsChartDataItemModel
        {
            Date = ("2019-03-01"),
            Revenue = 333000
        },
        new WsChartDataItemModel
        {
            Date = ("2019-04-01"),
            Revenue = 344000
        },
        new WsChartDataItemModel
        {
            Date = ("2019-05-01"),
            Revenue = 314000
        },
        new WsChartDataItemModel
        {
            Date = ("2019-06-01"),
            Revenue = 353000
        },
        new WsChartDataItemModel
        {
            Date = ("2019-07-01"),
            Revenue = 374000
        },
        new WsChartDataItemModel
        {
            Date = ("2019-08-01"),
            Revenue = 384000
        },
        new WsChartDataItemModel
        {
            Date = ("2019-09-01"),
            Revenue = 373000
        },
        new WsChartDataItemModel
        {
            Date = ("2019-10-01"),
            Revenue = 382000
        },
        new WsChartDataItemModel
        {
            Date = ("2019-11-01"),
            Revenue = 389000
        },
        new WsChartDataItemModel
        {
            Date = ("2019-12-01"),
            Revenue = 394000
        }
    };

    #endregion

    #region Public and private methods

    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = LogMemoryRepository.GetList(SqlCrudConfigSection);
    }

    string FormatAsUSD(object value)
    {
        return ((double)value).ToString("C0", CultureInfo.CreateSpecificCulture("en-US"));
    }

    string FormatAsMonth(object value)
    {
        if (value != null)
        {
            return Convert.ToDateTime(value).ToString("MMM");
        }

        return string.Empty;
    }

    #endregion
}