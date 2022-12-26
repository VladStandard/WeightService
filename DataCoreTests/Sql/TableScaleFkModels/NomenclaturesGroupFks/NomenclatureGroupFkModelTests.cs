// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.NomenclaturesGroupsFks;

namespace DataCoreTests.Sql.TableScaleFkModels.NomenclaturesGroupFks;

[TestFixture]
internal class NomenclatureGroupFkModelTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_ToString()
    {
        DataCore.TableBaseModelAssertToString<NomenclaturesGroupFkModel>();
    }

    [Test]
    public void Model_EqualsNew()
    {
        DataCore.TableBaseModelAssertEqualsNew<NomenclaturesGroupFkModel>();
    }
    
    [Test]
    public void Model_Serialize()
    {
        DataCore.TableBaseModelAssertSerialize<NomenclaturesGroupFkModel>();
    }
}