// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using BlazorCore.Models.CssStyles;
using DataCore.Localizations;

namespace BlazorCore.Shared;

public static class ComponentsUtils
{
	public static TheadStyleModel GetTheadStyle(List<int> columnsWidths) => 
		new(columnsWidths, "blue", "12px", "center");
	
	public static TheadStyleModel GetTheadStyleInfo() => 
		new(new() { 40, 30, 30 }, 
		new() { LocaleCore.Strings.SettingName, LocaleCore.Strings.SettingValue }, 
		"blue", "12px", "center");
}