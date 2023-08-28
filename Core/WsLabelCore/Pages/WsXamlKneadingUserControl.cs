using System.Windows.Forms;

namespace WsLabelCore.Pages;

/// <summary>
/// WinForms-контрол замеса.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public sealed partial class WsXamlKneadingUserControl : WsFormBaseUserControl, IWsFormUserControl
{
    #region Private fields and properties

    public WsXamlKneadingViewModel ViewModel => Page.ViewModel as WsXamlKneadingViewModel ?? new();
    /// <summary>
    /// Сохранение даты продукции.
    /// </summary>
    private DateTime SaveProductDate { get; set; }
    /// <summary>
    /// Сохранение замеса.
    /// </summary>
    private short SaveKneading { get; set; }
    /// <summary>
    /// Сохранение размера палеты.
    /// </summary>
    private byte SavePalletSize { get; set; }
    /// <summary>
    /// Сохранение использования инкремента счётчика этикеток.
    /// </summary>
    private bool SaveIsIncrementCounter { get; set; }

    public WsXamlKneadingUserControl() : base(WsEnumNavigationPage.Kneading)
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

        WsFormNavigationUtils.ActionTryCatch(() =>
        {
            // Сохранить.
            SaveIsIncrementCounter = LabelSession.IsIncrementCounter;
            checkBoxIsIncrementCounter.Checked = SaveIsIncrementCounter;
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
        labelKneading.Visible = fieldKneading.Visible = buttonKneading.Visible = LabelSession.Line.IsKneading;
        // Инкремент счётчика.
        labelCounter.Visible = checkBoxIsIncrementCounter.Visible =
            // Размер палеты.
            labelPalletSize.Visible = fieldPalletSize.Visible = buttonPalletSizePrev.Visible = buttonPalletSizeNext.Visible =
                buttonPalletSize10.Visible = buttonSet1.Visible = buttonSet40.Visible = buttonSet60.Visible = buttonSet120.Visible =
                    LabelSession.PluLine is { IsNotNew: true, Plu.IsCheckWeight: false };

        fieldProdDate.Text = LabelSession.ProductDate.ToString("dd.MM.yyyy");
        fieldKneading.Text = $@"{LabelSession.WeighingSettings.Kneading}";
    }

    private void ButtonKneading_Click(object sender, EventArgs e)
    {
        WsFormNavigationUtils.ActionTryCatch(() =>
        {
            WsFormDigitsForm digitsForm = new() { InputValue = 0 };
            DialogResult result = digitsForm.ShowDialog(this);
            digitsForm.Close();
            digitsForm.Dispose();
            if (result == DialogResult.OK)
                LabelSession.WeighingSettings.Kneading = (byte)digitsForm.InputValue;
            SetupControls();
        });
    }

    /// <summary>
    /// Возврат Отмена.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ButtonCancel_Click(object sender, EventArgs e)
    {
        WsFormNavigationUtils.ActionTryCatch(() =>
        {
            CheckWeightCount();
            LabelSession.SetIsIncrementCounter(SaveIsIncrementCounter);
            LabelSession.ProductDate = SaveProductDate;
            LabelSession.WeighingSettings.Kneading = SaveKneading;
            LabelSession.WeighingSettings.LabelsCountMain = SavePalletSize;
            ViewModel.CmdCancel.Relay();
        });
    }

    private void CheckWeightCount()
    {
        if (LabelSession.PluLine is { IsNotNew: true, Plu.IsCheckWeight: true } &&
            LabelSession.WeighingSettings.LabelsCountMain > 1)
        {
            //WpfUtils.ShowNewOperationControl(this, LocaleCore.Scales.CheckPluWeightCount, true, LogType.Information, null, 
            //    LabelSession.HostName, nameof(ScalesUI));
            LabelSession.WeighingSettings.LabelsCountMain = 1;
        }
        fieldPalletSize.Text = $@"{LabelSession.WeighingSettings.LabelsCountMain}";
    }

    /// <summary>
    /// Возврат Да.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ButtonYes_Click(object sender, EventArgs e)
    {
        WsFormNavigationUtils.ActionTryCatch(() =>
        {
            CheckWeightCount();
            checkBoxIsIncrementCounter_CheckedChanged(checkBoxIsIncrementCounter, e);
            ViewModel.CmdYes.Relay();
        });
    }

    private void ButtonDtRight_Click(object sender, EventArgs e)
    {
        WsFormNavigationUtils.ActionTryCatch(() =>
        {
            LabelSession.RotateProductDate(WsEnumDirection.Right);
            SetupControls();
        });
    }

    private void ButtonDtLeft_Click(object sender, EventArgs e)
    {
        WsFormNavigationUtils.ActionTryCatch(() =>
        {
            LabelSession.RotateProductDate(WsEnumDirection.Left);
            SetupControls();
        });
    }

    /// <summary>
    /// Задать размер палеты.
    /// </summary>
    private void SetPalletSize() => fieldPalletSize.Text = LabelSession.WeighingSettings.LabelsCountMain.ToString();

    private void ButtonPalletSizeNext_Click(object sender, EventArgs e)
    {
        WsFormNavigationUtils.ActionTryCatch(() =>
        {
            LabelSession.WeighingSettings.LabelsCountMain++;
            SetPalletSize();
        });
    }

    private void ButtonPalletSizePrev_Click(object sender, EventArgs e)
    {
        WsFormNavigationUtils.ActionTryCatch(() =>
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
        WsFormNavigationUtils.ActionTryCatch(() =>
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
        WsFormNavigationUtils.ActionTryCatch(() =>
        {
            LabelSession.WeighingSettings.LabelsCountMain = count;
            SetPalletSize();
        });
    }

    private void KneadingUserControl_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
    {
        WsFormNavigationUtils.ActionTryCatch(() =>
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
        labelKneading.Text = WsLocaleCore.LabelPrint.FieldKneading;
        // Дата продукции.
        labelProdDate.Text = WsLocaleCore.LabelPrint.FieldProductDate;
        // Размер палеты.
        labelPalletSize.Text = WsLocaleCore.LabelPrint.FieldPalletSize;
        // Инкремент счётчика.
        labelCounter.Text = WsLocaleCore.LabelPrint.FieldLabelCounter;
        checkBoxIsIncrementCounter.Text = WsLocaleCore.LabelPrint.FieldIsIncrementCounterEnable;
        // Кнопки.
        buttonYes.Text = WsLocaleCore.Buttons.Ok;
        buttonCancel.Text = WsLocaleCore.Buttons.Cancel;
    }

    /// <summary>
    /// Инкремент счётчика печати штучной продукции.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void checkBoxIsIncrementCounter_CheckedChanged(object sender, EventArgs e)
    {
        WsFormNavigationUtils.ActionTryCatch(() =>
        {
            if (sender is not CheckBox checkBox) return;
            LabelSession.SetIsIncrementCounter(checkBox.Checked);
        });
    }

    #endregion
}