namespace DeviceControl.Utils;

public class WsCssStyleTableHeadModel
{
    #region Public and private fields, properties, constructor

    public List<int> ColumnsWidths { get; set; }
    public List<string> ColumnsTitles { get; set; }
    
    public WsCssStyleTableHeadModel()
    {
        ColumnsWidths = new() {30, 70};
        ColumnsTitles = GetColumnsTitles();
    }
    
    public WsCssStyleTableHeadModel(List<int> columnsWidths)
    {
        ColumnsWidths = columnsWidths;
        ColumnsTitles = GetColumnsTitles();
    }

    #endregion

    #region Public and private methods

    private List<string> GetColumnsTitles()
    {
        
        if (!ColumnsWidths.Any())
            return new();
        
        List<string> columnsTitles = new()
        {
            LocaleCore.Strings.SettingName
        };

        for (int i = 1; i < ColumnsWidths.Count; i++)
            columnsTitles.Add(i % 2 == 0 ? LocaleCore.Strings.SettingName : LocaleCore.Strings.SettingValue);
        return columnsTitles;
    }

    #endregion
}
