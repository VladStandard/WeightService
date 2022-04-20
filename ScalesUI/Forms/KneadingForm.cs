// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System;
using WeightCore.Gui;
using WeightCore.Helpers;
using DataCore.Localizations;

namespace ScalesUI.Forms
{
    public partial class KneadingForm : Form
    {
        #region Private fields and properties

        private DateTime SaveProductDate { get; }
        private DebugHelper Debug { get; set; } = DebugHelper.Instance;
        private ExceptionHelper Exception { get; set; } = ExceptionHelper.Instance;
        private byte SaveKneading { get; }
        private byte SavePalletSize { get; }
        private SessionStateHelper SessionState { get; set; } = SessionStateHelper.Instance;

        #endregion

        #region Constructor and destructor

        public KneadingForm()
        {
            InitializeComponent();

            SaveProductDate = SessionState.ProductDate;
            SaveKneading = SessionState.WeighingSettings.Kneading;
            SavePalletSize = SessionState.WeighingSettings.CurrentLabelsCountMain;
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
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true);
            }
            finally
            {
                buttonOk.Select();
            }
        }

        private void ShowProductDate([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            try
            {
                fieldProdDate.Text = SessionState.ProductDate.ToString("dd.MM.yyyy");
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true, filePath, lineNumber, memberName);
            }
        }

        private void GuiUpdate([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            try
            {
                fieldKneading.Text = $"{SessionState.WeighingSettings.Kneading}";
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true, filePath, lineNumber, memberName);
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
                    SessionState.WeighingSettings.Kneading = (byte)numberInputForm.InputValue;
                GuiUpdate();
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true);
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
                Exception.Catch(this, ref ex, true);
            }
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            try
            {
                CheckWeightCount();
                DialogResult = DialogResult.Cancel;
                SessionState.ProductDate = SaveProductDate;
                SessionState.WeighingSettings.Kneading = SaveKneading;
                SessionState.WeighingSettings.CurrentLabelsCountMain = SavePalletSize;
                Close();
            }
            catch (Exception ex)
            {
                DialogResult = DialogResult.Abort;
                Exception.Catch(this, ref ex, true);
            }
        }

        private void CheckWeightCount()
        {
            if (SessionState.CurrentPlu == null)
                return;

            if (SessionState.CurrentPlu.IsCheckWeight == true && SessionState.WeighingSettings.CurrentLabelsCountMain > 1)
            {
                GuiUtils.WpfForm.ShowNewOperationControl(this, LocaleCore.Scales.CheckPluWeightCount);
                SessionState.WeighingSettings.CurrentLabelsCountMain = 1;
            }
            fieldPalletSize.Text = $"{SessionState.WeighingSettings.CurrentLabelsCountMain}";
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
                Exception.Catch(this, ref ex, true);
            }
        }

        private void ButtonDtRight_Click(object sender, EventArgs e)
        {
            try
            {
                SessionState.RotateProductDate(ProjectsEnums.Direction.Right);
                ShowProductDate();
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true);
            }
        }

        private void ButtonDtLeft_Click(object sender, EventArgs e)
        {
            try
            {
                SessionState.RotateProductDate(ProjectsEnums.Direction.Left);
                ShowProductDate();
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true);
            }
        }

        private void ButtonPalletSize10_Click(object sender, EventArgs e)
        {
            try
            {
                int n = SessionState.WeighingSettings.CurrentLabelsCountMain == 1 ? 9 : 10;
                for (int i = 0; i < n; i++)
                {
                    SessionState.WeighingSettings.CurrentLabelsCountMain++;
                    ShowPalletSize();
                }
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true);
            }
        }

        private void ShowPalletSize()
        {
            fieldPalletSize.Text = SessionState.WeighingSettings.CurrentLabelsCountMain.ToString();
        }

        private void ButtonPalletSizeNext_Click(object sender, EventArgs e)
        {
            try
            {
                SessionState.WeighingSettings.CurrentLabelsCountMain++;
                ShowPalletSize();
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true);
            }
        }

        private void ButtonPalletSizePrev_Click(object sender, EventArgs e)
        {
            try
            {
                SessionState.WeighingSettings.CurrentLabelsCountMain--;
                ShowPalletSize();
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true);
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

        private void SetLabelsCount(byte count,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            try
            {
                SessionState.WeighingSettings.CurrentLabelsCountMain = count;
                ShowPalletSize();
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true, filePath, lineNumber, memberName);
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
                Exception.Catch(this, ref ex, true);
            }
        }

        #endregion
    }
}
