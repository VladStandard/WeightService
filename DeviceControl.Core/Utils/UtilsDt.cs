﻿using System;

namespace DeviceControl.Core.Utils
{
    public static class UtilsDt
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
            DateTime dt = DateTime.Now;
            return useSeconds
                ? $"{dt.Day:D2}.{dt.Month:D2}.{dt.Year:D4} {dt.Hour:D2}:{dt.Minute:D2}.{dt.Second:D2}"
                : $"{dt.Day:D2}.{dt.Month:D2}.{dt.Year:D4} {dt.Hour:D2}:{dt.Minute:D2}";
        }

        public static string FormatCurDtEng(bool useSeconds)
        {
            DateTime dt = DateTime.Now;
            return useSeconds
                ? $"{dt.Year:D4}-{dt.Month:D2}-{dt.Day:D2} {dt.Hour:D2}:{dt.Minute:D2}.{dt.Second:D2}"
                : $"{dt.Year:D4}-{dt.Month:D2}-{dt.Day:D2} {dt.Hour:D2}:{dt.Minute:D2}";
        }
    }
}