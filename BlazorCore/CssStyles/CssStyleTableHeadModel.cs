// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Localizations;
using System.Collections.Generic;

namespace BlazorCore.CssStyles;

public class CssStyleTableHeadModel : CssStyleBase
{
	#region Public and private fields, properties, constructor

	public List<int> ColumnsWidths { get; set; }
	public List<string> ColumnsTitles { get; set; }
	public string Color { get; set; }
	public string FontWeight { get; set; }
	public string TextAlign { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
	public CssStyleTableHeadModel()
	{
		ColumnsWidths = new();
		ColumnsTitles = GetColumnsTitles();
		Color = string.Empty;
		FontWeight = string.Empty;
		TextAlign = string.Empty;
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="columnsWidths"></param>
	public CssStyleTableHeadModel(List<int> columnsWidths)
	{
		ColumnsWidths = columnsWidths;
		ColumnsTitles = GetColumnsTitles();
		Color = string.Empty;
		FontWeight = string.Empty;
		TextAlign = string.Empty;
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="columnsWidths"></param>
	/// <param name="color"></param>
	/// <param name="fontWeight"></param>
	/// <param name="textAlign"></param>
	public CssStyleTableHeadModel(List<int> columnsWidths, string color, string fontWeight, string textAlign)
	{
		ColumnsWidths = columnsWidths;
		ColumnsTitles = GetColumnsTitles();
		Color = color;
		FontWeight = fontWeight;
		TextAlign = textAlign;
	}

	public CssStyleTableHeadModel(List<int> columnsWidths, List<string> columnsTitles, string color, string fontWeight, string textAlign)
	{
		ColumnsWidths = columnsWidths;
		ColumnsTitles = columnsTitles;
		Color = color;
		FontWeight = fontWeight;
		TextAlign = textAlign;
	}

	#endregion

	#region Public and private methods

	public List<string> GetColumnsTitles()
	{
		if (!ColumnsWidths.Any())
			return new();

		List<string> columnsTitles = new();
		if (ColumnsWidths.Count > 0)
			columnsTitles.Add(LocaleCore.Strings.SettingName);
		if (ColumnsWidths.Count > 1)
			columnsTitles.Add(LocaleCore.Strings.SettingValue);
		if (ColumnsWidths.Count > 2)
			columnsTitles.Add(LocaleCore.Strings.SettingName);
		if (ColumnsWidths.Count > 3)
			columnsTitles.Add(LocaleCore.Strings.SettingValue);
		return columnsTitles;
	}

	public void SetColumnsTitles()
	{
		ColumnsTitles = GetColumnsTitles();
	}

	#endregion
}
