// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using static DataCore.ShareEnums;

namespace DataCore.Settings
{
    public class AppVersionHelper
    {
        #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private static AppVersionHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static AppVersionHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Public and private fields, properties, constructor

        public string App { get; private set; }
        public string AppName { get; private set; }
        public string AppDescription { get; set; }
        public string AppTitle { get; private set; }
        public string Version { get; private set; }

        #endregion

        #region Constructor and destructor

        public AppVersionHelper()
        {
            App = string.Empty;
            AppName = string.Empty;
            AppDescription = string.Empty;
            AppTitle = string.Empty;
            Version = string.Empty;
        }

        #endregion
        
        #region Public and private methods

        public string GetCurrentVersion(Assembly assembly, AppVerCountDigits countDigits, List<AppVerStringFormat>? stringFormats = null)
        {
            if (stringFormats == null || stringFormats.Count == 0)
                stringFormats = new List<AppVerStringFormat>() {
                    AppVerStringFormat.Use1, AppVerStringFormat.Use2, AppVerStringFormat.Use2 };

            AppVerStringFormat formatMajor = stringFormats[0];
            AppVerStringFormat formatMinor = AppVerStringFormat.AsString;
            AppVerStringFormat formatBuild = AppVerStringFormat.AsString;
            AppVerStringFormat formatRevision = AppVerStringFormat.AsString;
            if (stringFormats.Count > 1)
                formatMinor = stringFormats[1];
            if (stringFormats.Count > 2)
                formatBuild = stringFormats[2];
            if (stringFormats.Count > 3)
                formatRevision = stringFormats[3];

            Version version = assembly.GetName().Version;
            string major = GetCurrentVersionFormat(version.Major, formatMajor);
            string minor = GetCurrentVersionFormat(version.Minor, formatMinor);
            string build = GetCurrentVersionFormat(version.Build, formatBuild);
            string revision = GetCurrentVersionFormat(version.Revision, formatRevision);
            string version4 = $"{major}.{minor}.{build}.{revision}";
            string version3 = $"{major}.{minor}.{build}";
            string version2 = $"{major}.{minor}";
            string version1 = $"{major}";

            return countDigits == AppVerCountDigits.Use1
                ? version1 : countDigits == AppVerCountDigits.Use2
                ? version2 : countDigits == AppVerCountDigits.Use3
                ? version3 : version4;
        }

        public string GetCurrentVersionSubString(string input)
        {
            string result = string.Empty;
            int idx = input.LastIndexOf('.');
            if (idx >= 0)
                result = input[..idx];
            return result;
        }

        public string GetCurrentVersionFormat(int input, AppVerStringFormat format)
        {
            return format switch
            {
                AppVerStringFormat.Use1 => $"{input:D1}",
                AppVerStringFormat.Use2 => $"{input:D2}",
                AppVerStringFormat.Use3 => $"{input:D3}",
                AppVerStringFormat.Use4 => $"{input:D4}",
                _ => $"{input:D}",
            };
        }

        public string GetDescription(Assembly assembly)
        {
            string result = string.Empty;
            object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                if (attributes[0] is AssemblyDescriptionAttribute attribute)
                    result = attribute.Description;
            }
            return result;
        }

        public string GetTitle(Assembly assembly)
        {
            string result = string.Empty;
            object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            if (attributes.Length > 0)
            {
                if (attributes[0] is AssemblyTitleAttribute attribute)
                    result = attribute.Title;
            }
            return result;
        }

        public void Setup(Assembly assembly)
        {
            AppTitle = $"{GetTitle(assembly)} {GetCurrentVersion(assembly, AppVerCountDigits.Use3)}";
            if (AppTitle.Split(' ').Length > 1)
            {
                App = AppTitle.Split(' ')[0];
                Version = AppTitle.Split(' ')[1];
            }
        }

        #endregion
    }
}
