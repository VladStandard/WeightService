// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Localizations;
using System;
using System.Windows.Forms;
using DataCore.Helpers;
using DataCore.Models;
using WeightCore.Gui;
using WeightCore.Helpers;

namespace ScalesUI.Forms;

public partial class KneadingForm : Form
{
	#region Private fields and properties

	private DateTime SaveProductDate { get; }
	private DebugHelper Debug { get; } = DebugHelper.Instance;
	private short SaveKneading { get; }
	private byte SavePalletSize { get; }
	private UserSessionHelper UserSession { get; } = UserSessionHelper.Instance;

	#endregion

	#region Constructor and destructor

	public KneadingForm()
	{
		InitializeComponent();

		SaveProductDate = UserSession.ProductDate;
		SaveKneading = UserSession.WeighingSettings.Kneading;
		SavePalletSize = UserSession.WeighingSettings.LabelsCountMain;
	}

	#endregion

	#region Public and private methods

	private void KneadingForm_Load(object sender, EventArgs e)
	{
		try
		{
			TopMost = !Debug.IsDebug;
			Width = Owner.Width;
			Height = Owner.Height;
			Left = Owner.Left;
			Top = Owner.Top;
			StartPosition = FormStartPosition.CenterParent;
			ShowPalletSize();
			SetGuiConfig();
			SetGuiLocalize();
		}
		catch (Exception ex)
		{
			GuiUtils.WpfForm.CatchException(this, ex);
		}
		finally
		{
			buttonOk.Select();
		}
	}

	private void ShowProductDate()
	{
		try
		{
			fieldProdDate.Text = UserSession.ProductDate.ToString("dd.MM.yyyy");
		}
		catch (Exception ex)
		{
			GuiUtils.WpfForm.CatchException(ex);
		}
	}

	private void GuiUpdate()
	{
		try
		{
			fieldKneading.Text = $@"{UserSession.WeighingSettings.Kneading}";
		}
		catch (Exception ex)
		{
			GuiUtils.WpfForm.CatchException(this, ex);
		}
	}

	private void ButtonKneadingLeft_Click(object sender, EventArgs e)
	{
		try
		{
			NumberInputForm numberInputForm = new();
			numberInputForm.InputValue = 0;
			DialogResult result = numberInputForm.ShowDialog(this);
			numberInputForm.Close();
			numberInputForm.Dispose();
			if (result == DialogResult.OK)
				UserSession.WeighingSettings.Kneading = (byte)numberInputForm.InputValue;
			GuiUpdate();
		}
		catch (Exception ex)
		{
			GuiUtils.WpfForm.CatchException(this, ex);
		}
	}

	private void SetKneadingNumberForm_Shown(object sender, EventArgs e)
	{
		try
		{
			GuiUpdate();
			ShowProductDate();
		}
		catch (Exception ex)
		{
			GuiUtils.WpfForm.CatchException(this, ex);
		}
	}

	private void ButtonClose_Click(object sender, EventArgs e)
	{
		try
		{
			CheckWeightCount();
			DialogResult = DialogResult.Cancel;
			UserSession.ProductDate = SaveProductDate;
			UserSession.WeighingSettings.Kneading = SaveKneading;
			UserSession.WeighingSettings.LabelsCountMain = SavePalletSize;
			Close();
		}
		catch (Exception ex)
		{
			DialogResult = DialogResult.Abort;
			GuiUtils.WpfForm.CatchException(this, ex);
		}
	}

	private void CheckWeightCount()
	{
		if (UserSession.PluScale.Identity.IsNotNew() && UserSession.PluScale.Plu.IsCheckWeight && 
		    UserSession.WeighingSettings.LabelsCountMain > 1)
		{
			//GuiUtils.WpfForm.ShowNewOperationControl(this, LocaleCore.Scales.CheckPluWeightCount, true, LogType.Information, null, 
			//    UserSession.Scale.Host.HostName, nameof(ScalesUI));
			UserSession.WeighingSettings.LabelsCountMain = 1;
		}
		fieldPalletSize.Text = $@"{UserSession.WeighingSettings.LabelsCountMain}";
	}

	private void ButtonOk_Click(object sender, EventArgs e)
	{
		try
		{
			CheckWeightCount();
			DialogResult = DialogResult.OK;
			Close();
		}
		catch (Exception ex)
		{
			DialogResult = DialogResult.Abort;
			GuiUtils.WpfForm.CatchException(this, ex);
		}
	}

	private void ButtonDtRight_Click(object sender, EventArgs e)
	{
		try
		{
			UserSession.RotateProductDate(DirectionEnum.Right);
			ShowProductDate();
		}
		catch (Exception ex)
		{
			GuiUtils.WpfForm.CatchException(this, ex);
		}
	}

	private void ButtonDtLeft_Click(object sender, EventArgs e)
	{
		try
		{
			UserSession.RotateProductDate(DirectionEnum.Left);
			ShowProductDate();
		}
		catch (Exception ex)
		{
			GuiUtils.WpfForm.CatchException(this, ex);
		}
	}

	private void ButtonPalletSize10_Click(object sender, EventArgs e)
	{
		try
		{
			int n = UserSession.WeighingSettings.LabelsCountMain == 1 ? 9 : 10;
			for (int i = 0; i < n; i++)
			{
				UserSession.WeighingSettings.LabelsCountMain++;
				ShowPalletSize();
			}
		}
		catch (Exception ex)
		{
			GuiUtils.WpfForm.CatchException(this, ex);
		}
	}

	private void ShowPalletSize()
	{
		fieldPalletSize.Text = UserSession.WeighingSettings.LabelsCountMain.ToString();
	}

	private void ButtonPalletSizeNext_Click(object sender, EventArgs e)
	{
		try
		{
			UserSession.WeighingSettings.LabelsCountMain++;
			ShowPalletSize();
		}
		catch (Exception ex)
		{
			GuiUtils.WpfForm.CatchException(this, ex);
		}
	}

	private void ButtonPalletSizePrev_Click(object sender, EventArgs e)
	{
		try
		{
			UserSession.WeighingSettings.LabelsCountMain--;
			ShowPalletSize();
		}
		catch (Exception ex)
		{
			GuiUtils.WpfForm.CatchException(this, ex);
		}
	}

	private void ButtonSet40_Click(object sender, EventArgs e)
	{
		SetLabelsCount(40);
	}

	private void ButtonSet60_Click(object sender, EventArgs e)
	{
		SetLabelsCount(60);
	}

	private void ButtonSet120_Click(object sender, EventArgs e)
	{
		SetLabelsCount(120);
	}

	private void ButtonSet1_Click(object sender, EventArgs e)
	{
		SetLabelsCount(1);
	}

	private void SetLabelsCount(byte count)
	{
		try
		{
			UserSession.WeighingSettings.LabelsCountMain = count;
			ShowPalletSize();
		}
		catch (Exception ex)
		{
			GuiUtils.WpfForm.CatchException(this, ex);
		}
	}

	private void SetKneadingNumberForm_KeyUp(object sender, KeyEventArgs e)
	{
		try
		{
			if (e.KeyCode == Keys.Escape)
			{
				ButtonClose_Click(sender, e);
			}
		}
		catch (Exception ex)
		{
			GuiUtils.WpfForm.CatchException(this, ex);
		}
	}

	private void SetGuiConfig()
	{
		// Kneading.
		labelKneading.Visible = fieldKneading.Visible = buttonKneading.Visible = UserSession.Scale.IsKneading;
		// Pallet size.
		labelPalletSize.Visible = fieldPalletSize.Visible = buttonPalletSizePrev.Visible = buttonPalletSizeNext.Visible = 
			buttonPalletSize10.Visible = buttonSet1.Visible = buttonSet40.Visible = buttonSet60.Visible = buttonSet120.Visible = 
				UserSession.PluScale.Identity.IsNotNew() && !UserSession.PluScale.Plu.IsCheckWeight;
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