namespace WsLabelCore.Pages;

/// <summary>
/// Страница оджидания.
/// </summary>
[DebuggerDisplay("{ToString()}")]
#nullable enable
public partial class WsXamlWaitPage
{
    #region Public and private fields, properties, constructor

    public WsXamlWaitPage()
    {
        InitializeComponent();
    }

    #endregion

    #region Public and private methods

    /// <summary>
    /// Обновить модель представления.
    /// </summary>
    public void SetupViewModel(WsXamlWaitViewModel viewModel)
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