// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using System;
using System.Windows.Forms;
using WeightCore.Gui;
using WeightCore.Helpers;

namespace ScalesUI.Forms
{
    public partial class OrderDetailForm : Form
    {
        #region Public and private fields and properties

        private DebugHelper Debug { get; } = DebugHelper.Instance;
        private UserSessionHelper UserSession { get; } = UserSessionHelper.Instance;

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
                if (UserSession.Order != null)
                    foreach (string prop in UserSession.Order.ToString().Split('\n'))
                    {
                        listBox1.Items.Add(prop);
                    }
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
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
                GuiUtils.WpfForm.CatchException(this, ex);
            }
        }

        private void BtnOrderReset_Click(object sender, EventArgs e)
        {
            try
            {
                UserSession.Order?.SetStatus(ProjectsEnums.OrderStatus.Paused);
                UserSession.Order = null;
                DialogResult = DialogResult.Retry;
                Close();
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
        }

        private void BtnOrderComplitrd_Click(object sender, EventArgs e)
        {
            try
            {
                UserSession.Order?.SetStatus(ProjectsEnums.OrderStatus.Performed);
                UserSession.Order = null;
                DialogResult = DialogResult.Retry;
                Close();
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            try
            {
                UserSession.Order?.SetStatus(ProjectsEnums.OrderStatus.InProgress);
                UserSession.Order = null;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
        }

        #endregion
    }
}
