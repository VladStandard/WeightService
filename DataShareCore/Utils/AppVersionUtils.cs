// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Reflection;
using static DataShareCore.Utils.AppVersionEnums;

namespace DataShareCore.Utils
{
    public class AppVersionEnums
    {
        public enum StringFormat
        {
            AsString,
            Use1,
            Use2,
            Use3,
            Use4,
        }

        public enum VerCountDigits
        {
            Use1,
            Use2,
            Use3,
        }
    }

    public static class AppVersionUtils
    {
        /// <summary>
        /// Текущая версия.
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="countDigits"></param>
        /// <param name="stringFormats"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public static string GetCurrentVersion(Assembly assembly, VerCountDigits countDigits, List<StringFormat>? stringFormats = null, Version? version = null)
        {
            if (version == null)
                version = assembly.GetName().Version;
            if (stringFormats == null || stringFormats.Count == 0)
                stringFormats = new List<StringFormat>() { StringFormat.Use1, StringFormat.Use2, StringFormat.Use2 };

            StringFormat formatMajor = stringFormats[0];
            StringFormat formatMinor = StringFormat.AsString;
            StringFormat formatBuild = StringFormat.AsString;
            StringFormat formatRevision = StringFormat.AsString;
            if (stringFormats.Count > 1)
                formatMinor = stringFormats[1];
            if (stringFormats.Count > 2)
                formatBuild = stringFormats[2];
            if (stringFormats.Count > 3)
                formatRevision = stringFormats[3];

            string major = GetCurrentVersionFormat(version.Major, formatMajor);
            string minor = GetCurrentVersionFormat(version.Minor, formatMinor);
            string build = GetCurrentVersionFormat(version.Build, formatBuild);
            string revision = GetCurrentVersionFormat(version.Revision, formatRevision);
            string version4 = $"{major}.{minor}.{build}.{revision}";
            string version3 = $"{major}.{minor}.{build}";
            string version2 = $"{major}.{minor}";
            string version1 = $"{major}";

            return countDigits == VerCountDigits.Use1
                ? version1 : countDigits == VerCountDigits.Use2
                ? version2 : countDigits == VerCountDigits.Use3
                ? version3 : version4;
        }

        /// <summary>
        /// Подстрока текущей версии.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetCurrentVersionSubString(string input)
        {
            string result = string.Empty;
            int idx = input.LastIndexOf('.');
            if (idx >= 0)
                result = input.Substring(0, idx);
            return result;
        }

        /// <summary>
        /// Форматировання подстрока текущей версии.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string GetCurrentVersionFormat(int input, StringFormat format)
        {
            switch (format)
            {
                case StringFormat.Use1:
                    return $"{input:D1}";
                case StringFormat.Use2:
                    return $"{input:D2}";
                case StringFormat.Use3:
                    return $"{input:D3}";
                case StringFormat.Use4:
                    return $"{input:D4}";
            }
            return $"{input:D}";
        }

        /// <summary>
        /// Получить описание проекта.
        /// </summary>
        /// <returns></returns>
        public static string GetDescription(Assembly assembly)
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

        /// <summary>
        /// Получить заголовок проекта.
        /// </summary>
        /// <returns></returns>
        public static string GetTitle(Assembly assembly)
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

        /// <summary>
        /// Получить заголовок ПО.
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public static string GetMainFormText(Assembly assembly, Version version = null)
        {
            return $@"{GetTitle(assembly)} {GetCurrentVersion(assembly, VerCountDigits.Use3, null, version)}";
        }
    }
}
