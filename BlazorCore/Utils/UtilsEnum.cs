using System;
using System.Collections.Generic;
using BlazorCore.Models;

namespace BlazorCore.Utils
{
    public static class UtilsEnum
    {
        public static bool? GetEnumRelevanceStatusBool(short? value)
        {
            return value switch
            {
                1 => true,
                2 => false,
                _ => null
            };
        }

        public static EnumRelevanceStatus GetEnumRelevanceStatusEng(short? value)
        {
            return value switch
            {
                1 => EnumRelevanceStatus.Actual,
                2 => EnumRelevanceStatus.NoActual,
                _ => EnumRelevanceStatus.Unknown
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
        
        public static EnumNormilizationStatus GetEnumNormalizationStatusEng(short? value)
        {
            return value switch
            {
                1 => EnumNormilizationStatus.NormilizedFull,
                2 => EnumNormilizationStatus.NormilizedPart,
                3 => EnumNormilizationStatus.NotSubjectNormalization,
                _ => EnumNormilizationStatus.NotNormilized
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

        public static IEnumerable<EnumRelevanceStatus> GetEnumRelevenaceStatusesEng()
        {
            return (EnumRelevanceStatus[]) Enum.GetValues(typeof(EnumRelevanceStatus));
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
        
        public static IEnumerable<EnumNormilizationStatus> GetEnumNormilizationStatusesEng()
        {
            return (EnumNormilizationStatus[]) Enum.GetValues(typeof(EnumNormilizationStatus));
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