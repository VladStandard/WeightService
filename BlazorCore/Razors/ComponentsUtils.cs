// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using BlazorCore.CssStyles;
using DataCore.Localizations;
using DataCore.Sql.Core;

namespace BlazorCore.Razors;

public static class ComponentsUtils
{
	public static CssStyleTableHeadModel GetTableHeadStyle(List<int> columnsWidths) => 
		new(columnsWidths, "blue", "12px", "center");
	
	public static CssStyleTableHeadModel GetTabelHeadStyleInfo() => 
		new(new() { 40, 30, 30 }, 
		new() { LocaleCore.Strings.SettingName, LocaleCore.Strings.SettingValue }, 
		"blue", "12px", "center");

	public static CssStyleTableBodyModel GetTableBodyStyle(SqlFieldIdentityEnum columnName, bool isShowMarked) => new(columnName, isShowMarked);
}
