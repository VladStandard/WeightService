using Ws.Database.Core.Models;
using Ws.Shared.Enums;

namespace Ws.StorageCoreTests.Views;

public class ViewRepositoryTests
{
    protected SqlCrudConfigModel SqlCrudConfig { get; private set; }
    protected List<EnumConfiguration> AllConfigurations { get; }

    public ViewRepositoryTests()
    {
        SqlCrudConfig = new();
        AllConfigurations = [EnumConfiguration.DevelopVs, EnumConfiguration.ReleaseVs];
    }

    [SetUp]
    public void SetUp()
    {
        SqlCrudConfig = new() { SelectTopRowsCount = 10 };
    }
}