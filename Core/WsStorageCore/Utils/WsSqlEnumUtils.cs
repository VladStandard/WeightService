// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Drawing;

namespace WsStorageCore.Utils;

public static class WsSqlEnumUtils
{
    #region Public and private methods

    public static IEnumerable<string> GetEnumerableTimeIntervals() => new List<string>
    {
        WsLocaleCore.DeviceControl.LogsMemoryTimeIntervalEmpty,
        WsLocaleCore.DeviceControl.LogsMemoryTimeIntervalToday,
        WsLocaleCore.DeviceControl.LogsMemoryTimeIntervalMonth,
        WsLocaleCore.DeviceControl.LogsMemoryTimeIntervalYear,
        WsLocaleCore.DeviceControl.LogsMemoryTimeIntervalAll,
    };

    public static WsSqlEnumTimeInterval GetTimeInterval(string timeInterval)
    {
        if (timeInterval == WsLocaleCore.DeviceControl.LogsMemoryTimeIntervalAll)
            return WsSqlEnumTimeInterval.All;
        else if (timeInterval == WsLocaleCore.DeviceControl.LogsMemoryTimeIntervalToday)
            return WsSqlEnumTimeInterval.Today;
        else if (timeInterval == WsLocaleCore.DeviceControl.LogsMemoryTimeIntervalMonth)
            return WsSqlEnumTimeInterval.Month;
        else if (timeInterval == WsLocaleCore.DeviceControl.LogsMemoryTimeIntervalYear)
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