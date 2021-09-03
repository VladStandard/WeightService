// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
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

        public List<TypeEntity<EnumLang>> GetTemplateLanguages()
        {
            return LocalizationData.Lang switch
            {
                EnumLang.English => GetTemplateLanguagesEng(),
                EnumLang.Russian => GetTemplateLanguagesRus(),
                _ => new List<TypeEntity<EnumLang>>()
            };
        }

        private List<TypeEntity<EnumLang>> GetTemplateLanguagesEng()
        {
            return new()
            {
                new TypeEntity<EnumLang>("English", EnumLang.English),
                new TypeEntity<EnumLang>("Russian", EnumLang.Russian),
            };
        }

        private List<TypeEntity<EnumLang>> GetTemplateLanguagesRus()
        {
            return new()
            {
                new TypeEntity<EnumLang>("Английский", EnumLang.English),
                new TypeEntity<EnumLang>("Русский", EnumLang.Russian),
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
