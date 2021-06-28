// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using ScalesUI.Common;

namespace ScalesUI.Forms
{
    public partial class WpfPluList : Form
    {
        #region Public and private fields and properties

        private ElementHost ElementHost { get; set; }
        private PagePluList PluList { get; set; }
        private readonly SessionState _ws = SessionState.Instance;

        #endregion

        #region Constructor and destructor

        public WpfPluList()
        {
            InitializeComponent();
        }

        #endregion

        #region Public and private methods

        private void WpfPluList_Load(object sender, EventArgs e)
        {
            // Own GUI.
            TopMost = !_ws.IsDebug;
            Width = Owner.Width;
            Height = Owner.Height;
            Left = Owner.Left;
            Top = Owner.Top;
            //StartPosition = FormStartPosition.CenterParent;
            // WPF element.
            ElementHost = new ElementHost { Dock = DockStyle.Fill };
            panelMain.Controls.Add(ElementHost);
            PluList = new PagePluList();
            PluList.InitializeComponent();
            ElementHost.Child = PluList;
            PluList.Loaded += PluListOnLoaded;
        }

        private void PluListOnLoaded(object sender, RoutedEventArgs e)
        {
            
        }

        #endregion
    }
}
