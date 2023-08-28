using System.Drawing;

namespace WsStorageCore.Utils;

public static class WsSqlEnumUtils
{
    #region Public and private methods

    public static IEnumerable<string> GetEnumerableTimeIntervals() => new List<string>
    {
        WsLocaleCore.DeviceControl.ChartTimeIntervalEmpty,
        WsLocaleCore.DeviceControl.ChartTimeIntervalToday,
        WsLocaleCore.DeviceControl.ChartTimeIntervalMonth,
        WsLocaleCore.DeviceControl.ChartTimeIntervalYear,
        WsLocaleCore.DeviceControl.ChartTimeIntervalAll,
    };

    public static WsSqlEnumTimeInterval GetTimeInterval(string timeInterval)
    {
        if (timeInterval == WsLocaleCore.DeviceControl.ChartTimeIntervalAll)
            return WsSqlEnumTimeInterval.All;
        if (timeInterval == WsLocaleCore.DeviceControl.ChartTimeIntervalToday)
            return WsSqlEnumTimeInterval.Today;
        if (timeInterval == WsLocaleCore.DeviceControl.ChartTimeIntervalMonth)
            return WsSqlEnumTimeInterval.Month;
        if (timeInterval == WsLocaleCore.DeviceControl.ChartTimeIntervalYear)
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