// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NUnit.Framework;
using System.Runtime.CompilerServices;

namespace CoreTests
{
    /// <summary>
    /// Utilites.
    /// </summary>
    public static class TestsUtils
    {
        public static string ConectionStringDevelop(bool isTrusted) => isTrusted
            ? @"Data Source=CREATIO\INS1;Initial Catalog=Scales;Persist Security Info=True;Trusted Connection=True;TrustServerCertificate=True;"
            : @"Data Source=CREATIO\INS1;Initial Catalog=Scales;Persist Security Info=True;User ID=scale01;Password=scale01;TrustServerCertificate=True;";
        public static string ConectionStringProduct(bool isTrusted) => isTrusted
            ? @"Data Source=PALYCH\LUTON;Initial Catalog=ScalesDB;Persist Security Info=True;Trusted Connection=True;TrustServerCertificate=True;"
            : @"Data Source=PALYCH\LUTON;Initial Catalog=ScalesDB;Persist Security Info=True;User ID=scale01;Password=scale01;TrustServerCertificate=True;";

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
    }
}