// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace DataCoreTests;

/// <summary>
/// Utilites.
/// </summary>
public static class TestsUtils
{
    #region Public and private fields and properties

    public static List<string> GetListNomenclatureCode => new()
    {
        "жад00018851",
        "жад00008809",
        "жад00008818",
        "жад00009938",
        "жад00009937",
    };
    public static List<int> GetListNomenclatureId => new()
    {
        -2147460739,
        -2147440723,
        -2147460730,
        -2147464402,
        -2147464403,
    };
    public static List<string> GetListUrlNomenclatureV1 => new()
    {
        "https://t1000.kolbasa-vs.local:443/api/nomenclature",
        "https://t1000-preview.kolbasa-vs.local:443/api/nomenclature",
        "https://t1000-dev.kolbasa-vs.local:443/api/nomenclature",
        "https://t1000-dev-preview.kolbasa-vs.local:443/api/nomenclature",
    };
    public static List<string> GetListUrlNomenclatureV2 => new()
    {
        "https://t1000.kolbasa-vs.local:443/api/v2/nomenclature",
        "https://t1000-preview.kolbasa-vs.local:443/api/v2/nomenclature",
        "https://t1000-dev.kolbasa-vs.local:443/api/v2/nomenclature",
        "https://t1000-dev-preview.kolbasa-vs.local:443/api/v2/nomenclature",
    };
    public static List<string> GetListContragentCode => new()
    {
        "жад001603",
        "000000130",
        "жад001626",
        "жад001627",
        "жад001629",
    };
    public static List<int> GetListContragentId => new()
    {
        -2147482738,
        -2147477242,
        -2147478355,
        -2147482505,
        -2147482782,
    };
    public static List<string> GetListUrlContragentV1 => new()
    {
        "https://t1000.kolbasa-vs.local:443/api/contragent",
        "https://t1000-preview.kolbasa-vs.local:443/api/contragent",
        "https://t1000-dev.kolbasa-vs.local:443/api/contragent",
        "https://t1000-dev-preview.kolbasa-vs.local:443/api/contragent",
    };
    public static List<string> GetListUrlContragentV2 => new()
    {
        "https://t1000.kolbasa-vs.local:443/api/v2/contragent",
        "https://t1000-preview.kolbasa-vs.local:443/api/v2/contragent",
        "https://t1000-dev.kolbasa-vs.local:443/api/v2/contragent",
        "https://t1000-dev-preview.kolbasa-vs.local:443/api/v2/contragent",
    };
    public static List<string> GetProdListUrlInfoV1 => new()
    {
        "https://t1000.kolbasa-vs.local:443/api/info",
        "https://t1000-preview.kolbasa-vs.local:443/api/info",
    };
    public static List<string> GetDevListUrlInfoV1 => new()
    {
        "https://t1000-dev.kolbasa-vs.local:443/api/info",
        "https://t1000-dev-preview.kolbasa-vs.local:443/api/info",
    };
    public static List<string> GetProdListUrlInfoV2 => new()
    {
        "https://t1000.kolbasa-vs.local:443/api/v2/info",
        "https://t1000-preview.kolbasa-vs.local:443/api/v2/info",
    };
    public static List<string> GetDevListUrlInfoV2 => new()
    {
        "https://t1000-dev.kolbasa-vs.local:443/api/v2/info",
        "https://t1000-dev-preview.kolbasa-vs.local:443/api/v2/info",
    };

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
