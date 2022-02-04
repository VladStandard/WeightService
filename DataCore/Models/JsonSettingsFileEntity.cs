// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Newtonsoft.Json;
using System;
using System.IO;

namespace DataCore.Models
{
    public class JsonSettingsFileEntity
    {
        #region Public and private fields and properties

        public JsonSettingsBase? JsonSettings = null;

        #endregion

        #region Constructor and destructor

        public JsonSettingsFileEntity(string fileName)
        {
            if (File.Exists(fileName))
            {
                string json = File.ReadAllText(fileName);
                JsonSettings = JsonConvert.DeserializeObject<JsonSettingsBase>(json);
            }
        }

        #endregion

        #region Public and private methods

        #endregion
    }
}
