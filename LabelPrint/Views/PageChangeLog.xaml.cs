// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace LabelPrint.Views
{
    /// <summary>
    /// Страница истории изменений.
    /// </summary>
    public partial class PageChangeLog : Page
    {
        #region Constructor

        public PageChangeLog()
        {
            InitializeComponent();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Перед загрузкой страницы.
        /// </summary>
        public void BeforeLoaded()
        {
            string fileName = "CHANGELOG.md";
            if (File.Exists(fileName))
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    TextBlockMain.Text = sr.ReadToEnd();
                }
            }
        }

        #endregion
        
        #region Private methods

        /// <summary>
        /// После загрузки страницы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PageChangeLog_OnLoaded(object sender, RoutedEventArgs e)
        {
            //
        }

        #endregion
    }
}
