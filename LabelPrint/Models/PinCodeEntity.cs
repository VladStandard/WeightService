// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using MvvmHelpers;
using System;
using System.Text.RegularExpressions;

namespace LabelPrint.Models
{
    public class PinCodeEntity : BaseViewModel
    {
        #region Constructor

        public PinCodeEntity()
        {
            //
        }

        #endregion

        #region Public fields and properties

        private string _printValue;
        /// <summary>
        /// Печатаемое значение.
        /// </summary>
        public string PrintValue
        {
            get => _printValue;
            set
            {
                _printValue = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Текущий пин-код.
        /// </summary>
        public int Current
        {
            get => DateTime.Now.Hour * 100 + DateTime.Now.Minute;
        }

        private int _input;
        /// <summary>
        /// Вводимый пин-код.
        /// </summary>
        public int Input
        {
            get => _input;
            set
            {
                _input = value;
                PrintValue = value == 0 ? "...." : Regex.Replace(value.ToString(), "[0-9]", "*");
                AccessGranted = Input == Current;
                //// Страница настроек.
                //if (_pageSettings == null)
                //    _pageSettings = new PageSettings();
                //if (FrameMain.Content != null)
                //{
                //    if (!(FrameMain.Content is PageSettings))
                //        FrameMain.Navigate(_pageSettings);
                //}
                //else
                //    FrameMain.Navigate(_pageSettings);
                //// Перед загрузкой.
                //_pageSettings.BeforeLoaded();
                OnPropertyChanged();
            }
        }

        private bool _accessGranted;
        /// <summary>
        /// Проверка доступа.
        /// </summary>
        /// <returns></returns>
        public bool AccessGranted
        {
            get => _accessGranted;
            private set
            {
                _accessGranted = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }
}
