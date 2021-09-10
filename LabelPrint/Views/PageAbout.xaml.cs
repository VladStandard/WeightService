// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using LabelPrint.Utils;
using LabelPrint.ViewModels;
using System.Windows;
using System.Windows.Controls;
// ReSharper disable CommentTypo

namespace LabelPrint.Views
{
    /// <summary>
    /// Страница о программе.
    /// </summary>
    public partial class PageAbout : Page
    {
        #region Private fields and properties

        // Программные настройки.
        private ProgramSettings _settings;

        #endregion

        #region Constructor

        public PageAbout()
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
            // Получить программные настройки.
            _settings = WpfUtils.GetSettings(this);
            if (_settings != null)
            {
                // Версия ПО.
                //LabelVersion.Content = _settings.AppHelp.GetMainFormText(Assembly.GetExecutingAssembly(), true, Assembly.GetExecutingAssembly().GetName().Version, false);
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// После загрузки страницы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // 
        }

        #endregion
    }
}
