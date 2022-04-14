// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using WeightCore.Gui;
using WeightCore.Helpers;

namespace ScalesUI.Forms
{
    public partial class KneadingForm : Form
    {
        #region Private fields and properties

        private DateTime SaveProductDate { get; }
        private DebugHelper Debug { get; set; } = DebugHelper.Instance;
        private ExceptionHelper Exception { get; set; } = ExceptionHelper.Instance;
        private int SaveKneading { get; }
        private int SavePalletSize { get; }
        private SessionStateHelper SessionState { get; set; } = SessionStateHelper.Instance;

        #endregion

        #region Constructor and destructor

        public KneadingForm()
        {
            InitializeComponent();

            SaveKneading = SessionState.Kneading;
            SaveProductDate = SessionState.ProductDate;
            SavePalletSize = SessionState.CurrentLabelsCountMain;
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
                fieldKneading.Text = SessionState.Kneading.ToString();
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
                    SessionState.Kneading = numberInputForm.InputValue;
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
                SessionState.Kneading = SaveKneading;
                SessionState.ProductDate = SaveProductDate;
                SessionState.CurrentLabelsCountMain = SavePalletSize;
                Close();
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true);
            }
        }

        private void CheckWeightCount()
        {
            if (SessionState.CurrentPlu == null)
                return;

            if (SessionState.CurrentPlu.IsCheckWeight == true && SessionState.CurrentLabelsCountMain > 1)
            {
                // WPF MessageBox.
                using WpfPageLoader wpfPageLoader = new(ProjectsEnums.Page.MessageBox, false) { Width = 700, Height = 400 };
                wpfPageLoader.MessageBox.Caption = LocalizationData.ScalesUI.OperationControl;
                wpfPageLoader.MessageBox.Message = LocalizationData.ScalesUI.CheckPluWeightCount;
                wpfPageLoader.MessageBox.VisibilitySettings.ButtonOkVisibility = System.Windows.Visibility.Visible;
                wpfPageLoader.MessageBox.Localization();
                wpfPageLoader.ShowDialog(this);
                wpfPageLoader.Close();
                wpfPageLoader.Dispose();
                SessionState.CurrentLabelsCountMain = 1;
            }
            fieldPalletSize.Text = SessionState.CurrentLabelsCountMain.ToString();
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
                Exception.Catch(this, ref ex, true);
            }
        }

        private void ButtonDtRight_Click(object sender, EventArgs e)
        {
            try
            {
                SessionState.RotateProductDate(ProjectsEnums.Direction.Forward);
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
                SessionState.RotateProductDate(ProjectsEnums.Direction.Back);
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
                int n = SessionState.CurrentLabelsCountMain == 1 ? 9 : 10;
                for (int i = 0; i < n; i++)
                {
                    SessionState.RotatePalletSize(ProjectsEnums.Direction.Forward);
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
            fieldPalletSize.Text = SessionState.CurrentLabelsCountMain.ToString();
        }

        private void ButtonPalletSizeNext_Click(object sender, EventArgs e)
        {
            try
            {
                SessionState.RotatePalletSize(ProjectsEnums.Direction.Forward);
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
                SessionState.RotatePalletSize(ProjectsEnums.Direction.Back);
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
        
        private void SetLabelsCount(int count,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            try
            {
                SessionState.CurrentLabelsCountMain = count;
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
