// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataBaseCore;
using ScalesUI.Common;
using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace ScalesUI.Forms
{
    public partial class WpfPageLoader : Form
    {
        #region Public and private fields and properties

        private readonly SessionState _ws = SessionState.Instance;
        public bool UseOwnerSize { get; set; }
        public EnumPage Page { get; private set; }
        private ElementHost ElementHost { get; set; }
        private PagePluList PluList { get; set; }
        public PageSqlSettings SqlSettings { get; private set; }

        #endregion

        #region Constructor and destructor

        public WpfPageLoader()
        {
            InitializeComponent();

            Page = EnumPage.Default;
            _ws.IsWpfPageLoaderClose = false;
        }

        public WpfPageLoader(EnumPage page, bool useOwnerSize) : this()
        {
            Page = page;
            UseOwnerSize = useOwnerSize;
        }

        #endregion

        #region Public and private methods

        private void WpfPageLoader_Load(object sender, EventArgs e)
        {
            // Own GUI.
            TopMost = !_ws.IsDebug;

            if (UseOwnerSize)
            {
                Width = Owner.Width;
                Height = Owner.Height;
                Left = Owner.Left;
                Top = Owner.Top;
            }
            else
            {
                Left = Owner.Left + Owner.Width / 2 - Width / 2;
                Top = Owner.Top + Owner.Height / 2 - Height / 2;
            }

            // WPF element.
            if (Page != EnumPage.Default)
            {
                ElementHost = new ElementHost { Dock = DockStyle.Fill };
                panelMain.Controls.Add(ElementHost);
            }
            switch (Page)
            {
                case EnumPage.PluList:
                    PluList = new PagePluList();
                    PluList.InitializeComponent();
                    ElementHost.Child = PluList;
                    PluList.Loaded += PluListOnLoaded;
                    _ws.WpfPageLoader_OnClose += WpfPageLoader_OnClose;
                    break;
                case EnumPage.SqlSettings:
                    SqlSettings = new PageSqlSettings();
                    SqlSettings.InitializeComponent();
                    ElementHost.Child = SqlSettings;
                    SqlSettings.Loaded += SqlSettingsOnLoaded;
                    _ws.WpfPageLoader_OnClose += WpfPageLoader_OnClose;
                    break;
                case EnumPage.Default:
                default:
                    break;
            }
        }

        private void PluListOnLoaded(object sender, RoutedEventArgs e)
        {

        }

        private void SqlSettingsOnLoaded(object sender, RoutedEventArgs e)
        {

        }

        private void WpfPageLoader_OnClose(object sender, RoutedEventArgs e)
        {
            _ws.WpfPageLoader_OnClose -= WpfPageLoader_OnClose;
            Close();
        }

        private void WpfPageLoader_FormClosed(object sender, FormClosedEventArgs e)
        {
            switch (Page)
            {
                case EnumPage.PluList:
                    if (PluList != null)
                    {
                        DialogResult = PluList.Result;
                    }
                    break;
                case EnumPage.SqlSettings:
                    if (SqlSettings != null)
                    {
                        DialogResult = SqlSettings.Result;
                    }
                    break;
                case EnumPage.Default:
                default:
                    DialogResult = DialogResult.Cancel;
                    break;
            }
        }

        #endregion
    }
}
