// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable MismatchedFileName

namespace WsLabelCore.Pages;

/// <summary>
/// Interaction logic for WsXamlDialogPage.xaml
/// </summary>
#nullable enable
public partial class WsXamlDialogPage : WsXamlBasePage
{
    #region Public and private fields, properties, constructor

    public WsXamlDialogPage(WsXamlBaseViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
        ViewModel = viewModel;
        borderMain.Child = GridMain;
    }

    /// <summary>
    /// Обновить модель представления.
    /// </summary>
    public override void RefreshViewModel()
    {
        base.RefreshViewModel();
        WsFormNavigationUtils.ActionTryCatchSimple(() =>
        {
            //
        });
    }

    #endregion
}