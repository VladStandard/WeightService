// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorage.Models;

namespace WsStorageCoreTests.Fields;

[TestFixture]
internal class FieldsAssertEqualsNewTests
{
    #region Public and private methods

    [Test]
    public void FieldBinaryModel_Assert_EqualsNew()
    {
        DataCoreTestsUtils.DataCore.FieldBaseModelAssertEqualsNew<SqlFieldBinaryModel>();
    }

    [Test]
    public void FieldIdentityModel_Assert_EqualsNew()
    {
        DataCoreTestsUtils.DataCore.FieldBaseModelAssertEqualsNew<SqlFieldIdentityModel>();
    }

    [Test]
    public void FieldMacAddressModel_Assert_EqualsNew()
    {
        DataCoreTestsUtils.DataCore.FieldBaseModelAssertEqualsNew<SqlFieldMacAddressModel>();
    }

    #endregion
}
