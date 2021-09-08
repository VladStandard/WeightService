// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore.DAL.TableModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WeightCore.Models;

namespace ScalesUI.Forms
{
    public partial class OrderListForm : Form
    {
        #region Private helpers

        private readonly SessionState _ws = SessionState.Instance;
        private List<OrderDirect> _ordList = null;
        private int _numPage = 0;
        private readonly int offset = 9;

        #endregion

        public OrderListForm()
        {
            InitializeComponent();
        }

        private void OrderListForm_Load(object sender, EventArgs e)
        {
            TopMost = !_ws.IsDebug;

            _ordList = OrderDirect.GetOrderList(_ws.CurrentScale);
            if (_ordList.Count < offset)
            {
                btnLeftRoll.Visible = false;
                btnRightRoll.Visible = false;
            }

            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.RowStyles.Clear();

            tableLayoutPanel1.ColumnCount = 3;
            //tableLayoutPanel1.RowCount = 1 + (ordList.Count / tableLayoutPanel1.ColumnCount);
            tableLayoutPanel1.RowCount = 1;

            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 120));

            //tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;

            //int i = 0;
            //foreach (OrderEntity order in ordList)
            //{
            //    tableLayoutPanel1.Size = new System.Drawing.Size(200, tableLayoutPanel1.Size.Height + 100);
            //    if (i % tableLayoutPanel1.ColumnCount == 0)
            //    {
            //        tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
            //        tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120));
            //    }
            GetPage(tableLayoutPanel1, _numPage, offset);
            Refresh();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void AddRow(TableLayoutPanel panel, int i)
        {
            if (i % tableLayoutPanel1.ColumnCount == 0)
            {
                tableLayoutPanel1.RowCount = tableLayoutPanel1.RowCount + 1;
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 120));
            }
        }

        private void NewButton(TableLayoutPanel panel, int offset, int i, OrderDirect order)
        {
            Button btn = new Button
            {
                Font = new Font("Arial", 16, FontStyle.Bold),
                Text = order.ToString(),
                Name = "btn_" + i,
                TabIndex = i + offset,
                Dock = DockStyle.Fill,
                Size = new Size(120, 30),
                Visible = true,
                Parent = tableLayoutPanel1,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(0, 0),
                UseVisualStyleBackColor = true
            };
            //the names are changed!
            btn.Click += delegate
            {
                _ws.CurrentOrder = _ordList[btn.TabIndex];
                _ws.CurrentOrder.LoadTemplate();
                _ws.CurrentPlu = _ws.CurrentOrder.PLU;
                //ws.CurrentPLU.LoadTemplate();
                //_ws.WeightTare = (int)( _ws.CurrentOrder.PLU.GoodsTareWeight * _ws.CurrentPLU.);
                //_ws.WeightReal = 0;
                DialogResult = DialogResult.OK;
                Close();
            };

            panel.Controls.Add(btn, i % tableLayoutPanel1.ColumnCount, i / tableLayoutPanel1.ColumnCount);
        }

        private void DropButtons(TableLayoutPanel panel)
        {
            panel.Controls.Clear();
        }

        private void GetPage(TableLayoutPanel panel, int offset = 0, int rowCount = 10)
        {
            DropButtons(panel);
            int i = 0;

            List<OrderDirect> page = _ordList.GetRange(offset * rowCount,
                ((offset * rowCount + rowCount) < _ordList.Count()) ? (rowCount) : (_ordList.Count() - offset * rowCount));

            if (!page.Any())
            {
                page = _ordList.GetRange(offset * (--rowCount),
                ((offset * rowCount + rowCount) < _ordList.Count()) ? (rowCount) : (_ordList.Count() - offset * rowCount));
            }

            foreach (OrderDirect order in page)
            {
                panel.Size = new Size(200, tableLayoutPanel1.Size.Height + 100);
                AddRow(panel, i);
                NewButton(panel, offset, i, order);
                i++;
            }
        }

        private void btnRightRoll_Click(object sender, EventArgs e)
        {
            if (_numPage < (_ordList.Count() / offset)) _numPage++;
            else _numPage = (_ordList.Count() / offset);
            GetPage(tableLayoutPanel1, _numPage, offset);

        }

        private void btnLeftRoll_Click(object sender, EventArgs e)
        {
            if (_numPage > 0) _numPage--; else _numPage = 0;
            GetPage(tableLayoutPanel1, _numPage, offset);
        }
    }
}
