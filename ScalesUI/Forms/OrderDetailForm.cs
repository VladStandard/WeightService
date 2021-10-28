// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore;
using System;
using System.Windows.Forms;
using WeightCore.Helpers;

namespace ScalesUI.Forms
{
    public partial class OrderDetailForm : Form
    {
        #region Public and private fields and properties

        private readonly SessionStateHelper _sessionState = SessionStateHelper.Instance;
        private readonly DebugHelper _debug = DebugHelper.Instance;
        private readonly ExceptionHelper _exception = ExceptionHelper.Instance;

        #endregion

        #region Constructor and destructor

        public OrderDetailForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Public and private methods

        private void OrderDetailForm_Load(object sender, EventArgs e)
        {
            try
            {
                TopMost = !_debug.IsDebug;
                listBox1.Items.Clear();
                foreach (string prop in _sessionState.CurrentOrder.ToString().Split('\n'))
                {
                    listBox1.Items.Add(prop);
                }
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
        }

        private void btnOrderReset_Click(object sender, EventArgs e)
        {
            try
            {
                _sessionState.CurrentOrder.SetStatus(ProjectsEnums.OrderStatus.Paused);
                _sessionState.CurrentOrder = null;
                DialogResult = DialogResult.Retry;
                Close();
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
        }

        private void btnOrderComplitrd_Click(object sender, EventArgs e)
        {
            try
            {
                _sessionState.CurrentOrder.SetStatus(ProjectsEnums.OrderStatus.Performed);
                _sessionState.CurrentOrder = null;
                DialogResult = DialogResult.Retry;
                Close();
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                _sessionState.CurrentOrder.SetStatus(ProjectsEnums.OrderStatus.InProgress);
                _sessionState.CurrentOrder = null;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
        }

        #endregion
    }
}
