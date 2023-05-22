// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.ViewModels;

public sealed class WsPluNestingViewModel : WsWpfBaseViewModel
{
    #region Public and private fields, properties, constructor

    private WsSqlPluModel _plu;
    public WsSqlPluModel Plu { get => _plu; set { _plu = value; OnPropertyChanged(); } }
    private WsSqlViewPluNestingModel _pluNesting;
    public WsSqlViewPluNestingModel PluNesting { get => _pluNesting; set { _pluNesting = value; OnPropertyChanged(); } }
    private List<WsSqlViewPluNestingModel> _plusNestings;
    public List<WsSqlViewPluNestingModel> PlusNestings { get => _plusNestings; set { _plusNestings = value; OnPropertyChanged(); } }

    public WsPluNestingViewModel()
    {
        PlusNestings = ContextCache.LocalViewPlusNesting;
    }

    #endregion
}