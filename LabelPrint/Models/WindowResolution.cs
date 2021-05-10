// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.ObjectModel;

namespace LabelPrint.Models
{
    public class WindowResolution
    {
        #region Constructor

        public WindowResolution()
        {
            //
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Список размеров.
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<string> GetItems()
        {
            return new ObservableCollection<string>()
            {
                "Максимальное", 
                "800x600", 
                "1024х768", 
                "1366х768", 
                "1920х1080" 
            };
        }

        #endregion
    }
}
