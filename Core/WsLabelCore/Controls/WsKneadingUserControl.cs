// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.Controls;

#nullable enable
public sealed partial class WsKneadingUserControl : WsBaseUserControl
{
    #region Private fields and properties

    public WsKneadingViewModel ViewModel { get; }
    private DateTime SaveProductDate { get; }
    private short SaveKneading { get; }
    private byte SavePalletSize { get; }
    private Guid PreviousPluScaleUid { get; set; }

    public WsKneadingUserControl()
    {
        InitializeComponent();

        ViewModel = new();
        PreviousPluScaleUid = Guid.Empty;
        SaveProductDate = UserSession.ProductDate;
        SaveKneading = UserSession.WeighingSettings.Kneading;
        SavePalletSize = UserSession.WeighingSettings.LabelsCountMain;
    }

    #endregion

    #region Public and private methods

    public override void RefreshAction()
    {
        WsWinFormNavigationUtils.ActionTryCatch(() =>
        {
            if (!UserSession.PluScale.IdentityValueUid.Equals(PreviousPluScaleUid))
            {
                PreviousPluScaleUid = UserSession.PluScale.IdentityValueUid;
                ShowPalletSize();
                SetGuiConfig();
                SetGuiLocalize();
                RefreshControlsText();
            }
        });
        buttonOk.Select();
    }

    private void RefreshControlsText()
    {
        fieldKneading.Text = $@"{UserSession.WeighingSettings.Kneading}";
        fieldProdDate.Text = UserSession.ProductDate.ToString("dd.MM.yyyy");
    }

    private void ButtonKneadingLeft_Click(object sender, EventArgs e)
    {
        WsWinFormNavigationUtils.ActionTryCatch(() =>
        {
            WsNumberInputForm numberInputForm = new() { InputValue = 0 };
            DialogResult result = numberInputForm.ShowDialog(this);
            numberInputForm.Close();
            numberInputForm.Dispose();
            if (result == DialogResult.OK)
                UserSession.WeighingSettings.Kneading = (byte)numberInputForm.InputValue;
            RefreshControlsText();
        });
    }

    private void ButtonClose_Click(object sender, EventArgs e)
    {
        WsWinFormNavigationUtils.ActionTryCatch(() =>
        {
            CheckWeightCount();
            UserSession.ProductDate = SaveProductDate;
            UserSession.WeighingSettings.Kneading = SaveKneading;
            UserSession.WeighingSettings.LabelsCountMain = SavePalletSize;
            ViewModel.ActionReturnCancel();
        });
    }

    private void CheckWeightCount()
    {
        if (UserSession.PluScale is { IsNotNew: true, Plu.IsCheckWeight: true } &&
            UserSession.WeighingSettings.LabelsCountMain > 1)
        {
            //WpfUtils.ShowNewOperationControl(this, LocaleCore.Scales.CheckPluWeightCount, true, LogType.Information, null, 
            //    UserSession.HostName, nameof(ScalesUI));
            UserSession.WeighingSettings.LabelsCountMain = 1;
        }
        fieldPalletSize.Text = $@"{UserSession.WeighingSettings.LabelsCountMain}";
    }

    private void ButtonOk_Click(object sender, EventArgs e)
    {
        WsWinFormNavigationUtils.ActionTryCatch(() =>
        {
            CheckWeightCount();
            ViewModel.ActionReturnOk();
        });
    }

    private void ButtonDtRight_Click(object sender, EventArgs e)
    {
        WsWinFormNavigationUtils.ActionTryCatch(() =>
        {
            UserSession.RotateProductDate(WsEnumDirection.Right);
            RefreshControlsText();
        });
    }

    private void ButtonDtLeft_Click(object sender, EventArgs e)
    {
        WsWinFormNavigationUtils.ActionTryCatch(() =>
        {
            UserSession.RotateProductDate(WsEnumDirection.Left);
            RefreshControlsText();
        });
    }

    private void ShowPalletSize()
    {
        fieldPalletSize.Text = UserSession.WeighingSettings.LabelsCountMain.ToString();
    }

    private void ButtonPalletSizeNext_Click(object sender, EventArgs e)
    {
        WsWinFormNavigationUtils.ActionTryCatch(() =>
        {
            UserSession.WeighingSettings.LabelsCountMain++;
            ShowPalletSize();
        });
    }

    private void ButtonPalletSizePrev_Click(object sender, EventArgs e)
    {
        WsWinFormNavigationUtils.ActionTryCatch(() =>
        {
            UserSession.WeighingSettings.LabelsCountMain--;
            ShowPalletSize();
        });
    }

    private void ButtonPalletSizeSet1_Click(object sender, EventArgs e)
    {
        SetLabelsCount(1);
    }

    private void ButtonPalletSizeSet10_Click(object sender, EventArgs e)
    {
        WsWinFormNavigationUtils.ActionTryCatch(() =>
        {
            int n = UserSession.WeighingSettings.LabelsCountMain == 1 ? 9 : 10;
            for (int i = 0; i < n; i++)
            {
                UserSession.WeighingSettings.LabelsCountMain++;
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
        WsWinFormNavigationUtils.ActionTryCatch(() =>
        {
            UserSession.WeighingSettings.LabelsCountMain = count;
            ShowPalletSize();
        });
    }

    private void KneadingUserControl_KeyUp(object sender, KeyEventArgs e)
    {
        WsWinFormNavigationUtils.ActionTryCatch(() =>
        {
            if (e.KeyCode == Keys.Escape)
            {
                ButtonClose_Click(sender, e);
            }
        });
    }

    private void SetGuiConfig()
    {
        // Kneading.
        labelKneading.Visible = fieldKneading.Visible = buttonKneading.Visible = UserSession.Scale.IsKneading;
        // Pallet size.
        labelPalletSize.Visible = fieldPalletSize.Visible = buttonPalletSizePrev.Visible = buttonPalletSizeNext.Visible =
            buttonPalletSize10.Visible = buttonSet1.Visible = buttonSet40.Visible = buttonSet60.Visible = buttonSet120.Visible =
                UserSession.PluScale.IsNotNew && !UserSession.PluScale.Plu.IsCheckWeight;
    }

    private void SetGuiLocalize()
    {
        labelKneading.Text = LocaleCore.Scales.FieldKneading;
        labelProdDate.Text = LocaleCore.Scales.FieldProductDate;
        labelPalletSize.Text = LocaleCore.Scales.FieldPalletSize;
        buttonOk.Text = LocaleCore.Buttons.Ok;
        buttonCancel.Text = LocaleCore.Buttons.Cancel;
    }

    #endregion
}