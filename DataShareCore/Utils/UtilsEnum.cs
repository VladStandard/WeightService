// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore.Models;
using System;
using System.Collections.Generic;

namespace DataShareCore.Utils
{
    public static class UtilsEnum
    {
        // https://stackoverflow.com/questions/79126/create-generic-method-constraining-t-to-an-enum
        public static T ParseEnum<T>(string value, T defaultValue) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum) throw new ArgumentException("T must be an enumerated type");
            if (string.IsNullOrEmpty(value)) return defaultValue;

            foreach (T item in Enum.GetValues(typeof(T)))
            {
                if (item.ToString().ToLower().Equals(value.Trim().ToLower())) return item;
            }
            return defaultValue;
        }

        // https://stackoverflow.com/questions/79126/create-generic-method-constraining-t-to-an-enum
        public static Dictionary<int, string> EnumNamedValues<T>() where T : Enum
        {
            var result = new Dictionary<int, string>();
            var values = Enum.GetValues(typeof(T));

            foreach (int item in values)
                result.Add(item, Enum.GetName(typeof(T), item));
            return result;
        }

        public static bool? GetEnumRelevanceStatusBool(short? value)
        {
            return value switch
            {
                1 => true,
                2 => false,
                _ => null
            };
        }

        public static ShareEnums.RelevanceStatus GetEnumRelevanceStatusEng(short? value)
        {
            return value switch
            {
                1 => ShareEnums.RelevanceStatus.Actual,
                2 => ShareEnums.RelevanceStatus.NoActual,
                _ => ShareEnums.RelevanceStatus.Unknown
            };
        }

        public static string GetEnumRelevanceStatusRus(short? value)
        {
            return value switch
            {
                1 => "Актуальна",
                2 => "Неактуальна",
                _ => "Неизвестна"
            };
        }

        public static ShareEnums.NormilizationStatus GetEnumNormalizationStatusEng(short? value)
        {
            return value switch
            {
                1 => ShareEnums.NormilizationStatus.NormilizedFull,
                2 => ShareEnums.NormilizationStatus.NormilizedPart,
                3 => ShareEnums.NormilizationStatus.NotSubjectNormalization,
                _ => ShareEnums.NormilizationStatus.NotNormilized
            };
        }

        public static string GetEnumNormalizationStatusRus(short? value)
        {
            return value switch
            {
                1 => "Нормализована полностью",
                2 => "Нормализована частично",
                3 => "Не подлежит нормализации",
                _ => "Ненормализована"
            };
        }

        public static IEnumerable<ShareEnums.RelevanceStatus> GetEnumRelevenaceStatusesEng()
        {
            return (ShareEnums.RelevanceStatus[])Enum.GetValues(typeof(ShareEnums.RelevanceStatus));
        }

        public static IEnumerable<TypeEntity<short>> GetEnumRelevenaceStatusesRus()
        {
            var result = new List<TypeEntity<short>>
            {
                new TypeEntity<short>("Неизвестно", 0),
                new TypeEntity<short>("Актуально", 1),
                new TypeEntity<short>("Неактуально", 2),
            };
            return result;
        }

        public static IEnumerable<ShareEnums.NormilizationStatus> GetEnumNormilizationStatusesEng()
        {
            return (ShareEnums.NormilizationStatus[])Enum.GetValues(typeof(ShareEnums.NormilizationStatus));
        }

        public static IEnumerable<TypeEntity<short>> GetEnumNormilizationStatusesRus()
        {
            var result = new List<TypeEntity<short>>
            {
                new TypeEntity<short>("Ненормализована", 0),
                new TypeEntity<short>("Нормализована полностью", 1),
                new TypeEntity<short>("Нормализована частично", 2),
                new TypeEntity<short>("Не подлежит нормализации", 3),
            };
            return result;
        }

        public static string GetDayOfWeekRu(DayOfWeek day)
        {
            return day switch
            {
                DayOfWeek.Monday => "Понедельник",
                DayOfWeek.Tuesday => "Вторник",
                DayOfWeek.Wednesday => "Среда",
                DayOfWeek.Thursday => "Четверг",
                DayOfWeek.Friday => "Пятница",
                DayOfWeek.Saturday => "Суббота",
                DayOfWeek.Sunday => "Воскресенье",
                _ => string.Empty,
            };
        }
    }
}