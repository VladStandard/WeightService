// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.ViewModels;

public sealed class WsPageLineViewModel : WsPageBaseViewModel
{
    #region Public and private fields, properties, constructor

    public List<WsSqlProductionFacilityModel> ProductionFacilities
    {
        get => ContextCache.ProductionFacilitiesDb;
        private set
        {
            _ = value;
            OnPropertyChanged();
        }
    }

    public List<WsSqlScaleModel> Scales
    {
        get => ContextCache.ScalesDb;
        private set
        {
            _ = value;
            OnPropertyChanged();
        }
    }

    public WsPageLineViewModel()
    {
        //
    }

    #endregion
}