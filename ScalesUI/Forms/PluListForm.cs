// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using ScalesUI.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WeightCore.DAL.TableModels;
using WeightCore.Gui;

namespace ScalesUI.Forms
{
    public partial class PluListForm : Form
    {
        #region Private fields and properties

        private readonly SessionState _ws = SessionState.Instance;
        private List<PluEntity> _orderList;
        private readonly List<PluEntity> _pluList;
        public int RowCount { get; } = 5;
        public int ColumnCount { get; } = 4;
        public int PageSize { get; } = 20;
        public int CurrentPage { get; private set; }

        #endregion

        #region Constructor and destructor

        public PluListForm()
        {
            InitializeComponent();
            //GridCustomizatorClass.GridCustomizator(PluListGrid, ColumnCount, RowCount);
            _pluList = PluEntity.GetPluList(_ws.CurrentScale);
        }

        #endregion

        #region Public and private methods

        private void PluListForm_Load(object sender, EventArgs e)
        {
            TopMost = !_ws.IsDebug;
            Width = Owner.Width;
            Height = Owner.Height;
            Left = Owner.Left;
            Top = Owner.Top;
            //StartPosition = FormStartPosition.CenterParent;

            _orderList = PluEntity.GetPluList(_ws.CurrentScale);

            var pluEntities = _pluList.Skip(CurrentPage * PageSize).Take(PageSize).ToArray();
            var controls = CreateControls(pluEntities, ColumnCount, RowCount);
            GridCustomizatorClass.PageBuilder(PluListGrid, controls);

            labelCurrentPage.Text = $@"Cтр. {CurrentPage}";
        }

        private Control[,] CreateControls(IReadOnlyList<PluEntity> pluEntities, int x, int y)
        {
            var controls = new Control[x, y];
            for (int j = 0, k = 0; j < y; ++j)
            {
                for (var i = 0; i < x; ++i)
                {
                    if (k >= pluEntities.Count) break;
                    var control = CreateNewControl(pluEntities[k], CurrentPage, k);
                    controls[i, j] = control;
                    k++;
                }
            }
            return controls;
        }

        private Control CreateNewControl(PluEntity plu, int pageNumber, int i)
        {
            var buttonWidth = 150;
            var buttonHeight = 30;

            var button = new Button()
            {
                Font = new Font("Arial", 18, FontStyle.Bold),
                Text = Environment.NewLine + plu.GoodsName,
                Name = "btn_" + i,
                Dock = DockStyle.Fill,
                Size = new Size(buttonWidth, buttonHeight),
                Visible = true,
                Parent = PluListGrid,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(0, 0),
                UseVisualStyleBackColor = true,
                BackColor = SystemColors.Control,
                TabIndex = i + pageNumber * PageSize,
            };
            button.Click += buttonPlu_Click;

            // PLU number label.
            var mashtabW = 0.19M;
            var mashtabH = 0.05M;
            var label = new Label()
            {
                Font = new Font("Arial", 20, FontStyle.Bold),
                Text = plu.PLU.ToString(),
                TextAlign = ContentAlignment.MiddleCenter,
                Parent = button,
                Size = new Size((int)(PluListGrid.Height * mashtabW), (int)(PluListGrid.Height * mashtabH)),
                Dock = DockStyle.None,
                Left = 3,
                Top = 3,
                BackColor = plu.CheckWeight == false
                    ? Color.FromArgb(255, 255, 92, 92)
                    : Color.FromArgb(255, 92, 255, 92),
                BorderStyle = BorderStyle.FixedSingle,
                TabIndex = i + pageNumber * PageSize,
            };
            label.MouseClick += (sender, args) =>
            {
                buttonPlu_Click(label, null);
            };

            // Weight label.
            mashtabW = 0.11M;
            var labelCount = new Label()
            {
                Font = new Font("Arial", 20, FontStyle.Bold),
                Text = plu.CheckWeight == false ? @"шт" : @"вес",
                TextAlign = ContentAlignment.MiddleCenter,
                Parent = button,
                Size = new Size((int)(PluListGrid.Height * mashtabW), (int)(PluListGrid.Height * mashtabH)),
                Dock = DockStyle.None,
                Left = label.Width + 15,
                Top = 3,
                BackColor = plu.CheckWeight == false
                    ? Color.FromArgb(255, 255, 92, 92)
                    : Color.FromArgb(255, 92, 255, 92),
                BorderStyle = BorderStyle.FixedSingle,
                TabIndex = i + pageNumber * PageSize,
            };
            labelCount.MouseClick += (sender, args) =>
            {
                buttonPlu_Click(labelCount, null);
            };

            return button;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void buttonPlu_Click(object sender, EventArgs e)
        {
            _ws.CurrentOrder = null;
            var tabIndex = 0;
            if (sender is Control control)
                tabIndex = control.TabIndex;
            if (_orderList?.Count >= tabIndex)
            {
                _ws.CurrentPlu = _orderList[tabIndex];
                _ws.CurrentPlu.LoadTemplate();
                //_ws.WeightTare = (int)(_ws.CurrentPLU.GoodsTareWeight * _ws.Calibre);
                //_ws.WeightReal = 0;
                DialogResult = DialogResult.OK;
            }
            Close();
        }

        private void buttonLeftRoll_Click(object sender, EventArgs e)
        {
            var saveCurrentPage = CurrentPage;
            CurrentPage = CurrentPage > 0 ? CurrentPage - 1 : 0;
            if (CurrentPage == saveCurrentPage)
                return;

            var pluEntities = _pluList.Skip(CurrentPage * PageSize).Take(PageSize).ToArray();
            var controls = CreateControls(pluEntities, ColumnCount, RowCount);
            GridCustomizatorClass.PageBuilder(PluListGrid, controls);

            labelCurrentPage.Text = $@"Cтр. {CurrentPage}";
        }

        private void buttonRightRoll_Click(object sender, EventArgs e)
        {
            var saveCurrentPage = CurrentPage;
            var countPage = _pluList.Count / PageSize;
            CurrentPage = CurrentPage < countPage ? CurrentPage + 1 : countPage;
            if (CurrentPage == saveCurrentPage)
                return;

            var pluEntities = _pluList.Skip(CurrentPage * PageSize).Take(PageSize).ToArray();
            var controls = CreateControls(pluEntities, ColumnCount, RowCount);
            GridCustomizatorClass.PageBuilder(PluListGrid, controls);

            labelCurrentPage.Text = $@"Cтр. {CurrentPage}";
        }

        #endregion
    }
}
