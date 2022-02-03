// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using DataCore.Models;
using NUnit.Framework;

namespace DataCoreTests.DAL
{
    public enum EnumDb
    {
        Debug,
        Release
    }

    [TestFixture]
    internal static class DataAccessUtilsTests
    {
        public static JsonSettingsEntity JsonSettings { get; private set; }
        public static DataAccessEntity DataAccess { get; }

        static DataAccessUtilsTests()
        {
            JsonSettings = GetAppSettings();
            DataAccess = GetDataAccess();
        }

        private static JsonSettingsEntity GetAppSettings()
        {
            if (JsonSettings != null)
                return JsonSettings;
            JsonSettings?.CheckProperties();
            return JsonSettings;
        }

        private static DataAccessEntity GetDataAccess()
        {
            if (DataAccess != null)
                return DataAccess;
            return new DataAccessEntity(JsonSettings);
        }
    }
}
