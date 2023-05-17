// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.ViewModels;

public sealed class WsPagePluNestingViewModel : WsPageBaseViewModel
{
    #region Public and private fields, properties, constructor

    private List<WsSqlViewPluNestingModel> _viewPluNestings;
    public List<WsSqlViewPluNestingModel> ViewPluNestings
    {
        get => _viewPluNestings;
        set
        {
            _viewPluNestings = value;
            OnPropertyChanged();
        }
    }

    public WsPagePluNestingViewModel()
    {
        _viewPluNestings = new();
    }

    #endregion
}