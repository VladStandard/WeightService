// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Drawing;

namespace WsStorageCore.Utils;

public static class WsSqlEnumUtils
{
    #region Public and private methods

    public static IEnumerable<string> GetEnumerableTimeIntervals() => new List<string>
    {
        WsLocaleCore.DeviceControl.ChartMemoryTimeIntervalEmpty,
        WsLocaleCore.DeviceControl.ChartMemoryTimeIntervalToday,
        WsLocaleCore.DeviceControl.ChartMemoryTimeIntervalMonth,
        WsLocaleCore.DeviceControl.ChartMemoryTimeIntervalYear,
        WsLocaleCore.DeviceControl.ChartMemoryTimeIntervalAll,
    };

    public static WsSqlEnumTimeInterval GetTimeInterval(string timeInterval)
    {
        if (timeInterval == WsLocaleCore.DeviceControl.ChartMemoryTimeIntervalAll)
            return WsSqlEnumTimeInterval.All;
        else if (timeInterval == WsLocaleCore.DeviceControl.ChartMemoryTimeIntervalToday)
            return WsSqlEnumTimeInterval.Today;
        else if (timeInterval == WsLocaleCore.DeviceControl.ChartMemoryTimeIntervalMonth)
            return WsSqlEnumTimeInterval.Month;
        else if (timeInterval == WsLocaleCore.DeviceControl.ChartMemoryTimeIntervalYear)
            return WsSqlEnumTimeInterval.Year;
        return WsSqlEnumTimeInterval.Empty;
    }

    public static IEnumerable<Color> GetEnumerableColors() => new List<Color>
    {
        Color.Black,
        Color.Blue,
        Color.Brown,
        Color.DarkGray,
        Color.Gold,
        Color.Green,
        Color.LightBlue,
        Color.LightGray,
        Color.LightGreen,
        Color.Magenta,
        Color.Orange,
        Color.Pink,
        Color.Purple,
        Color.Red,
        Color.Silver,
        Color.White,
        Color.Yellow,
    };

    #endregion
}