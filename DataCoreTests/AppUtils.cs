// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Models;
using System.IO;

namespace DataCoreTests
{
    public static class AppUtils
    {
        public static AppSettingsHelper AppSettings { get; private set; } = AppSettingsHelper.Instance;


        static AppUtils()
        {
            AppSettings.DataAccess.SetupForTests(Directory.GetCurrentDirectory());
        }
    }
}
