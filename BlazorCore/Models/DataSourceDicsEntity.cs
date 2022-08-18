// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Localizations;
using DataCore.Models;
using System.Collections.Generic;

namespace BlazorCore.Models;

/// <summary>
/// Data source dictionaries.
/// </summary>
public class DataSourceDicsEntity
{
    #region Public and private methods

    public override string ToString()
    {
        return
            $"{nameof(GetTemplateCategories)}: {GetTemplateCategories().Count}. " +
            $"{nameof(GetTemplateLanguagesEng)}: {GetTemplateLanguagesEng().Count}. " +
            $"{nameof(GetTemplateLanguagesRus)}: {GetTemplateLanguagesRus().Count}. " +
            $"{nameof(GetTemplateIsDebug)}: {GetTemplateIsDebug().Count}. ";
    }

    public static List<TypeEntity<string>> GetTemplateCategories()
    {
        return new()
        {
            new TypeEntity<string>("", ""),
            new TypeEntity<string>("NaN", "NaN"),
            new TypeEntity<string>("203 dpi", "203 dpi"),
            new TypeEntity<string>("203 dpi tsc", "203 dpi tsc"),
            new TypeEntity<string>("300 dpi", "300 dpi"),
            new TypeEntity<string>("300 dpi tsc", "300 dpi tsc"),
            new TypeEntity<string>("608 dpi", "608 dpi"),
            new TypeEntity<string>("608 dpi tsc", "608 dpi tsc"),
            new TypeEntity<string>("zpl", "zpl"),
        };
    }

    public List<TypeEntity<ShareEnums.AccessRights>> GetTemplateAccessRights(byte? accessRights = null)
    {
        List<TypeEntity<ShareEnums.AccessRights>> result = new();
        result.Add(new TypeEntity<ShareEnums.AccessRights>($"{ShareEnums.AccessRights.None}", ShareEnums.AccessRights.None));
        result.Add(new TypeEntity<ShareEnums.AccessRights>($"{ShareEnums.AccessRights.Read}", ShareEnums.AccessRights.Read));
        result.Add(new TypeEntity<ShareEnums.AccessRights>($"{ShareEnums.AccessRights.Write}", ShareEnums.AccessRights.Write));
        if (accessRights >= (byte)ShareEnums.AccessRights.Admin)
            result.Add(new TypeEntity<ShareEnums.AccessRights>($"{ShareEnums.AccessRights.Admin}", ShareEnums.AccessRights.Admin));
        return result;
    }

    public List<TypeEntity<ShareEnums.Lang>> GetTemplateLanguages()
    {
        return LocaleCore.Lang switch
        {
            ShareEnums.Lang.English => GetTemplateLanguagesEng(),
            ShareEnums.Lang.Russian => GetTemplateLanguagesRus(),
            _ => new List<TypeEntity<ShareEnums.Lang>>()
        };
    }

    private static List<TypeEntity<ShareEnums.Lang>> GetTemplateLanguagesEng()
    {
        return new()
        {
            new TypeEntity<ShareEnums.Lang>($"{ShareEnums.Lang.English}", ShareEnums.Lang.English),
            new TypeEntity<ShareEnums.Lang>($"{ShareEnums.Lang.Russian}", ShareEnums.Lang.Russian),
        };
    }

    private static List<TypeEntity<ShareEnums.Lang>> GetTemplateLanguagesRus()
    {
        return new()
        {
            new TypeEntity<ShareEnums.Lang>("Английский", ShareEnums.Lang.English),
            new TypeEntity<ShareEnums.Lang>("Русский", ShareEnums.Lang.Russian),
        };
    }

    public static List<TypeEntity<bool>> GetTemplateIsDebug()
    {
        return new()
        {
            new TypeEntity<bool>("Enable", true),
            new TypeEntity<bool>("Disable", false),
        };
    }

    #endregion
}
