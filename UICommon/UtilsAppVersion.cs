// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Reflection;
// ReSharper disable UnusedMember.Global
// ReSharper disable CheckNamespace
// ReSharper disable CommentTypo

namespace WeightServices.Common
{
    /// <summary>
    /// Строковый формат.
    /// </summary>
    public enum EnumStringFormat
    {
        AsString,
        Use1,
        Use2,
        Use3,
        Use4,
    }

    /// <summary>
    /// Количество символов для строки версии ПО.
    /// </summary>
    public enum EnumVerCountDigits
    {
        Use1,
        Use2,
        Use3,
    }

    public static class UtilsAppVersion
    {
        /// <summary>
        /// Текущая версия.
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="countDigits"></param>
        /// <param name="stringFormats"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public static string GetCurrentVersion(Assembly assembly, EnumVerCountDigits countDigits, List<EnumStringFormat> stringFormats = null, Version version = null)
        {
            if (version == null)
                version = assembly.GetName().Version;
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
            var version4 = $"{major}.{minor}.{build}.{revision}";
            var version3 = $"{major}.{minor}.{build}";
            var version2 = $"{major}.{minor}";
            var version1 = $"{major}";

            return countDigits == EnumVerCountDigits.Use1
                ? version1 : countDigits == EnumVerCountDigits.Use2
                ? version2 : countDigits == EnumVerCountDigits.Use3
                ? version3 : version4;
        }

        /// <summary>
        /// Подстрока текущей версии.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetCurrentVersionSubString(string input)
        {
            var result = string.Empty;
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
        public static string GetCurrentVersionFormat(int input, EnumStringFormat format)
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
        /// Получить описание проекта.
        /// </summary>
        /// <returns></returns>
        public static string GetDescription(Assembly assembly)
        {
            var result = string.Empty;
            var attributes = assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
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
            var result = string.Empty;
            var attributes = assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
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
            return $@"{GetTitle(assembly)} {GetCurrentVersion(assembly, EnumVerCountDigits.Use3, null, version)}";
        }
    }
}
