using NUnit.Framework;
using System.Runtime.CompilerServices;

namespace HardwareTests
{
    /// <summary>
    /// Utilites.
    /// </summary>
    public static class Utils
    {
        internal static void MethodStart([CallerMemberName] string memberName = "")
        {
            TestContext.WriteLine(@"--------------------------------------------------------------------------------");
            TestContext.WriteLine($@"{memberName} start.");
            TestContext.WriteLine();
        }

        internal static void MethodComplete([CallerMemberName] string memberName = "")
        {
            TestContext.WriteLine();
            TestContext.WriteLine($@"{memberName} complete.");
        }
    }
}
