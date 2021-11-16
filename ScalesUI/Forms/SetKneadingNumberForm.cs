// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataProjectsCore;
using System;
using System.Windows.Forms;
using WeightCore.Gui;
using WeightCore.Helpers;

namespace ScalesUI.Forms
{
    public partial class SetKneadingNumberForm : Form
    {
        #region Private fields and properties

        private readonly DebugHelper _debug = DebugHelper.Instance;
        private readonly ExceptionHelper _exception = ExceptionHelper.Instance;
        private readonly SessionStateHelper _sessionState = SessionStateHelper.Instance;
        private readonly int _saveKneading;
        private readonly int _savePalletSize;
        private readonly DateTime _saveProductDate;

        #endregion

        #region Constructor and destructor

        public SetKneadingNumberForm()
        {
            InitializeComponent();

            _saveKneading = _sessionState.Kneading;
            _saveProductDate = _sessionState.ProductDate;
            _savePalletSize = _sessionState.LabelsCount;
        }

        #endregion

        #region Public and private methods

        private void SetKneadingNumberForm_Load(object sender, EventArgs e)
        {
            try
            {
                TopMost = !_debug.IsDebug;
                Width = Owner.Width;
                Height = Owner.Height;
                Left = Owner.Left;
                Top = Owner.Top;
                StartPosition = FormStartPosition.CenterParent;
                ShowPalletSize();
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
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
                fieldProdDate.Text = _sessionState.ProductDate.ToString("dd.MM.yyyy");
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
        }

        private void ShowKneading()
        {
            try
            {
                fieldKneading.Text = _sessionState.Kneading.ToString();
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
        }

        private void ButtonKneadingLeft_Click(object sender, EventArgs e)
        {
            try
            {
                NumberInputForm numberInputForm = new();
                numberInputForm.InputValue = 0;// _sessionState.Kneading;
                if (numberInputForm.ShowDialog() == DialogResult.OK)
                {
                    _sessionState.Kneading = numberInputForm.InputValue;
                }
                ShowKneading();
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
        }

        private void SetKneadingNumberForm_Shown(object sender, EventArgs e)
        {
            try
            {
                ShowKneading();
                ShowProductDate();
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            try
            {
                CheckWeightCount();
                DialogResult = DialogResult.Cancel;
                _sessionState.Kneading = _saveKneading;
                _sessionState.ProductDate = _saveProductDate;
                _sessionState.LabelsCount = _savePalletSize;
                Close();
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
        }

        private void CheckWeightCount()
        {
            if (_sessionState.CurrentPlu == null)
                return;

            if (_sessionState.CurrentPlu.CheckWeight == true && _sessionState.LabelsCount > 1)
            {
                CustomMessageBox messageBox = CustomMessageBox.Show(this, LocalizationData.ScalesUI.CheckPluWeightCount,
                    LocalizationData.ScalesUI.OperationControl, MessageBoxButtons.OK, MessageBoxIcon.Error);
                messageBox.Wait();
                _sessionState.LabelsCount = 1;
            }
            fieldPalletSize.Text = _sessionState.LabelsCount.ToString();
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
                _exception.Catch(this, ref ex);
            }
        }

        private void ButtonDtRight_Click(object sender, EventArgs e)
        {
            try
            {
                _sessionState.RotateProductDate(ProjectsEnums.Direction.Forward);
                ShowProductDate();
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
        }

        private void ButtonDtLeft_Click(object sender, EventArgs e)
        {
            try
            {
                _sessionState.RotateProductDate(ProjectsEnums.Direction.Back);
                ShowProductDate();
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
        }

        private void ButtonPalletSize10_Click(object sender, EventArgs e)
        {
            try
            {
                int n = _sessionState.LabelsCount == 1 ? 9 : 10;
                for (int i = 0; i < n; i++)
                {
                    _sessionState.RotatePalletSize(ProjectsEnums.Direction.Forward);
                    ShowPalletSize();
                }
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
        }

        private void ShowPalletSize()
        {
            fieldPalletSize.Text = _sessionState.LabelsCount.ToString();
        }

        private void ButtonPalletSizeNext_Click(object sender, EventArgs e)
        {
            try
            {
                _sessionState.RotatePalletSize(ProjectsEnums.Direction.Forward);
                ShowPalletSize();
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
        }

        private void ButtonPalletSizePrev_Click(object sender, EventArgs e)
        {
            try
            {
                _sessionState.RotatePalletSize(ProjectsEnums.Direction.Back);
                ShowPalletSize();
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
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
        
        private void SetLabelsCount(int count)
        {
            try
            {
                _sessionState.LabelsCount = count;
                ShowPalletSize();
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
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
                _exception.Catch(this, ref ex);
            }
        }

        #endregion
    }
}
