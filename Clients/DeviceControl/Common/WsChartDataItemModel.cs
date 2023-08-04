// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DeviceControl.Common;

/// <summary>
/// Данные для чарта RadzenChart.
/// </summary>
public class WsChartDataItemModel
{
    public string Date { get; set; }
    public double Revenue { get; set; }
}