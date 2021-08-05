// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;

namespace BlazorCore.Models
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

        public List<TypeEntity<string>> GetTemplateLanguagesEng()
        {
            return new()
            {
                new TypeEntity<string>("English", "English"),
                new TypeEntity<string>("Russian", "Russian"),
            };
        }

        public List<TypeEntity<string>> GetTemplateLanguagesRus()
        {
            return new()
            {
                new TypeEntity<string>("English", "Английский"),
                new TypeEntity<string>("Russian", "Русский"),
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
