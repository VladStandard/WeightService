// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DeviceControl.Common;

/// <summary>
/// Данные для чарта RadzenChart.
/// </summary>
public sealed class WsChartDataItemModel
{
    #region Public and private fields, properties, constructor

    public DateTime Dt { get; set; }
    public int SizeApp { get; set; }
    public int SizeFree { get; set; }
    public string Date => Dt.Date.ToString("MMM");
    public string Day => Dt.Day.ToString();

    public WsChartDataItemModel()
    {
        Dt = DateTime.MinValue;
        SizeApp = 0;
        SizeFree = 0;
    }

    public WsChartDataItemModel(DateTime dt, int sizeApp, int sizeFree)
    {
        Dt = dt;
        SizeApp = sizeApp;
        SizeFree = sizeFree;
    }

    #endregion
}