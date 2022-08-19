// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Localizations;
using System.Collections.Generic;

namespace BlazorCore.Models.CssStyles;

public class TheadStyleModel : IBaseStyleModel
{
    #region Public and private fields, properties, constructor

    public List<int> ColumnsWidths { get; set; }
    public List<string> ColumnsTitles { get; set; }
    public string Color { get; set; }
    public string FontWeight { get; set; }
    public string TextAlign { get; set; }

    public TheadStyleModel() : this(new())
    {
	    //
    }

    public TheadStyleModel(List<int> columnsWidths) : 
	    this(columnsWidths, string.Empty, string.Empty, string.Empty)
	{
	    //
    }

    public TheadStyleModel(List<int> columnsWidths, string color, string fontWeight, string textAlign)
    {
	    ColumnsWidths = columnsWidths;
	    SetColumnsTitles();
		Color = color;
	    FontWeight = fontWeight;
	    TextAlign = textAlign;
    }

    public TheadStyleModel(List<int> columnsWidths, List<string> columnsTitels,
	    string color, string fontWeight, string textAlign)
    {
	    ColumnsWidths = columnsWidths;
		ColumnsTitles = columnsTitels;
		Color = color;
	    FontWeight = fontWeight;
	    TextAlign = textAlign;
    }

	#endregion

	#region Public and private methods

	private void SetColumnsTitles()
	{
		if (!ColumnsWidths.Any())
			return;
		
		ColumnsTitles = new();
		if (ColumnsWidths.Count > 0)
			ColumnsTitles.Add(LocaleCore.Strings.SettingName);
		if (ColumnsWidths.Count > 1)
			ColumnsTitles.Add(LocaleCore.Strings.SettingValue);
		if (ColumnsWidths.Count > 2)
			ColumnsTitles.Add(LocaleCore.Strings.SettingName);
		if (ColumnsWidths.Count > 3)
			ColumnsTitles.Add(LocaleCore.Strings.SettingValue);
	}

	#endregion
}
