// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.ViewModels;

public partial class WsWpfBaseViewModel : BaseViewModel, INotifyPropertyChanged
{
    #region Public and private fields, properties, constructor

    protected WsSqlContextCacheHelper ContextCache => WsSqlContextCacheHelper.Instance;
    public Action ActionReturnOk { get; set; }
    public Action ActionReturnCancel { get; set; }

    [RelayCommand]
    public void ReturnOk()
    {
        ActionReturnOk();
    }

    [RelayCommand]
    public void ReturnCancel()
    {
        ActionReturnCancel();
    }

    #endregion
}