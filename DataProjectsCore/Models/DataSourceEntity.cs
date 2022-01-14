// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore;
using DataShareCore.Models;
using System.Collections.Generic;

namespace DataProjectsCore.Models
{
    public class DataSourceEntity
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

        public List<TypeEntity<string>> GetTemplateCategories()
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

        public List<TypeEntity<ShareEnums.Lang>> GetTemplateLanguages()
        {
            return LocalizationCore.Lang switch
            {
                ShareEnums.Lang.English => GetTemplateLanguagesEng(),
                ShareEnums.Lang.Russian => GetTemplateLanguagesRus(),
                _ => new List<TypeEntity<ShareEnums.Lang>>()
            };
        }

        private List<TypeEntity<ShareEnums.Lang>> GetTemplateLanguagesEng()
        {
            return new()
            {
                new TypeEntity<ShareEnums.Lang>("English", ShareEnums.Lang.English),
                new TypeEntity<ShareEnums.Lang>("Russian", ShareEnums.Lang.Russian),
            };
        }

        private List<TypeEntity<ShareEnums.Lang>> GetTemplateLanguagesRus()
        {
            return new()
            {
                new TypeEntity<ShareEnums.Lang>("Английский", ShareEnums.Lang.English),
                new TypeEntity<ShareEnums.Lang>("Русский", ShareEnums.Lang.Russian),
            };
        }

        public List<TypeEntity<bool>> GetTemplateIsDebug()
        {
            return new()
            {
                new TypeEntity<bool>("Enable", true),
                new TypeEntity<bool>("Disable", false),
            };
        }

        #endregion
    }
}
