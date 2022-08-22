// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Localizations;
using System.Collections.Generic;

namespace BlazorCore.Models.CssStyles;

public class TableHeadStyleModel : IBaseStyleModel
{
    #region Public and private fields, properties, constructor

    public List<int> ColumnsWidths { get; set; }
    public List<string> ColumnsTitles { get; set; }
    public string Color { get; set; }
    public string FontWeight { get; set; }
    public string TextAlign { get; set; }

    public TableHeadStyleModel() : this(new())
    {
	    //
    }

    public TableHeadStyleModel(List<int> columnsWidths) : 
	    this(columnsWidths, string.Empty, string.Empty, string.Empty)
	{
	    //
    }

    public TableHeadStyleModel(List<int> columnsWidths, string color, string fontWeight, string textAlign)
    {
	    ColumnsWidths = columnsWidths;
	    ColumnsTitles = GetColumnsTitles();
		Color = color;
	    FontWeight = fontWeight;
	    TextAlign = textAlign;
    }

    public TableHeadStyleModel(List<int> columnsWidths, List<string> columnsTitles,
	    string color, string fontWeight, string textAlign)
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
		if (columnsTitles.Count > 0)
			columnsTitles.Add(LocaleCore.Strings.SettingName);
		if (columnsTitles.Count > 1)
			columnsTitles.Add(LocaleCore.Strings.SettingValue);
		if (columnsTitles.Count > 2)
			columnsTitles.Add(LocaleCore.Strings.SettingName);
		if (columnsTitles.Count > 3)
			columnsTitles.Add(LocaleCore.Strings.SettingValue);
		return columnsTitles;
	}

	#endregion
}
