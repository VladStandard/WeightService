// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using ScalesUI.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using EntitiesLib;
using UICommon;

namespace ScalesUI.Forms
{
    public partial class PluListForm : Form
    {
        #region Private fields and properties

        private readonly SessionState _ws = SessionState.Instance;
        private List<PluEntity> _orderList;
        private readonly List<PluEntity> _pluList;
        private readonly int _rowCount = 5;
        private readonly int _columnCount = 4;
        private readonly int _pageSize = 20;
        private int _currentPage = 0;

        #endregion

        #region Public and private methods

        public PluListForm()
        {
            InitializeComponent();
            GridCustomizatorClass.GridCustomizator(PluListGrid, _columnCount, _rowCount);
            _pluList = PluEntity.GetPLUList(_ws.CurrentScale);
            //WindowState = FormWindowState.Maximized;
        }

        private void PluListForm_Load(object sender, EventArgs e)
        {
            TopMost = !_ws.IsDebug;
            Width = Owner.Width;
            Height = Owner.Height;
            Left   = Owner.Left;
            Top = Owner.Top;
            StartPosition = FormStartPosition.CenterScreen;

            _orderList = PluEntity.GetPLUList(_ws.CurrentScale);

            PluEntity[] pluEntities = _pluList.Skip(_currentPage * _pageSize).Take(_pageSize).ToArray();
            Control[,] controls = CreateControls(pluEntities, _columnCount, _rowCount);
            GridCustomizatorClass.PageBuilder(PluListGrid, controls);

            lbCurrentPage.Text = $@"Cтр. {_currentPage}";
        }

        private Control[,] CreateControls(IReadOnlyList<PluEntity> pluEntities, int x, int y)
        {
            var controls = new Control[x, y];
            for (int j = 0, k = 0; j < y; ++j)
            {
                for (var i = 0; i < x; ++i)
                {
                    if (k >= pluEntities.Count) break;
                    var control = NewControl(pluEntities[k], _currentPage, k);
                    controls[i, j] = control;
                    k++;
                }
            }
            return controls;
        }

        private Control NewControl(PluEntity plu, int pageNumber, int i)
        {
            var buttonWidth = 150;
            var buttonHeight = 30;

            var button = new Button()
            {
                Font = new Font("Arial", 18, FontStyle.Bold),
                Text = Environment.NewLine + plu.GoodsName,
                Name = "btn_" + i,
                TabIndex = i + pageNumber* _pageSize,
                Dock = DockStyle.Fill,
                Size = new Size(buttonWidth, buttonHeight),
                Visible = true,
                Parent = PluListGrid,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(0, 0),
                UseVisualStyleBackColor = true,
                BackColor = SystemColors.Control
            };
            button.Click += NewButton_Click;

            // PLU number label.
            var mashtabW = 0.11M;
            var mashtabH = 0.05M;
            var label = new Label()
            {
                Font = new Font("Arial", 20, FontStyle.Bold),
                Text = plu.PLU.ToString(),
                TextAlign = ContentAlignment.MiddleCenter,
                Parent = button,
                Size = new Size((int)(PluListGrid.Height * mashtabW), (int) (PluListGrid.Height * mashtabH)),
                Dock = DockStyle.None,
                Left = 3,
                Top = 3,
                BackColor = plu.CheckWeight == false 
                    ? Color.FromArgb(255, 255, 92,  92) 
                    : Color.FromArgb(255, 92, 255, 92),
                BorderStyle = BorderStyle.FixedSingle,
            };
            label.MouseClick += (sender, args) =>
            {
                NewButton_Click(button, null);
            };

            // Weight label.
            var labelCount = new Label()
            {
                Font = new Font("Arial", 20, FontStyle.Bold),
                Text = plu.CheckWeight == false ? @"шт" : @"вес",
                TextAlign = ContentAlignment.MiddleCenter,
                Parent = button,
                Size = new Size((int) (PluListGrid.Height * mashtabW), (int) (PluListGrid.Height * mashtabH)),
                Dock = DockStyle.None,
                Left = label.Width + 15,
                Top = 3,
                BackColor = plu.CheckWeight == false
                    ? Color.FromArgb(255, 255, 92, 92)
                    : Color.FromArgb(255, 92, 255, 92),
                BorderStyle = BorderStyle.FixedSingle,
            };
            labelCount.MouseClick += (sender, args) =>
            {
                NewButton_Click(button, null);
            };

            return button;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                _ws.CurrentOrder = null;
                if (_orderList?.Count >= button.TabIndex)
                {
                    _ws.CurrentPlu = _orderList[button.TabIndex];
                    _ws.CurrentPlu.LoadTemplate();
                    //_ws.WeightTare = (int)(_ws.CurrentPLU.GoodsTareWeight * _ws.Calibre);
                    //_ws.WeightReal = 0;
                    DialogResult = DialogResult.OK;
                }
                Close();
            }
        }

        private void btnLeftRoll_Click(object sender, EventArgs e)
        {
            if (_currentPage > 0) _currentPage--; else _currentPage = 0;

            PluEntity[] pluEntities = _pluList.Skip(_currentPage * _pageSize).Take(_pageSize).ToArray();
            Control[,] controls = CreateControls(pluEntities, _columnCount, _rowCount);
            GridCustomizatorClass.PageBuilder(PluListGrid, controls);

            lbCurrentPage.Text = $"Cтр. {_currentPage}";
        }

        private void btnRightRoll_Click(object sender, EventArgs e)
        {
            int countPage = _pluList.Count / _pageSize;
            if (_currentPage < countPage) _currentPage++;
            else _currentPage = countPage;

            PluEntity[] pluEntities = _pluList.Skip(_currentPage * _pageSize).Take(_pageSize).ToArray();
            Control[,] controls = CreateControls(pluEntities, _columnCount, _rowCount);
            GridCustomizatorClass.PageBuilder(PluListGrid, controls);

            lbCurrentPage.Text = $@"Cтр. {_currentPage}";
        }

        #endregion
    }
}
