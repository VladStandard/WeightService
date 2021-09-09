using NUnit.Framework;

namespace BlazorCoreTests.DAL
{
    [TestFixture]
    internal static class DataAccessUtils
    {
        public static CoreSettingsEntity AppSettings { get; private set; }
        public static DataAccessEntity DataAccess { get; }

        static DataAccessUtils()
        {
            AppSettings = GetAppSettings(EnumDb.Debug);
            DataAccess = GetDataAccess();
        }

        private static CoreSettingsEntity GetAppSettings(EnumDb db)
        {
            if (AppSettings != null)
                return AppSettings;
            AppSettings = db switch
            {
                EnumDb.Debug => new CoreSettingsEntity("CREATIO\\INS1", "SCALES", false, "scale01", "scale01"),
                EnumDb.Release => new CoreSettingsEntity("PALYCH\\LUTON", "ScalesDB", false, "scale01", "scale01"),
                _ => null
            };
            AppSettings?.CheckProperties();
            return AppSettings;
        }

        private static DataAccessEntity GetDataAccess()
        {
            if (DataAccess != null)
                return DataAccess;
            return new DataAccessEntity(AppSettings);
        }
    }
}
