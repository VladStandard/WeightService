using WsStorageCore.Entities.SchemaScale.Organizations;
namespace WsStorageCoreTests.Tables.TableScaleModels.Organizations;

[TestFixture]
public sealed class OrganizationRepositoryTests : TableRepositoryTests
{
    private WsSqlOrganizationRepository OrganizationRepository { get; set; } = new();

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlOrganizationEntity> items = OrganizationRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }
}