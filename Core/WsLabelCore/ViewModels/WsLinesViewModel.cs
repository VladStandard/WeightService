// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.ViewModels;

public sealed class WsLinesViewModel : WsWpfBaseViewModel
{
    #region Public and private fields, properties, constructor

    public WsSqlProductionFacilityModel Area { get; set; }
    public List<WsSqlProductionFacilityModel> Areas { get; set; }
    public WsSqlScaleModel Line { get; set; }
    public List<WsSqlScaleModel> Lines { get; set; }

    public WsLinesViewModel()
    {
        Area = new();
        Line = new();
        Areas = ContextCache.ProductionFacilities;
        Lines = ContextCache.Scales;
    }

    #endregion
}