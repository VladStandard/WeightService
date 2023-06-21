// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsLocalizationCore.Utils;
using WsStorageCore.Common;

namespace WsBlazorCore.Razors;

public static class RzComponentUtils
{
    public static CssStyleTableHeadModel GetTableHeadStyle(List<int> columnsWidths) =>
        new(columnsWidths, "blue", "12px", "center");

    public static CssStyleTableHeadModel GetTableHeadStyleInfo() =>
        new(new() { 40, 30, 30 },
        new() { WsLocaleCore.Strings.SettingName, WsLocaleCore.Strings.SettingValue },
        "blue", "12px", "center");
}