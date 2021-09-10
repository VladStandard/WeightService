// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using LabelPrint.Utils;
using LabelPrint.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace LabelPrint.Views
{
    /// <summary>
    /// Главная страница.
    /// </summary>
    public partial class PageHome : Page
    {
        #region Private fields and properties

        // Программные настройки.
        private ProgramSettings _settings;

        #endregion

        #region Constructor

        public PageHome()
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
        }

        #endregion

        #region Private methods

        /// <summary>
        /// После загрузки страницы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PageHome_OnLoaded(object sender, RoutedEventArgs e)
        {
            // 
        }

        /// <summary>
        /// Обновить.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            // 
        }

        /// <summary>
        /// Печать.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonPrint_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Печать завершена.");
        }

        #endregion
    }
}
