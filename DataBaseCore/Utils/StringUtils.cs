// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;

namespace DataBaseCore.Utils
{
    public static class StringUtils
    {
        public static string FormatDtRus(DateTime dt, bool useSeconds)
        {
            return useSeconds
                ? $"{dt.Day:D2}.{dt.Month:D2}.{dt.Year:D4} {dt.Hour:D2}:{dt.Minute:D2}.{dt.Second:D2}"
                : $"{dt.Day:D2}.{dt.Month:D2}.{dt.Year:D4} {dt.Hour:D2}:{dt.Minute:D2}";
        }

        public static string FormatDtEng(DateTime dt, bool useSeconds)
        {
            return useSeconds
                ? $"{dt.Year:D4}-{dt.Month:D2}-{dt.Day:D2} {dt.Hour:D2}:{dt.Minute:D2}.{dt.Second:D2}"
                : $"{dt.Year:D4}-{dt.Month:D2}-{dt.Day:D2} {dt.Hour:D2}:{dt.Minute:D2}";
        }

        public static string FormatCurDtRus(bool useSeconds)
        {
            var dt = DateTime.Now;
            return useSeconds
                ? $"{dt.Day:D2}.{dt.Month:D2}.{dt.Year:D4} {dt.Hour:D2}:{dt.Minute:D2}.{dt.Second:D2}"
                : $"{dt.Day:D2}.{dt.Month:D2}.{dt.Year:D4} {dt.Hour:D2}:{dt.Minute:D2}";
        }

        public static string FormatCurDtEng(bool useSeconds)
        {
            var dt = DateTime.Now;
            return useSeconds
                ? $"{dt.Year:D4}-{dt.Month:D2}-{dt.Day:D2} {dt.Hour:D2}:{dt.Minute:D2}.{dt.Second:D2}"
                : $"{dt.Year:D4}-{dt.Month:D2}-{dt.Day:D2} {dt.Hour:D2}:{dt.Minute:D2}";
        }

        public static string FormatCurTimeRus(bool useSeconds)
        {
            var dt = DateTime.Now;
            return useSeconds
                ? $"{dt.Hour:D2}:{dt.Minute:D2}.{dt.Second:D2}"
                : $"{dt.Hour:D2}:{dt.Minute:D2}";
        }

        public static string FormatCurTimeEng(bool useSeconds)
        {
            var dt = DateTime.Now;
            return useSeconds
                ? $"{dt.Hour:D2}:{dt.Minute:D2}.{dt.Second:D2}"
                : $"{dt.Hour:D2}:{dt.Minute:D2}";
        }

        public static char GetProgressChar(char ch)
        {
            switch (ch)
            {
                case '*':
                    return '/';
                case '/':
                    return '|';
                case '|':
                    return '\\';
                case '\\':
                    return '-';
                case '-':
                    return '/';
                default:
                    return '*';
            }
        }

        public static void StringValueTrim(ref string value, int length, bool isGetFileName = false)
        {
            if (isGetFileName)
                value = Path.GetFileName(value);
            if (value.Length > length)
                value = value.Substring(0, length);
        }
    }
}