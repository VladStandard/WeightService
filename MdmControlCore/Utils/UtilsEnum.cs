// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore;
using System;
using System.Collections.Generic;

namespace MdmControlCore.Utils
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
            return (ShareEnums.RelevanceStatus[]) Enum.GetValues(typeof(ShareEnums.RelevanceStatus));
        }
        
        public static IEnumerable<TypeEntity<short>> GetEnumRelevenaceStatusesRus()
        {
            var result = new List<TypeEntity<short>>
            {
                new("Неизвестно", 0),
                new("Актуально", 1),
                new("Неактуально", 2),
            };
            return result;
        }
        
        public static IEnumerable<ShareEnums.NormilizationStatus> GetEnumNormilizationStatusesEng()
        {
            return (ShareEnums.NormilizationStatus[]) Enum.GetValues(typeof(ShareEnums.NormilizationStatus));
        }
        
        public static IEnumerable<TypeEntity<short>> GetEnumNormilizationStatusesRus()
        {
            var result = new List<TypeEntity<short>>
            {
                new("Ненормализована", 0),
                new("Нормализована полностью", 1),
                new("Нормализована частично", 2),
                new("Не подлежит нормализации", 3),
            };
            return result;
        }
    }
}