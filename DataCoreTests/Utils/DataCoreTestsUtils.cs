// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Helpers;

namespace DataCoreTests.Utils;

internal static class DataCoreTestsUtils
{
    #region Public and private fields, properties, constructor

    internal static DataCoreHelper DataCore => DataCoreHelper.Instance;
    internal static DataAccessHelper DataAccess => DataAccessHelper.Instance;

    #endregion
}