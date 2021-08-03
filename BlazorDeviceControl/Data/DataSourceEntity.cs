// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using BlazorCore.Models;

namespace BlazorDeviceControl.Data
{
    public class DataSourceEntity
    {
        #region Public and private methods

        public override string ToString()
        {
            return $"{nameof(GetTemplateCategories)}: {GetTemplateCategories().Count}";
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

        #endregion
    }
}
