using System.Windows.Forms;

namespace WsLabelCore.Pages;

/// <summary>
/// WinForms-контрол замеса.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed partial class XamlKneadingUserControl : FormBaseUserControl, IFormUserControl
{
    #region Private fields and properties

    public XamlKneadingViewModel ViewModel => Page.ViewModel as XamlKneadingViewModel ?? new();
    /// <summary>
    /// Сохранение даты продукции.
    /// </summary>
    private DateTime SaveProductDate { get; set; }
    /// <summary>
    /// Сохранение замеса.
    /// </summary>
    private int SaveKneading { get; set; }
    /// <summary>
    /// Сохранение размера палеты.
    /// </summary>
    private byte SavePalletSize { get; set; }
    
    public XamlKneadingUserControl() : base(EnumNavigationPage.Kneading)
    {
        InitializeComponent();
    }

    #endregion

    #region Public and private methods

    public override string ToString() => ViewModel.ToString();

    /// <summary>
    /// Обновить контрол.
    /// </summary>
    public void SetupUserControl()
    {
        Page.SetupViewModel(ViewModel);

        FormNavigationUtils.ActionTryCatch(() =>
        {
            // Сохранить.
            SaveProductDate = LabelSession.ProductDate;
            SaveKneading = LabelSession.WeighingSettings.Kneading;
            SavePalletSize = LabelSession.WeighingSettings.LabelsCountMain;

            SetPalletSize();
            SetLocalization();
            SetupControls();
        });
        buttonYes.Select();
    }

    private void SetupControls()
    {
        // Замес.
        labelKneading.Visible = fieldKneading.Visible = buttonKneading.Visible = true;
        // Размер палеты.
        labelPalletSize.Visible = fieldPalletSize.Visible = buttonPalletSizePrev.Visible = buttonPalletSizeNext.Visible =
            buttonPalletSize10.Visible = buttonSet1.Visible = buttonSet40.Visible = buttonSet60.Visible = buttonSet120.Visible =
                LabelSession.PluLine is { IsExists: true, Plu.IsCheckWeight: false };

        fieldProdDate.Text = LabelSession.ProductDate.ToString("dd.MM.yyyy");
        fieldKneading.Text = $@"{LabelSession.WeighingSettings.Kneading}";
    }

    private void ButtonKneading_Click(object sender, EventArgs e)
    {
        FormNavigationUtils.ActionTryCatch(() =>
        {
            FormDigitsForm digitsForm = new() { InputValue = 0 };
            DialogResult result = digitsForm.ShowDialog(this);
            digitsForm.Close();
            digitsForm.Dispose();
            if (result == DialogResult.OK)
                LabelSession.WeighingSettings.Kneading = digitsForm.InputValue;
            SetupControls();
        });
    }

    /// <summary>
    /// Возврат Отмена.
    /// </summary>
    private void ButtonCancel_Click(object sender, EventArgs e)
    {
        FormNavigationUtils.ActionTryCatch(() =>
        {
            CheckWeightCount();
            LabelSession.ProductDate = SaveProductDate;
            LabelSession.WeighingSettings.Kneading = SaveKneading;
            LabelSession.WeighingSettings.LabelsCountMain = SavePalletSize;
            ViewModel.CmdCancel.Relay();
        });
    }

    private void CheckWeightCount()
    {
        if (LabelSession is { PluLine: { IsExists: true, Plu.IsCheckWeight: true }, WeighingSettings.LabelsCountMain: > 1 })
        {
            LabelSession.WeighingSettings.LabelsCountMain = 1;
        }
        fieldPalletSize.Text = $@"{LabelSession.WeighingSettings.LabelsCountMain}";
    }

    /// <summary>
    /// Возврат Да.
    /// </summary>
    private void ButtonYes_Click(object sender, EventArgs e)
    {
        FormNavigationUtils.ActionTryCatch(() =>
        {
            CheckWeightCount();
            ViewModel.CmdYes.Relay();
        });
    }

    private void ButtonDtRight_Click(object sender, EventArgs e)
    {
        FormNavigationUtils.ActionTryCatch(() =>
        {
            LabelSession.RotateProductDate(EnumDirection.Right);
            SetupControls();
        });
    }

    private void ButtonDtLeft_Click(object sender, EventArgs e)
    {
        FormNavigationUtils.ActionTryCatch(() =>
        {
            LabelSession.RotateProductDate(EnumDirection.Left);
            SetupControls();
        });
    }

    /// <summary>
    /// Задать размер палеты.
    /// </summary>
    private void SetPalletSize() => fieldPalletSize.Text = LabelSession.WeighingSettings.LabelsCountMain.ToString();

    private void ButtonPalletSizeNext_Click(object sender, EventArgs e)
    {
        FormNavigationUtils.ActionTryCatch(() =>
        {
            LabelSession.WeighingSettings.LabelsCountMain++;
            SetPalletSize();
        });
    }

    private void ButtonPalletSizePrev_Click(object sender, EventArgs e)
    {
        FormNavigationUtils.ActionTryCatch(() =>
        {
            LabelSession.WeighingSettings.LabelsCountMain--;
            SetPalletSize();
        });
    }

    private void ButtonPalletSizeSet1_Click(object sender, EventArgs e)
    {
        SetLabelsCount(1);
    }

    private void ButtonPalletSizeSet10_Click(object sender, EventArgs e)
    {
        FormNavigationUtils.ActionTryCatch(() =>
        {
            int n = LabelSession.WeighingSettings.LabelsCountMain == 1 ? 9 : 10;
            for (int i = 0; i < n; i++)
            {
                LabelSession.WeighingSettings.LabelsCountMain++;
                SetPalletSize();
            }
        });
    }

    private void ButtonPalletSizeSet40_Click(object sender, EventArgs e)
    {
        SetLabelsCount(40);
    }

    private void ButtonPalletSizeSet60_Click(object sender, EventArgs e)
    {
        SetLabelsCount(60);
    }

    private void ButtonPalletSizeSet120_Click(object sender, EventArgs e)
    {
        SetLabelsCount(120);
    }

    private void SetLabelsCount(byte count)
    {
        FormNavigationUtils.ActionTryCatch(() =>
        {
            LabelSession.WeighingSettings.LabelsCountMain = count;
            SetPalletSize();
        });
    }

    private void KneadingUserControl_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
    {
        FormNavigationUtils.ActionTryCatch(() =>
        {
            if (e.KeyCode == Keys.Escape)
            {
                ButtonCancel_Click(sender, e);
            }
        });
    }

    /// <summary>
    /// Локализация.
    /// </summary>
    private void SetLocalization()
    {
        // Замес.
        labelKneading.Text = LocaleCore.LabelPrint.FieldKneading;
        // Дата продукции.
        labelProdDate.Text = LocaleCore.LabelPrint.FieldProductDate;
        // Размер палеты.
        labelPalletSize.Text = LocaleCore.LabelPrint.FieldPalletSize;
        // Кнопки.
        buttonYes.Text = LocaleCore.Buttons.Ok;
        buttonCancel.Text = LocaleCore.Buttons.Cancel;
    }

    #endregion
}