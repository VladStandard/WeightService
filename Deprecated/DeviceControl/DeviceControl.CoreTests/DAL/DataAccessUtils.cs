using DeviceControl.Core.DAL;
using DeviceControl.Core.DAL.DataModels;
using DeviceControl.Core.Models;
using NUnit.Framework;

namespace DeviceControl.CoreTests.DAL
{
    [TestFixture]
    internal static class DataAccessUtils
    {
        public static AppSettingsEntity AppSettings { get; private set; }
        public static DataAccessEntity DataAccess { get; }

        static DataAccessUtils()
        {
            AppSettings = GetAppSettings(EnumDb.Develop);
            DataAccess = GetDataAccess();
        }

        private static AppSettingsEntity GetAppSettings(EnumDb db)
        {
            if (AppSettings != null)
                return AppSettings;
            AppSettings = db switch
            {
                EnumDb.Develop => new AppSettingsEntity("DEV1C.kolbasa-vs.local\\DVLP", "ScalesDB", false, "scale01", "scale01"),
                EnumDb.Product => new AppSettingsEntity("192.168.0.26\\WMS", "ScalesDB", false, "scale01", "scale01"),
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
