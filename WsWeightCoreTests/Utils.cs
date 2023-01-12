// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NUnit.Framework;
using System.Runtime.CompilerServices;

namespace WeightCoreTests;

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
