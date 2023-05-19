// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.ViewModels;

public sealed class WsLineViewModel : WsWpfBaseViewModel
{
    #region Public and private fields, properties, constructor

    private WsSqlProductionFacilityModel _area;
    public WsSqlProductionFacilityModel Area { get => _area; set { _area = value; OnPropertyChanged(); } }
    private WsSqlScaleModel _line;
    public WsSqlScaleModel Line { get => _line; set { _line = value; OnPropertyChanged(); } }
    private List<WsSqlProductionFacilityModel> _areas;
    public List<WsSqlProductionFacilityModel> Areas { get => _areas; private set { _areas = value; OnPropertyChanged(); } }
    private List<WsSqlScaleModel> _lines;
    public List<WsSqlScaleModel> Lines { get => _lines; private set { _lines = value; OnPropertyChanged(); } }

    public WsLineViewModel()
    {
        Areas = ContextCache.ProductionFacilitiesDb;
        Lines = ContextCache.Scales;
    }

    #endregion
}