// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows;
using DataProjectsCore;
using LabelPrint.Utils;
using LabelPrint.ViewModels;

// ReSharper disable CommentTypo

namespace LabelPrint.Views
{
    /// <summary>
    /// Главное окно.
    /// </summary>
    public partial class WindowMain : Window
    {
        #region Private fields and properties

        // Программные настройки.
        private ProgramSettings _settings;

        #endregion

        #region Constructor

        public WindowMain()
        {
            InitializeComponent();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Перед загрузкой окна.
        /// </summary>
        private void BeforeLoaded()
        {
            // Получить программные настройки.
            _settings = WpfUtils.GetSettings(this);
            if (_settings != null)
            {
                // Поверх других окон.
                Topmost = !_settings.IsAdmin;
            }
        }

        /// <summary>
        /// После загрузки окна.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Перед загрузкой.
            BeforeLoaded();
            // По-умолчанию.
            ButtonHome_Click(sender, e);
        }

        /// <summary>
        /// Выход.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Домашняя страница.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonHome_Click(object sender, RoutedEventArgs e)
        {
            _settings.ActivePage = ProjectsEnums.WpfActivePage.Home;
        }

        /// <summary>
        /// Страница настроек.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSettings_OnClick(object sender, RoutedEventArgs e)
        {
            _settings.ActivePage = _settings.IsAdmin ? ProjectsEnums.WpfActivePage.Settings : ProjectsEnums.WpfActivePage.PinCode;
        }

        /// <summary>
        /// Страница истории изменений.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonChangeLog_OnClick(object sender, RoutedEventArgs e)
        {
            _settings.ActivePage = ProjectsEnums.WpfActivePage.ChangeLog;
        }

        /// <summary>
        /// Страница о программе.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAbout_OnClick(object sender, RoutedEventArgs e)
        {
            _settings.ActivePage = ProjectsEnums.WpfActivePage.About;
        }

        /// <summary>
        /// Клик левой кнопкой мыщи.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LabelHader_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

        #endregion
    }
}
