// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Localizations;
using DataCore.Sql.TableDirectModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WeightCore.Gui;
using WeightCore.Helpers;

namespace ScalesUI.Forms
{
    public partial class SelectPluForm : Form
    {
        #region Private fields and properties

        private DebugHelper Debug { get; set; } = DebugHelper.Instance;
        private List<PluDirect> OrderList { get; set; }
        private List<PluDirect> PluList { get; set; }
        private UserSessionHelper UserSession { get; set; } = UserSessionHelper.Instance;
        public int ColumnCount { get; } = 4;
        public int CurrentPage { get; private set; }
        public int PageSize { get; } = 20;
        public int RowCount { get; } = 5;

        #endregion

        #region Constructor and destructor

        public SelectPluForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Public and private methods

        private void PluListForm_Load(object sender, EventArgs e)
        {
            try
            {
                PluList = new PluDirect().GetPluList(UserSession.Scale);
                OrderList = new PluDirect().GetPluList(UserSession.Scale);
                PluDirect[] pluEntities = PluList.Skip(CurrentPage * PageSize).Take(PageSize).ToArray();
                Control[,] controls = CreateControls(pluEntities, ColumnCount, RowCount);
                GridCustomizatorClass.PageBuilder(PluListGrid, controls);

                labelCurrentPage.Text = $"{LocaleCore.Scales.PluPage} {CurrentPage}";
                TopMost = !Debug.IsDebug;
                Width = Owner.Width;
                Height = Owner.Height;
                Left = Owner.Left;
                Top = Owner.Top;
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
        }

        private Control[,] CreateControls(IReadOnlyList<PluDirect> pluEntities, int x, int y)
        {
            Control[,] controls = new Control[x, y];
            try
            {
                for (int j = 0, k = 0; j < y; ++j)
                {
                    for (int i = 0; i < x; ++i)
                    {
                        if (k >= pluEntities.Count) break;
                        Control control = CreateNewControl(pluEntities[k], CurrentPage, k);
                        controls[i, j] = control;
                        k++;
                    }
                }
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
            return controls;
        }

        private Control CreateNewControl(PluDirect plu, int pageNumber, int i)
        {
            Button button = null;
            try
            {
                int buttonWidth = 150;
                int buttonHeight = 30;

                button = new()
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
                button.Click += ButtonPlu_Click;

                // PLU number label.
                decimal mashtabW = 0.19M;
                decimal mashtabH = 0.05M;
                Label label = new()
                {
                    Font = new Font("Arial", 20, FontStyle.Bold),
                    Text = plu.PLU.ToString(),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Parent = button,
                    Size = new Size((int)(PluListGrid.Height * mashtabW), (int)(PluListGrid.Height * mashtabH)),
                    Dock = DockStyle.None,
                    Left = 3,
                    Top = 3,
                    BackColor = plu.IsCheckWeight == false
                        ? Color.FromArgb(255, 255, 92, 92)
                        : Color.FromArgb(255, 92, 255, 92),
                    BorderStyle = BorderStyle.FixedSingle,
                    TabIndex = i + pageNumber * PageSize,
                };
                label.MouseClick += (sender, args) =>
                {
                    ButtonPlu_Click(label, null);
                };

                // Weight label.
                mashtabW = 0.11M;
                Label labelCount = new()
                {
                    Font = new Font("Arial", 20, FontStyle.Bold),
                    Text = plu.IsCheckWeight == false ? @"шт" : @"вес",
                    TextAlign = ContentAlignment.MiddleCenter,
                    Parent = button,
                    Size = new Size((int)(PluListGrid.Height * mashtabW), (int)(PluListGrid.Height * mashtabH)),
                    Dock = DockStyle.None,
                    Left = label.Width + 15,
                    Top = 3,
                    BackColor = plu.IsCheckWeight == false
                        ? Color.FromArgb(255, 255, 92, 92)
                        : Color.FromArgb(255, 92, 255, 92),
                    BorderStyle = BorderStyle.FixedSingle,
                    TabIndex = i + pageNumber * PageSize,
                };
                labelCount.MouseClick += (sender, args) =>
                {
                    ButtonPlu_Click(labelCount, null);
                };
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
            return button;
        }

        private void ButtonClose_Click(object sender, EventArgs e)
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

        private void ButtonPlu_Click(object sender, EventArgs e)
        {
            try
            {
                UserSession.Order = null;
                int tabIndex = 0;
                if (sender is Control control)
                    tabIndex = control.TabIndex;
                if (OrderList?.Count >= tabIndex)
                {
                    UserSession.SetCurrentPlu(OrderList[tabIndex]);
                    UserSession.Plu.LoadTemplate();
                    DialogResult = DialogResult.OK;
                }
                Close();
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
        }

        private void ButtonLeftRoll_Click(object sender, EventArgs e)
        {
            try
            {
                int saveCurrentPage = CurrentPage;
                CurrentPage = CurrentPage > 0 ? CurrentPage - 1 : 0;
                if (CurrentPage == saveCurrentPage)
                    return;

                PluDirect[] pluEntities = PluList.Skip(CurrentPage * PageSize).Take(PageSize).ToArray();
                Control[,] controls = CreateControls(pluEntities, ColumnCount, RowCount);
                GridCustomizatorClass.PageBuilder(PluListGrid, controls);

                labelCurrentPage.Text = $@"Cтр. {CurrentPage}";
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
        }

        private void ButtonRightRoll_Click(object sender, EventArgs e)
        {
            try
            {
                int saveCurrentPage = CurrentPage;
                int countPage = PluList.Count / PageSize;
                CurrentPage = CurrentPage < countPage ? CurrentPage + 1 : countPage;
                if (CurrentPage == saveCurrentPage)
                    return;

                PluDirect[] pluEntities = PluList.Skip(CurrentPage * PageSize).Take(PageSize).ToArray();
                Control[,] controls = CreateControls(pluEntities, ColumnCount, RowCount);
                GridCustomizatorClass.PageBuilder(PluListGrid, controls);

                labelCurrentPage.Text = $@"Cтр. {CurrentPage}";
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
        }

        #endregion
    }
}
