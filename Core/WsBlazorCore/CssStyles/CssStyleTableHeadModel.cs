// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsLocalizationCore.Utils;

namespace WsBlazorCore.CssStyles;

public class CssStyleTableHeadModel
{
    #region Public and private fields, properties, constructor

    public List<int> ColumnsWidths { get; set; }
    public List<string> ColumnsTitles { get; set; }
    
    public CssStyleTableHeadModel()
    {
        ColumnsWidths = new List<int> {30, 70};
        ColumnsTitles = GetColumnsTitles();
    }
    
    public CssStyleTableHeadModel(List<int> columnsWidths)
    {
        ColumnsWidths = columnsWidths;
        ColumnsTitles = GetColumnsTitles();
    }

    #endregion

    #region Public and private methods

    private List<string> GetColumnsTitles()
    {
        
        if (!ColumnsWidths.Any())
            return new List<string>();
        
        List<string> columnsTitles = new List<string> {
            WsLocaleCore.Strings.SettingName
        };

        for (int i = 1; i < ColumnsWidths.Count; i++)
            columnsTitles.Add(i % 2 == 0 ? WsLocaleCore.Strings.SettingName : WsLocaleCore.Strings.SettingValue);
        return columnsTitles;
    }

    #endregion
}