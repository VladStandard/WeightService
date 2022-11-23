// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WebApiCore.Utils;

/// <summary>
/// Utilites.
/// </summary>
public static class TestsUtils
{
    #region Public and private fields and properties

    public static List<int> GetListNomenclatureId => new()
    {
        -2147460739,
        -2147440723,
        -2147460730,
        -2147464402,
        -2147464403,
    };

    public static List<int> GetListContragentId => new()
    {
        -2147482738,
        -2147477242,
        -2147478355,
        -2147482505,
        -2147482782,
    };

    #endregion


}
