// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.NomenclaturesGroupsFks;

namespace DataCoreTests.Sql.TableScaleFkModels.NomenclaturesGroupFks;

[TestFixture]
internal class NomenclatureGroupFkSerializeTests
{
	[Test]
    public void Item_Serialize_Validate()
    {
		DataCoreTestsUtils.DataCore.AssertSqlDbContentSerialize<NomenclaturesGroupFkModel>();
	}
}