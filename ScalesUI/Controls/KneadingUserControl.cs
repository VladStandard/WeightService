// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsWeight.Gui;
using WsWeight.Utils;

namespace ScalesUI.Controls;

public partial class KneadingUserControl : UserControlBase
{
    #region Private fields and properties

    private DateTime SaveProductDate { get; }
    private short SaveKneading { get; }
    private byte SavePalletSize { get; }
    private Guid PreviousPluScaleUid { get; set; }

    public KneadingUserControl()
    {
        InitializeComponent();

        PreviousPluScaleUid = Guid.Empty;
        SaveProductDate = UserSession.ProductDate;
        SaveKneading = UserSession.WeighingSettings.Kneading;
        SavePalletSize = UserSession.WeighingSettings.LabelsCountMain;
        RefreshAction = KneadingUserControl_Refresh;
    }

    #endregion

    #region Public and private methods

    private void KneadingUserControl_Refresh()
    {
        ActionUtils.ActionTryCatchFinally(this, () =>
        {
            if (!UserSession.PluScale.IdentityValueUid.Equals(PreviousPluScaleUid))
            {
                PreviousPluScaleUid = UserSession.PluScale.IdentityValueUid;
                ShowPalletSize();
                SetGuiConfig();
                SetGuiLocalize();
                RefreshControlsText();
            }
        }, buttonOk.Select);
    }

    private void RefreshControlsText()
    {
        fieldKneading.Text = $@"{UserSession.WeighingSettings.Kneading}";
        fieldProdDate.Text = UserSession.ProductDate.ToString("dd.MM.yyyy");
    }

    private void ButtonKneadingLeft_Click(object sender, EventArgs e)
    {
        ActionUtils.ActionTryCatch(this, () =>
        {
            NumberInputForm numberInputForm = new() { InputValue = 0 };
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
        ActionUtils.ActionTryCatch(this, () =>
        {
            CheckWeightCount();
            Result = DialogResult.Cancel;
            UserSession.ProductDate = SaveProductDate;
            UserSession.WeighingSettings.Kneading = SaveKneading;
            UserSession.WeighingSettings.LabelsCountMain = SavePalletSize;
            ReturnBackAction();
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
        ActionUtils.ActionTryCatch(this, () =>
        {
            CheckWeightCount();
            Result = DialogResult.OK;
            ReturnBackAction();
        });
    }

    private void ButtonDtRight_Click(object sender, EventArgs e)
    {
        ActionUtils.ActionTryCatch(this, () =>
        {
            UserSession.RotateProductDate(DirectionEnum.Right);
            RefreshControlsText();
        });
    }

    private void ButtonDtLeft_Click(object sender, EventArgs e)
    {
        ActionUtils.ActionTryCatch(this, () =>
        {
            UserSession.RotateProductDate(DirectionEnum.Left);
            RefreshControlsText();
        });
    }

    private void ShowPalletSize()
    {
        fieldPalletSize.Text = UserSession.WeighingSettings.LabelsCountMain.ToString();
    }

    private void ButtonPalletSizeNext_Click(object sender, EventArgs e)
    {
        ActionUtils.ActionTryCatch(this, () =>
        {
            UserSession.WeighingSettings.LabelsCountMain++;
            ShowPalletSize();
        });
    }

    private void ButtonPalletSizePrev_Click(object sender, EventArgs e)
    {
        ActionUtils.ActionTryCatch(this, () =>
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
        ActionUtils.ActionTryCatch(this, () =>
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
        ActionUtils.ActionTryCatch(this, () =>
        {
            UserSession.WeighingSettings.LabelsCountMain = count;
            ShowPalletSize();
        });
    }

    private void KneadingUserControl_KeyUp(object sender, KeyEventArgs e)
    {
        ActionUtils.ActionTryCatch(this, () =>
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