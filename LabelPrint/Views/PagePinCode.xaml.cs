// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using LabelPrint.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using LabelPrint.Utils;
using DataCore;
// ReSharper disable CommentTypo

namespace LabelPrint.Views
{
    /// <summary>
    /// Interaction logic for PagePin.xaml
    /// </summary>
    public partial class PagePinCode : Page
    {
        #region Constructor

        public PagePinCode()
        {
            InitializeComponent();
        }

        #endregion

        #region Private fields and properties

        // Программные настройки.
        private ProgramSettings _settings;

        #endregion

        #region Public fields and properties

        //

        #endregion

        #region Private methods

        /// <summary>
        /// После загрузки страницы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PagePin_OnLoaded(object sender, RoutedEventArgs e)
        {
            // Очистить.
            ButtonClear_Click(sender, e);
        }

        /// <summary>
        /// Клик по цифре.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonNum_Click(object sender, EventArgs e)
        {
            string num = (string)(sender as Button).Content;
            _settings.PinCode.Input = int.Parse(_settings.PinCode.Input.ToString() + num);
        }

        /// <summary>
        /// Очистить.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            _settings.PinCode.Input = 0;
        }

        /// <summary>
        /// Ввод.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEnter_Click(object sender, RoutedEventArgs e)
        {
            if (_settings.PinCode.AccessGranted)
            {
                _settings.ActivePage = ProjectsEnums.WpfActivePage.Settings;
            }
            else
            {
                ButtonClear_Click(sender, e);
            }
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
    }
}
