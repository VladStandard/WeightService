// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Access;
using DataCore.Sql.TableScaleModels.Apps;


namespace DataCoreTests.Sql;

[TestFixture]
internal class TablesEqualsNewTests
{
    #region Public and private fields, properties, constructor

    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    #endregion

    #region Public and private methods

    [Test]
    public void DbTable_Validate_AccessModel()
    {
        DataCore.TableBaseModelAssertEqualsNew<AccessModel>();
    }

    [Test]
    public void DbTable_Validate_AppModel()
    {
        DataCore.TableBaseModelAssertEqualsNew<AppModel>();
    }

    #endregion
}
