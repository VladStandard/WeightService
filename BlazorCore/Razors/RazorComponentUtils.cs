// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using DataCore.CssStyles;

namespace BlazorCore.Razors;

public static class RazorComponentUtils
{
	public static CssStyleTableHeadModel GetTableHeadStyle(List<int> columnsWidths) =>
		new(columnsWidths, "blue", "12px", "center");

	public static CssStyleTableHeadModel GetTableHeadStyleInfo() =>
		new(new() { 40, 30, 30 },
		new() { LocaleCore.Strings.SettingName, LocaleCore.Strings.SettingValue },
		"blue", "12px", "center");

	public static CssStyleTableBodyModel GetTableBodyStyle(SqlFieldIdentity columnName, bool isShowMarked) => new(columnName, isShowMarked);
}
