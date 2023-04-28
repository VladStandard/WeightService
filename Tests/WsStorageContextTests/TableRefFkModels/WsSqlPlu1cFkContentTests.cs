// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable InconsistentNaming

using WsStorageCore.TableRefFkModels.Plus1cFk;

namespace WsStorageContextTests.TableRefFkModels;

[TestFixture]
public sealed class WsSqlPlu1cFkContentTests
{
    [Test]
    public void Validate_plus_1c_fks()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlPlu1cFkModel>();
    }
}