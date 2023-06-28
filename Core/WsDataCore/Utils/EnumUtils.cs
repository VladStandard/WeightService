// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsDataCore.Utils;

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

    public static WsEnumRelevanceStatus GetEnumRelevanceStatusEng(short? value)
    {
        return value switch
        {
            1 => WsEnumRelevanceStatus.Actual,
            2 => WsEnumRelevanceStatus.NoActual,
            _ => WsEnumRelevanceStatus.Unknown
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

    public static WsEnumNormilizationStatus GetEnumNormalizationStatusEng(short? value)
    {
        return value switch
        {
            1 => WsEnumNormilizationStatus.NormilizedFull,
            2 => WsEnumNormilizationStatus.NormilizedPart,
            3 => WsEnumNormilizationStatus.NotSubjectNormalization,
            _ => WsEnumNormilizationStatus.NotNormilized
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

    public static IEnumerable<WsEnumRelevanceStatus> GetEnumRelevenaceStatusesEng()
    {
        return (WsEnumRelevanceStatus[])Enum.GetValues(typeof(WsEnumRelevanceStatus));
    }

    public static IEnumerable<WsEnumTypeModel<short>> GetEnumRelevenaceStatusesRus()
    {
        List<WsEnumTypeModel<short>>? result = new()
        {
            new("Неизвестно", 0),
            new("Актуально", 1),
            new("Неактуально", 2)
        };
        return result;
    }

    public static IEnumerable<WsEnumNormilizationStatus> GetEnumNormilizationStatusesEng()
    {
        return (WsEnumNormilizationStatus[])Enum.GetValues(typeof(WsEnumNormilizationStatus));
    }

    public static IEnumerable<WsEnumTypeModel<short>> GetEnumNormilizationStatusesRus()
    {
        List<WsEnumTypeModel<short>>? result = new()
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