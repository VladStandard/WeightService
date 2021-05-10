// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using LabelPrint.Models;
using LabelPrint.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
// ReSharper disable CommentTypo
// ReSharper disable StringLiteralTypo

namespace LabelPrint.ViewModels
{
    /// <summary>
    /// Программные настройки.
    /// </summary>
    public class ProgramSettings : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyRaised([CallerMemberName] string caller = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }

        #endregion

        #region Constructor

        public ProgramSettings()
        {
            // Версия ПО.
            ProgramHeader = GetMainFormText(Assembly.GetExecutingAssembly(), Assembly.GetExecutingAssembly().GetName().Version);
            // ПИН-код.
            PinCode = new PinCode();
            // Кадр.
            ProgramFrame = new Frame
            {
                NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden
            };
            // Размеры по-умолчанию.
            DefaultSizes();
        }

        #endregion

        #region Public properties - Sizes

        private double _width;
        /// <summary>
        /// Ширина окна.
        /// </summary>
        public double Width
        {
            get => _width;
            set
            {
                _width = value;
                OnPropertyRaised();
            }
        }

        private double _minWidth;
        /// <summary>
        /// Минимальная ширина окна.
        /// </summary>
        public double MinWidth
        {
            get => _minWidth;
            set
            {
                _minWidth = value;
                OnPropertyRaised();
            }
        }

        private double _maxWidth;
        /// <summary>
        /// Максимальная ширина окна.
        /// </summary>
        public double MaxWidth
        {
            get => _maxWidth;
            set
            {
                _maxWidth = value;
                OnPropertyRaised();
            }
        }

        private double _height;
        /// <summary>
        /// Высота окна.
        /// </summary>
        public double Height
        {
            get => _height;
            set
            {
                _height = value;
                OnPropertyRaised();
            }
        }

        private double _minHeight;
        /// <summary>
        /// Минимальная высота окна.
        /// </summary>
        public double MinHeight
        {
            get => _minHeight;
            set
            {
                _minHeight = value;
                OnPropertyRaised();
            }
        }

        private double _maxHeight;
        /// <summary>
        /// Максимальная высота окна.
        /// </summary>
        public double MaxHeight
        {
            get => _maxHeight;
            set
            {
                _maxHeight = value;
                OnPropertyRaised();
            }
        }

        private double _fontSize;
        /// <summary>
        /// Размер шрифта.
        /// </summary>
        public double FontSize
        {
            get => _fontSize;
            set
            {
                _fontSize = value;
                OnPropertyRaised();
            }
        }

        /// <summary>
        /// Список размеров.
        /// </summary>
        public ObservableCollection<string> ItemsResolution { get; set; } = new WindowResolution().GetItems();

        private int _selectedResolution;
        /// <summary>
        /// Выбранное разрешение.
        /// </summary>
        public int SelectedResolution
        {
            get => _selectedResolution;
            set
            {
                // Задать новые размеры.
                SetSize((EnumWindowResolution)value);
                _selectedResolution = value;
                OnPropertyRaised();
            }
        }

        #endregion

        #region Private fields and properties - Pages

        // Главная страница.
        private PageHome _pageHome;
        // Страница о программе.
        private PageAbout _pageAbout;
        // Страница истории версий.
        private PageChangeLog _pageChangeLog;
        // Страница пин-кода.
        private PagePinCode _pagePinCode;
        // Страница настроек.
        private PageSettings _pageSettings;

        #endregion

        #region Public properties

        private Frame _programFrame;
        /// <summary>
        /// Кадр.
        /// </summary>
        public Frame ProgramFrame
        {
            get => _programFrame;
            set
            {
                _programFrame = value;
                OnPropertyRaised();
            }
        }

        public WpfActivePage _activePage;
        /// <summary>
        /// Активная страница.
        /// </summary>
        public WpfActivePage ActivePage
        {
            get => _activePage;
            set
            {
                switch (value)
                {
                    // Домашняя страница.
                    case WpfActivePage.Home:
                        MenuHeader = "Домашняя страница";
                        if (_pageHome == null)
                            _pageHome = new PageHome();
                        if (ProgramFrame.Content != null)
                        {
                            if (!(ProgramFrame.Content is PageHome))
                                ProgramFrame.Navigate(_pageHome);
                        }
                        else
                            ProgramFrame.Navigate(_pageHome);
                        // Перед загрузкой.
                        _pageHome.BeforeLoaded();
                        break;
                    // Страница истории изменений.
                    case WpfActivePage.ChangeLog:
                        MenuHeader = "История изменений";
                        if (_pageChangeLog == null)
                            _pageChangeLog = new PageChangeLog();
                        if (ProgramFrame.Content != null)
                        {
                            if (!(ProgramFrame.Content is PageChangeLog))
                                ProgramFrame.Navigate(_pageChangeLog);
                        }
                        else
                            ProgramFrame.Navigate(_pageChangeLog);
                        // Перед загрузкой.
                        _pageChangeLog.BeforeLoaded();
                        break;
                    // Страница о программе.
                    case WpfActivePage.About:
                        MenuHeader = "О программе";
                        if (_pageAbout == null)
                            _pageAbout = new PageAbout();
                        if (ProgramFrame.Content != null)
                        {
                            if (!(ProgramFrame.Content is PageAbout))
                                ProgramFrame.Navigate(_pageAbout);
                        }
                        else
                            ProgramFrame.Navigate(_pageAbout);
                        // Перед загрузкой.
                        _pageAbout.BeforeLoaded();
                        break;
                    case WpfActivePage.PinCode:
                        MenuHeader = "ПИН-код";
                        if (_pagePinCode == null)
                            _pagePinCode = new PagePinCode();
                        if (ProgramFrame.Content != null)
                        {
                            if (!(ProgramFrame.Content is PagePinCode))
                                ProgramFrame.Navigate(_pagePinCode);
                        }
                        else
                            ProgramFrame.Navigate(_pagePinCode);
                        // Перед загрузкой.
                        _pagePinCode.BeforeLoaded();
                        break;
                    // Страница настроек и пин-кода.
                    case WpfActivePage.Settings:
                        MenuHeader = "Настройки";
                        if (_pageSettings == null)
                            _pageSettings = new PageSettings();
                        if (ProgramFrame.Content != null)
                        {
                            if (!(ProgramFrame.Content is PageSettings))
                                ProgramFrame.Navigate(_pageSettings);
                        }
                        else
                            ProgramFrame.Navigate(_pageSettings);
                        // Перед загрузкой.
                        _pageSettings.BeforeLoaded();
                        break;
                }
                _activePage = value;
                OnPropertyRaised();
            }
        }

        private string _menuHeader;
        /// <summary>
        /// Заголовок меню.
        /// </summary>
        public string MenuHeader
        {
            get => _menuHeader;
            set
            {
                _menuHeader = value;
                OnPropertyRaised();
            }
        }

        private string _programHeader;
        /// <summary>
        /// Заголовок программы.
        /// </summary>
        public string ProgramHeader
        {
            get => _programHeader;
            set
            {
                _programHeader = value;
                OnPropertyRaised();
            }
        }

        private PinCode _pinCode;
        /// <summary>
        /// Заголовок программы.
        /// </summary>
        public PinCode PinCode
        {
            get => _pinCode;
            set
            {
                _pinCode = value;
                OnPropertyRaised();
            }
        }

        #endregion

        #region Public methods

        private string GetMainFormText(Assembly assembly, System.Version version)
        {
            return $@"{GetDescription(assembly)} {GetCurrentVersion(EnumVerCountDigits.Use3, null, version)}";
        }

        private string GetDescription(Assembly assembly)
        {
            var result = string.Empty;
            var attributes = assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                var descriptionAttribute = (AssemblyDescriptionAttribute)attributes[0];
                result = descriptionAttribute.Description;
            }
            return result;
        }

        public string GetCurrentVersion(EnumVerCountDigits countDigits, List<EnumStringFormat> stringFormats = null, Version version = null)
        {
            if (version == null)
                version = Assembly.GetExecutingAssembly().GetName().Version;
            string version1;
            string version2;
            string version3;
            string version4;
            if (stringFormats == null || stringFormats.Count == 0)
                stringFormats = new List<EnumStringFormat>() { EnumStringFormat.Use1, EnumStringFormat.Use2, EnumStringFormat.Use2 };

            var formatMajor = stringFormats[0];
            var formatMinor = EnumStringFormat.AsString;
            var formatBuild = EnumStringFormat.AsString;
            var formatRevision = EnumStringFormat.AsString;
            if (stringFormats.Count > 1)
                formatMinor = stringFormats[1];
            if (stringFormats.Count > 2)
                formatBuild = stringFormats[2];
            if (stringFormats.Count > 3)
                formatRevision = stringFormats[3];

            var major = GetCurrentVersionFormat(version.Major, formatMajor);
            var minor = GetCurrentVersionFormat(version.Minor, formatMinor);
            var build = GetCurrentVersionFormat(version.Build, formatBuild);
            var revision = GetCurrentVersionFormat(version.Revision, formatRevision);
            version4 = $"{major}.{minor}.{build}.{revision}";
            version3 = $"{major}.{minor}.{build}";
            version2 = $"{major}.{minor}";
            version1 = $"{major}";

            return countDigits == EnumVerCountDigits.Use1
                ? version1 : countDigits == EnumVerCountDigits.Use2
                ? version2 : countDigits == EnumVerCountDigits.Use3
                ? version3 : version4;
        }

        /// <summary>
        /// Форматировання подстрока текущей версии.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public string GetCurrentVersionFormat(int input, EnumStringFormat format)
        {
            switch (format)
            {
                case EnumStringFormat.Use1:
                    return $"{input:D1}";
                case EnumStringFormat.Use2:
                    return $"{input:D2}";
                case EnumStringFormat.Use3:
                    return $"{input:D3}";
                case EnumStringFormat.Use4:
                    return $"{input:D4}";
            }
            return $"{input:D}";
        }

        /// <summary>
        /// Админ-права.
        /// </summary>
        public bool IsAdmin =>
            System.Net.Dns.GetHostName().Equals("DEV-MAIN") ||  // Домашний ПК Морозов Д.В.
            System.Net.Dns.GetHostName().Equals("PC208") ||     // Рабочий ПК Морозов Д.В.
            System.Net.Dns.GetHostName().Equals("PC0147");      // Рабочий ПК Ивакин Д.В.

        /// <summary>
        /// Размеры по-умолчанию.
        /// </summary>
        public void DefaultSizes()
        {
            // Выбранное разрешение.
            SelectedResolution = IsAdmin ? (int)EnumWindowResolution.Res_1024x768 : (int)EnumWindowResolution.Default;
            // Размер шрифта.
            FontSize = 20;
        }

        /// <summary>
        /// Задать размеры.
        /// </summary>
        /// <param name="resolution"></param>
        public void SetSize(EnumWindowResolution resolution)
        {
            if (Application.Current.MainWindow != null)
            {
                Application.Current.MainWindow.Visibility = Visibility.Hidden;
                switch (resolution)
                {
                    case EnumWindowResolution.Res_800x600:
                        // Ширина.
                        MaxWidth = MinWidth = Width = 800;
                        // Высота.
                        MaxHeight = MinHeight = Height = 600;
                        break;
                    case EnumWindowResolution.Res_1024x768:
                        // Ширина.
                        MaxWidth = MinWidth = Width = 1024;
                        // Высота.
                        MaxHeight = MinHeight = Height = 768;
                        break;
                    case EnumWindowResolution.Res_1366х768:
                        // Ширина.
                        MaxWidth = MinWidth = Width = 1366;
                        // Высота.
                        MaxHeight = MinHeight = Height = 768;
                        break;
                    case EnumWindowResolution.Res_1920х1080:
                        // Ширина.
                        MaxWidth = MinWidth = Width = 1920;
                        // Высота.
                        MaxHeight = MinHeight = Height = 1080;
                        break;
                    case EnumWindowResolution.Default:
                        // Ширина.
                        MaxWidth = MinWidth = Width = SystemParameters.PrimaryScreenWidth;
                        // Высота.
                        MaxHeight = MinHeight = Height = SystemParameters.PrimaryScreenHeight;
                        break;
                }

                Application.Current.MainWindow.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Привязка данных.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Binding GetBinding(string path)
        {
            return new Binding(path + "Property")
            {
                Source = this,
                Mode = BindingMode.OneWay,
                Path = new PropertyPath(path),
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
            };
        }

        /// <summary>
        /// Безопасное обновление DataGrid.
        /// </summary>
        /// <param name="dataGrid"></param>
        public static void RefreshSafely(DataGrid dataGrid)
        {
            ((IEditableCollectionView)dataGrid.Items).CommitEdit();
            dataGrid.Items.Refresh();
        }

        #endregion
    }
}

