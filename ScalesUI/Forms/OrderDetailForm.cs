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

        private SessionStateHelper SessionState { get; set; } = SessionStateHelper.Instance;
        private DebugHelper Debug { get; set; } = DebugHelper.Instance;
        private ExceptionHelper Exception { get; set; } = ExceptionHelper.Instance;

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
                TopMost = !Debug.IsDebug;
                listBox1.Items.Clear();
                foreach (string prop in SessionState.CurrentOrder.ToString().Split('\n'))
                {
                    listBox1.Items.Add(prop);
                }
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true);
            }
        }

        private void BtnOrderReset_Click(object sender, EventArgs e)
        {
            try
            {
                SessionState.CurrentOrder.SetStatus(ProjectsEnums.OrderStatus.Paused);
                SessionState.CurrentOrder = null;
                DialogResult = DialogResult.Retry;
                Close();
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true);
            }
        }

        private void BtnOrderComplitrd_Click(object sender, EventArgs e)
        {
            try
            {
                SessionState.CurrentOrder.SetStatus(ProjectsEnums.OrderStatus.Performed);
                SessionState.CurrentOrder = null;
                DialogResult = DialogResult.Retry;
                Close();
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true);
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            try
            {
                SessionState.CurrentOrder.SetStatus(ProjectsEnums.OrderStatus.InProgress);
                SessionState.CurrentOrder = null;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                Exception.Catch(this, ref ex, true);
            }
        }

        #endregion
    }
}
