// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows.Forms;
using WsLocalizationCore.Utils;

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
    private DateTime SaveProductDate { get; }
    private short SaveKneading { get; }
    private byte SavePalletSize { get; }
    private Guid PreviousPluScaleUid { get; set; } = Guid.Empty;

    public WsXamlKneadingUserControl() : base(WsEnumNavigationPage.Kneading)
    {
        InitializeComponent();

        SaveProductDate = LabelSession.ProductDate;
        SaveKneading = LabelSession.WeighingSettings.Kneading;
        SavePalletSize = LabelSession.WeighingSettings.LabelsCountMain;
    }

    #endregion

    #region Public and private methods

    public override string ToString() => ViewModel.ToString();

    /// <summary>
    /// Обновить контрол.
    /// </summary>
    public void SetupUserConrol()
    {
        Page.SetupViewModel(ViewModel);

        WsFormNavigationUtils.ActionTryCatch(() =>
        {
            if (!LabelSession.PluLine.IdentityValueUid.Equals(PreviousPluScaleUid))
            {
                PreviousPluScaleUid = LabelSession.PluLine.IdentityValueUid;
                ShowPalletSize();
                SetGuiConfig();
                SetGuiLocalize();
                RefreshControlsText();
            }
        });
        buttonYes.Select();
    }

    private void RefreshControlsText()
    {
        fieldKneading.Text = $@"{LabelSession.WeighingSettings.Kneading}";
        fieldProdDate.Text = LabelSession.ProductDate.ToString("dd.MM.yyyy");
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
            RefreshControlsText();
        });
    }

    private void ButtonClose_Click(object sender, EventArgs e)
    {
        WsFormNavigationUtils.ActionTryCatch(() =>
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
        if (LabelSession.PluLine is { IsNotNew: true, Plu.IsCheckWeight: true } &&
            LabelSession.WeighingSettings.LabelsCountMain > 1)
        {
            //WpfUtils.ShowNewOperationControl(this, LocaleCore.Scales.CheckPluWeightCount, true, LogType.Information, null, 
            //    LabelSession.HostName, nameof(ScalesUI));
            LabelSession.WeighingSettings.LabelsCountMain = 1;
        }
        fieldPalletSize.Text = $@"{LabelSession.WeighingSettings.LabelsCountMain}";
    }

    private void ButtonOk_Click(object sender, EventArgs e)
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
            RefreshControlsText();
        });
    }

    private void ButtonDtLeft_Click(object sender, EventArgs e)
    {
        WsFormNavigationUtils.ActionTryCatch(() =>
        {
            LabelSession.RotateProductDate(WsEnumDirection.Left);
            RefreshControlsText();
        });
    }

    private void ShowPalletSize()
    {
        fieldPalletSize.Text = LabelSession.WeighingSettings.LabelsCountMain.ToString();
    }

    private void ButtonPalletSizeNext_Click(object sender, EventArgs e)
    {
        WsFormNavigationUtils.ActionTryCatch(() =>
        {
            LabelSession.WeighingSettings.LabelsCountMain++;
            ShowPalletSize();
        });
    }

    private void ButtonPalletSizePrev_Click(object sender, EventArgs e)
    {
        WsFormNavigationUtils.ActionTryCatch(() =>
        {
            LabelSession.WeighingSettings.LabelsCountMain--;
            ShowPalletSize();
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
                ShowPalletSize();
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
            ShowPalletSize();
        });
    }

    private void KneadingUserControl_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
    {
        WsFormNavigationUtils.ActionTryCatch(() =>
        {
            if (e.KeyCode == Keys.Escape)
            {
                ButtonClose_Click(sender, e);
            }
        });
    }

    private void SetGuiConfig()
    {
        // Замес.
        labelKneading.Visible = fieldKneading.Visible = buttonKneading.Visible = LabelSession.Line.IsKneading;
        // Инкремент счётчика.
        labelIsIncrementCounter.Visible = checkBoxIsIncrementCounter.Visible =
        // Размер палеты.
        labelPalletSize.Visible = fieldPalletSize.Visible = buttonPalletSizePrev.Visible = buttonPalletSizeNext.Visible =
            buttonPalletSize10.Visible = buttonSet1.Visible = buttonSet40.Visible = buttonSet60.Visible = buttonSet120.Visible =
                LabelSession.PluLine is { IsNotNew: true, Plu.IsCheckWeight: false };
    }

    /// <summary>
    /// Локализация.
    /// </summary>
    private void SetGuiLocalize()
    {
        // Замес.
        labelKneading.Text = WsLocaleCore.LabelPrint.FieldKneading;
        // Дата продукции.
        labelProdDate.Text = WsLocaleCore.LabelPrint.FieldProductDate;
        // Размер палеты.
        labelPalletSize.Text = WsLocaleCore.LabelPrint.FieldPalletSize;
        // Инкремент счётчика.
        labelIsIncrementCounter.Text = WsLocaleCore.LabelPrint.FieldPrintCounter;
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