// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.ViewModels;

public sealed class WsPluNestingViewModel : WsWpfBaseViewModel
{
    #region Public and private fields, properties, constructor

    private WsSqlViewPluNestingModel _viewPluNesting;
    public WsSqlViewPluNestingModel ViewPluNesting
    {
        get => _viewPluNesting;
        set
        {
            _viewPluNesting = value;
            OnPropertyChanged();
        }
    }
    public List<WsSqlViewPluNestingModel> ViewPluNestings
    {
        get => ContextCache.ViewPlusNesting;
        set
        {
            _ = value;
            OnPropertyChanged();
        }
    }

    public WsPluNestingViewModel()
    {
        //
    }

    #endregion
}