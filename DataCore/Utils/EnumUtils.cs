// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Utils;

/// <summary>
/// Enumeration utilities.
/// </summary>
public static class EnumUtils
{
    #region Public and private methods

    // https://stackoverflow.com/questions/79126/create-generic-method-constraining-t-to-an-enum
    public static T ParseEnum<T>(string value, T defaultValue) where T : struct, IConvertible
    {
        if (!typeof(T).IsEnum) throw new ArgumentException("T must be an enumerated type!");
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
        Dictionary<int, string>? result = new();
        Array? values = Enum.GetValues(typeof(T));

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

    public static RelevanceStatusEnum GetEnumRelevanceStatusEng(short? value)
    {
        return value switch
        {
            1 => RelevanceStatusEnum.Actual,
            2 => RelevanceStatusEnum.NoActual,
            _ => RelevanceStatusEnum.Unknown
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

    public static NormilizationStatusEnum GetEnumNormalizationStatusEng(short? value)
    {
        return value switch
        {
            1 => NormilizationStatusEnum.NormilizedFull,
            2 => NormilizationStatusEnum.NormilizedPart,
            3 => NormilizationStatusEnum.NotSubjectNormalization,
            _ => NormilizationStatusEnum.NotNormilized
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

    public static IEnumerable<RelevanceStatusEnum> GetEnumRelevenaceStatusesEng()
    {
        return (RelevanceStatusEnum[])Enum.GetValues(typeof(RelevanceStatusEnum));
    }

    public static IEnumerable<TypeModel<short>> GetEnumRelevenaceStatusesRus()
    {
        List<TypeModel<short>>? result = new()
        {
            new("Неизвестно", 0),
            new("Актуально", 1),
            new("Неактуально", 2)
        };
        return result;
    }

    public static IEnumerable<NormilizationStatusEnum> GetEnumNormilizationStatusesEng()
    {
        return (NormilizationStatusEnum[])Enum.GetValues(typeof(NormilizationStatusEnum));
    }

    public static IEnumerable<TypeModel<short>> GetEnumNormilizationStatusesRus()
    {
        List<TypeModel<short>>? result = new()
        {
            new("Ненормализована", 0),
            new("Нормализована полностью", 1),
            new("Нормализована частично", 2),
            new("Не подлежит нормализации", 3)
        };
        return result;
    }

    public static string GetDayOfWeekRu(DayOfWeek day) => 
        day switch
        {
            DayOfWeek.Monday => "Понедельник",
            DayOfWeek.Tuesday => "Вторник",
            DayOfWeek.Wednesday => "Среда",
            DayOfWeek.Thursday => "Четверг",
            DayOfWeek.Friday => "Пятница",
            DayOfWeek.Saturday => "Суббота",
            DayOfWeek.Sunday => "Воскресенье",
            _ => string.Empty
        };

    #endregion
}