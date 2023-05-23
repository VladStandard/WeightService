// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.Controls;

#nullable enable
public sealed partial class WsMoreUserControl : WsBaseUserControl
{
    #region Private fields and properties

    public WsMoreViewModel ViewModel { get; }
    private DateTime SaveProductDate { get; }
    private short SaveKneading { get; }
    private byte SavePalletSize { get; }
    private Guid PreviousPluScaleUid { get; set; }

    public WsMoreUserControl()
    {
        InitializeComponent();

        ViewModel = new();
        PreviousPluScaleUid = Guid.Empty;
        SaveProductDate = LabelSession.ProductDate;
        SaveKneading = LabelSession.WeighingSettings.Kneading;
        SavePalletSize = LabelSession.WeighingSettings.LabelsCountMain;
    }

    #endregion

    #region Public and private methods

    public override void RefreshAction()
    {
        WsWinFormNavigationUtils.ActionTryCatch(() =>
        {
            if (!LabelSession.PluScale.IdentityValueUid.Equals(PreviousPluScaleUid))
            {
                PreviousPluScaleUid = LabelSession.PluScale.IdentityValueUid;
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
        fieldKneading.Text = $@"{LabelSession.WeighingSettings.Kneading}";
        fieldProdDate.Text = LabelSession.ProductDate.ToString("dd.MM.yyyy");
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
                LabelSession.WeighingSettings.Kneading = (byte)numberInputForm.InputValue;
            RefreshControlsText();
        });
    }

    private void ButtonClose_Click(object sender, EventArgs e)
    {
        WsWinFormNavigationUtils.ActionTryCatch(() =>
        {
            CheckWeightCount();
            LabelSession.ProductDate = SaveProductDate;
            LabelSession.WeighingSettings.Kneading = SaveKneading;
            LabelSession.WeighingSettings.LabelsCountMain = SavePalletSize;
            ViewModel.ActionReturnCancel();
        });
    }

    private void CheckWeightCount()
    {
        if (LabelSession.PluScale is { IsNotNew: true, Plu.IsCheckWeight: true } &&
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
            LabelSession.RotateProductDate(WsEnumDirection.Right);
            RefreshControlsText();
        });
    }

    private void ButtonDtLeft_Click(object sender, EventArgs e)
    {
        WsWinFormNavigationUtils.ActionTryCatch(() =>
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
        WsWinFormNavigationUtils.ActionTryCatch(() =>
        {
            LabelSession.WeighingSettings.LabelsCountMain++;
            ShowPalletSize();
        });
    }

    private void ButtonPalletSizePrev_Click(object sender, EventArgs e)
    {
        WsWinFormNavigationUtils.ActionTryCatch(() =>
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
        WsWinFormNavigationUtils.ActionTryCatch(() =>
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
        WsWinFormNavigationUtils.ActionTryCatch(() =>
        {
            LabelSession.WeighingSettings.LabelsCountMain = count;
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
        labelKneading.Visible = fieldKneading.Visible = buttonKneading.Visible = LabelSession.Scale.IsKneading;
        // Pallet size.
        labelPalletSize.Visible = fieldPalletSize.Visible = buttonPalletSizePrev.Visible = buttonPalletSizeNext.Visible =
            buttonPalletSize10.Visible = buttonSet1.Visible = buttonSet40.Visible = buttonSet60.Visible = buttonSet120.Visible =
                LabelSession.PluScale.IsNotNew && !LabelSession.PluScale.Plu.IsCheckWeight;
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