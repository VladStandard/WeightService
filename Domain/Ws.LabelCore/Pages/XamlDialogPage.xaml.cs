namespace Ws.LabelCore.Pages;

/// <summary>
/// Страница диалога.
/// </summary>
[DebuggerDisplay("{ToString()}")]
#nullable enable
public partial class XamlDialogPage
{
    #region Public and private fields, properties, constructor

    public XamlDialogPage()
    {
        InitializeComponent();
    }

    #endregion

    #region Public and private methods

    public override string ToString() => ViewModel.ToString();

    /// <summary>
    /// Обновить модель представления.
    /// </summary>
    public void SetupViewModel(XamlDialogViewModel viewModel)
    {
        SetupViewModel(viewModel, gridLocal);

        FormNavigationUtils.ActionTryCatch(() =>
        {
            // Настроить список кнопок.
            SetupListButtons(gridLocal, 1, 0);
        });
    }

    #endregion
}