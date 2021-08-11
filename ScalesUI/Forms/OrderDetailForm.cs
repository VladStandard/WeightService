// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using ScalesUI.Common;
using System;
using System.Windows.Forms;
using WeightCore.DAL;
using WeightCore.Utils;

namespace ScalesUI.Forms
{
    public partial class OrderDetailForm : Form
    {
        private readonly SessionState _ws = SessionState.Instance;

        public OrderDetailForm()
        {
            InitializeComponent();
        }

        private void OrderDetailForm_Load(object sender, EventArgs e)
        {
            TopMost = !_ws.IsDebug;
            listBox1.Items.Clear();
            foreach (string prop in _ws.CurrentOrder.ToString().Split('\n'))
            {
                listBox1.Items.Add(prop);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnOrderReset_Click(object sender, EventArgs e)
        {
            // приостановить
            _ws.CurrentOrder.SetStatus(OrderStatus.Paused);
            _ws.CurrentOrder = null;
            DialogResult = DialogResult.Retry;
            Close();
        }

        private void btnOrderComplitrd_Click(object sender, EventArgs e)
        {
            // выполнен
            _ws.CurrentOrder.SetStatus(OrderStatus.Performed);
            _ws.CurrentOrder = null;
            DialogResult = DialogResult.Retry;
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            _ws.CurrentOrder.SetStatus(OrderStatus.InProgress);
            _ws.CurrentOrder = null;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
