// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Helpers;

namespace AssertCoreTests;

public static class DataCoreTestsUtils
{
    #region Public and private fields, properties, constructor

    public static DataCoreHelper DataCore => DataCoreHelper.Instance;
    public static WsDataAccessHelper DataAccess => WsDataAccessHelper.Instance;

    #endregion
}