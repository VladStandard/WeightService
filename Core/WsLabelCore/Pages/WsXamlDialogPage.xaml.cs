// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable MismatchedFileName

namespace WsLabelCore.Pages;

/// <summary>
/// Страница диалога.
/// </summary>
[DebuggerDisplay("{ToString()}")]
#nullable enable
public partial class WsXamlDialogPage
{
    #region Public and private fields, properties, constructor

    public WsXamlDialogPage()
    {
        InitializeComponent();
    }

    #endregion

    #region Public and private methods

    public override string ToString() => ViewModel.ToString();

    /// <summary>
    /// Обновить модель представления.
    /// </summary>
    public void SetupViewModel(IWsXamlViewModel viewModel)
    {
        if (viewModel is not WsXamlDialogViewModel dialogViewModel) return;
        base.SetupViewModel(dialogViewModel, gridLocal);

        WsFormNavigationUtils.ActionTryCatch(() =>
        {
            // Настроить список кнопок.
            SetupListButtons(gridLocal, 1, 0);
        });
    }

    #endregion
}