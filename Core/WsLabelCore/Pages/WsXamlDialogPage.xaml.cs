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
    public void SetupViewModel(WsXamlDialogViewModel viewModel)
    {
        SetupViewModel(viewModel, gridLocal);

        WsFormNavigationUtils.ActionTryCatch(() =>
        {
            // Настроить список кнопок.
            SetupListButtons(gridLocal, 1, 0);
        });
    }

    #endregion
}