// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using MvvmHelpers;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;

// ReSharper disable CommentTypo

namespace WebSocketsExamples.ViewModels
{
    /// <summary>
    /// Программные настройки.
    /// </summary>
    public class AppSettings : BaseViewModel
    {
        #region Constructor

        public AppSettings()
        {
            ProgramHeader = string.Empty;
            var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                var descriptionAttribute = (AssemblyDescriptionAttribute)attributes[0];
                ProgramHeader = descriptionAttribute.Description;
            }
        }

        #endregion

        #region Public properties

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
                OnPropertyChanged();
            }
        }

        #endregion

        #region Public methods

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

        #endregion
    }
}
