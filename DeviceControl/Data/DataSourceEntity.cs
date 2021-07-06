using System.Collections.Generic;
using DeviceControl.Core.Models;

namespace BlazorDeviceControl.Data
{
    public class DataSourceEntity
    {
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
    }
}
