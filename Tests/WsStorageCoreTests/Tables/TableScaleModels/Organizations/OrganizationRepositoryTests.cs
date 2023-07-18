namespace WsStorageCoreTests.Tables.TableScaleModels.Organizations;

[TestFixture]
public sealed class OrganizationRepositoryTests : TableRepositoryTests
{
    private WsSqlOrganizationRepository OrganizationRepository  { get; set; } = new();
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlOrganizationModel> items = OrganizationRepository.GetList(SqlCrudConfig);
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, DefaultPublishTypes);
    }
}