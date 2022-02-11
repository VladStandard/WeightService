// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NUnit.Framework;
using System.Runtime.CompilerServices;

namespace DataCoreTests
{
    /// <summary>
    /// Utilites.
    /// </summary>
    public static class TestsUtils
    {
        #region Public and private fields and properties

        public static bool IsPrepare { get; private set; } = false;
        public static string GetUrlDev { get; private set; } = "https://t1000-dev.kolbasa-vs.local:443/api/";
        public static string GetUrlDevPreview { get; private set; } = "https://t1000-dev-preview.kolbasa-vs.local:443/api/";
        public static string GetUrlProd { get; private set; } = "https://t1000.kolbasa-vs.local:443/api/";
        public static string GetUrlProdPreview { get; private set; } = "https://t1000-preview.kolbasa-vs.local:443/api/";

        #endregion

        #region Public and private methods

        public static void MethodStart([CallerMemberName] string memberName = "")
        {
            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
            TestContext.WriteLine($@"{memberName} start.");
            TestContext.WriteLine();
        }

        public static void MethodComplete([CallerMemberName] string memberName = "")
        {
            TestContext.WriteLine();
            TestContext.WriteLine($@"{memberName} complete.");
        }

        #endregion
    }
}
