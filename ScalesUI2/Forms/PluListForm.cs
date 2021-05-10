// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using ScalesUI.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WeightServices.Common;
using WeightServices.Entities;

namespace ScalesUI.Forms
{
    public partial class PluListForm : Form
    {
        #region Private fields and properties

        private readonly SessionState _ws = SessionState.Instance;
        private List<PluEntity> ordList = null;
        //private readonly int offset = 12;

        private int CurrentPage { get; set; } = 0;
        private readonly int RowCount = 5;
        private readonly int ColumnCount = 4;
        private readonly int PageSize = 20;
        private List<PluEntity> PluList;

        #endregion

        #region Form methods

        public PluListForm()
        {
            InitializeComponent();
            GridCustomizatorClass.GridCustomizator(PluListGrid, ColumnCount, RowCount);
            PluList = PluEntity.GetPLUList(_ws.CurrentScale);
            //WindowState = FormWindowState.Maximized;

        }

        private void PluListForm_Load(object sender, EventArgs e)
        {
            TopMost = !_ws.IsDebug;
            this.Width = Owner.Width;
            this.Height = Owner.Height;
            this.Left   = Owner.Left;
            this.Top = Owner.Top;
            this.StartPosition = FormStartPosition.CenterScreen;

            ordList = PluEntity.GetPLUList(_ws.CurrentScale);

            PluEntity[] pluEntities = PluList.Skip(CurrentPage * PageSize).Take(PageSize).ToArray();
            Control[,] controls = ControlBuilder(pluEntities, this.ColumnCount, this.RowCount);
            GridCustomizatorClass.PageBuilder(this.PluListGrid, controls);

            lbCurrentPage.Text = $@"Cтр. {CurrentPage}";
        }

        private Control[,] ControlBuilder(PluEntity[] pluEntities, int _X, int _Y)
        {
            Control[,] Controls = new Control[_X, _Y];
            for (int j = 0, k = 0; j < _Y; ++j)
                for (int i = 0; i < _X; ++i)
                {
                    if (k >= pluEntities.Length) break;
                    //Control btn = NewButton(pluEntities[k], CurrentPage, k);
                    Control btn = NewControl(pluEntities[k], CurrentPage, k);
                    Controls[i, j] = btn;
                    k++;

                }
            return Controls;
        }

        private Button NewButton(PluEntity plu, int pageNumber, int i)
        {
            Button btn = new Button
            {
                Font = new Font("Arial", 18, FontStyle.Bold),
                Text = plu.ToString(),
                Name = "btn_" + i,
                TabIndex = i + pageNumber* PageSize,
                Dock = DockStyle.Fill,
                Size = new Size(150, 30),
                Visible = true,
                Parent = PluListGrid,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(0, 0),
                UseVisualStyleBackColor = true,
                BackColor = SystemColors.Control
            };
            btn.Click += NewButton_Click;
            return btn;
        }

        private Control NewControl(PluEntity plu, int pageNumber, int i)
        {
            var button = new Button()
            {
                Font = new Font("Arial", 18, FontStyle.Bold),
                Text = plu.GoodsName,
                Name = "btn_" + i,
                TabIndex = i + pageNumber* PageSize,
                Dock = DockStyle.Fill,
                Size = new Size(150, 30),
                Visible = true,
                Parent = PluListGrid,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(0, 0),
                UseVisualStyleBackColor = true,
                BackColor = SystemColors.Control
            };
            button.Click += NewButton_Click;

            var mashtabW = 0.12M;
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
                Top = 3, // button.Height - (int)(PluListGrid.Height * mashtabH) - 3,
                //BackColor = Color.YellowGreen,
                BackColor = ((plu .CheckWeight == false) ? Color.FromArgb(255, 255, 92,  92) : Color.FromArgb(255, 92, 255, 92)),
                BorderStyle = BorderStyle.FixedSingle,
            };
            label.MouseClick += (sender, args) =>
            {
                NewButton_Click(sender, null);
            };

            return button;
        }



        #endregion

        #region Private methods

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
                if (ordList?.Count >= button.TabIndex)
                {
                    _ws.CurrentPLU = ordList[button.TabIndex];
                    _ws.CurrentPLU.LoadTemplate();
                    //_ws.WeightTare = (int)(_ws.CurrentPLU.GoodsTareWeight * _ws.Calibre);
                    //_ws.WeightReal = 0;
                    DialogResult = DialogResult.OK;
                }
                Close();
            }
        }



        private void btnLeftRoll_Click(object sender, EventArgs e)
        {
            if (CurrentPage > 0) CurrentPage--; else CurrentPage = 0;

            PluEntity[] pluEntities = PluList.Skip(CurrentPage * PageSize).Take(PageSize).ToArray();
            Control[,] controls = ControlBuilder(pluEntities, this.ColumnCount, this.RowCount);
            GridCustomizatorClass.PageBuilder(this.PluListGrid, controls);

            lbCurrentPage.Text = $"Cтр. {CurrentPage}";
        }

        private void btnRightRoll_Click(object sender, EventArgs e)
        {
            int countPage = (PluList.Count() / PageSize);

            if (CurrentPage < countPage) CurrentPage++;
            else CurrentPage = countPage;

            PluEntity[] pluEntities = PluList.Skip(CurrentPage * PageSize).Take(PageSize).ToArray();
            Control[,] controls = ControlBuilder(pluEntities, this.ColumnCount, this.RowCount);
            GridCustomizatorClass.PageBuilder(this.PluListGrid, controls);

            lbCurrentPage.Text = $"Cтр. {CurrentPage}";
        }

        #endregion
    }
}
