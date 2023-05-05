// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableRefModels.Plus1CFk;

namespace WsStorageContextTests.TableRefModels;

[TestFixture]
public sealed class WsSqlPlu1CFkContentTests
{
    [Test]
    public void Validate_plus_1c_fks()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<WsSqlPlu1CFkModel>();
    }
}