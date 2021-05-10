// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows;
using WebSocketsExamples.ViewModels;

// ReSharper disable CommentTypo

namespace WebSocketsExamples.Models
{
    public static class Utils
    {
        /// <summary>
        /// Получить программные настройки.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="resource"></param>
        /// <returns></returns>
        public static AppSettings GetSettings(FrameworkElement element, string resource = "ViewModelAppSettings")
        {
            var context = element.FindResource(resource);
            if (context is AppSettings settings)
            {
                return settings;
            }
            return null;
        }
    }
}
