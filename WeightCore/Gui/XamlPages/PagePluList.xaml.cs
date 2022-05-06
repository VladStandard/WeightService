// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableDirectModels;
using DataCore.Sql;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using WeightCore.Helpers;

namespace WeightCore.Gui.XamlPages
{
    /// <summary>
    /// Interaction logic for PagePluList.xaml
    /// </summary>
    public partial class PagePluList : System.Windows.Controls.UserControl
    {
        #region Private fields and properties

        public UserSessionHelper UserSession { get; private set; } = UserSessionHelper.Instance;
        public SqlViewModelEntity SqlViewModel { get; set; }
        private readonly List<PluDirect> _pluList;
        public int RowCount { get; } = 5;
        public int ColumnCount { get; } = 4;
        public int PageSize { get; } = 20;
        public DialogResult Result { get; private set; }
        public RoutedEventHandler OnClose { get; set; }

        #endregion

        #region Constructor and destructor

        public PagePluList()
        {
            InitializeComponent();

            //GridCustomizatorClass.GridCustomizator(PluListGrid, ColumnCount, RowCount);
            _pluList = new PluDirect().GetPluList(UserSession.Scale);
            
            object context = FindResource("SqlViewModel");
            if (context is SqlViewModelEntity sqlViewModel)
            {
                sqlViewModel = SqlViewModel;
            }
            SqlViewModel = UserSession.SqlViewModel;
        }

        #endregion

        #region Public and private methods

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //_orderList = PluEntity.GetPluList(_sessionState.CurrentScale);

            //var pluEntities = _pluList.Skip(_sessionState.CurrentPage * PageSize).Take(PageSize).ToArray();
            //var controls = CreateControls(pluEntities, ColumnCount, RowCount);
            //GridCustomizatorClass.PageBuilder(PluListGrid, controls);
        }

        //private Control[,] CreateControls(IReadOnlyList<PluEntity> pluEntities, int x, int y)
        //{
        //    var controls = new Control[x, y];
        //    for (int j = 0, k = 0; j < y; ++j)
        //    {
        //        for (var i = 0; i < x; ++i)
        //        {
        //            if (k >= pluEntities.Count) break;
        //            var control = CreateNewControl(pluEntities[k], _sessionState.CurrentPage, k);
        //            controls[i, j] = control;
        //            k++;
        //        }
        //    }
        //    return controls;
        //}

        //private Control CreateNewControl(PluEntity plu, int pageNumber, int i)
        //{
        //    var buttonWidth = 150;
        //    var buttonHeight = 30;

        //    var button = new Button()
        //    {
        //        //Font = new Font("Arial", 18, FontStyle.Bold),
        //        //Text = Environment.NewLine + plu.GoodsName,
        //        Name = "btn_" + i,
        //        //Dock = DockStyle.Fill,
        //        //Size = new Size(buttonWidth, buttonHeight),
        //        //Visible = true,
        //        Parent = PluListGrid,
        //        //FlatStyle = FlatStyle.Flat,
        //        //Location = new Point(0, 0),
        //        //UseVisualStyleBackColor = true,
        //        //BackColor = SystemColors.Control,
        //        TabIndex = i + pageNumber * PageSize,
        //    };
        //    button.Click += ButtonPlu_Click;

        //    // PLU number label.
        //    var mashtabW = 0.11M;
        //    var mashtabH = 0.05M;
        //    var label = new Label()
        //    {
        //        Font = new Font("Arial", 20, FontStyle.Bold),
        //        Text = plu.PLU.ToString(),
        //        TextAlign = ContentAlignment.MiddleCenter,
        //        Parent = button,
        //        Size = new Size((int)(PluListGrid.Height * mashtabW), (int)(PluListGrid.Height * mashtabH)),
        //        Dock = DockStyle.None,
        //        Left = 3,
        //        Top = 3,
        //        BackColor = plu.CheckWeight == false
        //            ? Color.FromArgb(255, 255, 92, 92)
        //            : Color.FromArgb(255, 92, 255, 92),
        //        BorderStyle = BorderStyle.FixedSingle,
        //        TabIndex = i + pageNumber * PageSize,
        //    };
        //    label.MouseClick += (sender, args) =>
        //    {
        //        ButtonPlu_Click(label, null);
        //    };

        //    // Weight label.
        //    var labelCount = new Label()
        //    {
        //        Font = new Font("Arial", 20, FontStyle.Bold),
        //        Text = plu.CheckWeight == false ? @"шт" : @"вес",
        //        TextAlign = ContentAlignment.MiddleCenter,
        //        Parent = button,
        //        Size = new Size((int)(PluListGrid.Height * mashtabW), (int)(PluListGrid.Height * mashtabH)),
        //        Dock = DockStyle.None,
        //        Left = label.Width + 15,
        //        Top = 3,
        //        BackColor = plu.CheckWeight == false
        //            ? Color.FromArgb(255, 255, 92, 92)
        //            : Color.FromArgb(255, 92, 255, 92),
        //        BorderStyle = BorderStyle.FixedSingle,
        //        TabIndex = i + pageNumber * PageSize,
        //    };
        //    labelCount.MouseClick += (sender, args) =>
        //    {
        //        ButtonPlu_Click(labelCount, null);
        //    };

        //    return button;
        //}

        public void ButtonClose_OnClick(object sender, RoutedEventArgs e)
        {
            Result = DialogResult.Cancel;
            OnClose?.Invoke(sender, e);
        }

        //private void ButtonPlu_Click(object sender, EventArgs e)
        //{
        //    _sessionState.CurrentOrder = null;
        //    var tabIndex = 0;
        //    if (sender is Control control)
        //        tabIndex = control.TabIndex;
        //    if (_orderList?.Count >= tabIndex)
        //    {
        //        _sessionState.CurrentPlu = _orderList[tabIndex];
        //        _sessionState.CurrentPlu.LoadTemplate();
        //        //_sessionState.WeightTare = (int)(_sessionState.CurrentPLU.GoodsTareWeight * _sessionState.Calibre);
        //        //_sessionState.WeightReal = 0;
        //        DialogResult = DialogResult.OK;
        //    }
        //    Close();
        //}

        //private void ButtonLeftRoll_Click(object sender, EventArgs e)
        //{
        //    var saveCurrentPage = CurrentPage;
        //    CurrentPage = CurrentPage > 0 ? CurrentPage - 1 : 0;
        //    if (CurrentPage == saveCurrentPage)
        //        return;

        //    var pluEntities = _pluList.Skip(CurrentPage * PageSize).Take(PageSize).ToArray();
        //    var controls = CreateControls(pluEntities, ColumnCount, RowCount);
        //    GridCustomizatorClass.PageBuilder(PluListGrid, controls);

        //    labelCurrentPage.Text = $@"Cтр. {CurrentPage}";
        //}

        //private void ButtonRightRoll_Click(object sender, EventArgs e)
        //{
        //    var saveCurrentPage = _sessionState.CurrentPage;
        //    var countPage = _pluList.Count / PageSize;
        //    _sessionState.CurrentPage = _sessionState.CurrentPage < countPage ? _sessionState.CurrentPage + 1 : countPage;
        //    if (_sessionState.CurrentPage == saveCurrentPage)
        //        return;

        //    var pluEntities = _pluList.Skip(_sessionState.CurrentPage * PageSize).Take(PageSize).ToArray();
        //    var controls = CreateControls(pluEntities, ColumnCount, RowCount);
        //    //GridCustomizatorClass.PageBuilder(PluListGrid, controls);

        //    //_sessionState.CurrentPage.Text = $@"Cтр. {CurrentPage}";
        //}

        #endregion
    }
}
