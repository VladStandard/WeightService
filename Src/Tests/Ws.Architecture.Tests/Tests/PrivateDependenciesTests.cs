using Ws.Architecture.Tests.Utils;

namespace Ws.Architecture.Tests.Tests;

public class PrivateDependenciesTests
{
    public static readonly TheoryData<string, string> TestData = SolutionUtils.GetProjectFiles();

    [Theory]
    [MemberData(nameof(TestData))]
    public void Shared_dependency_must_be_private(string file, string filePath)
    {
        ProjectFileUtils.CheckPrivateProject(filePath, "Ws.Shared")
            .Should().BeTrue(Path.GetFileName(file));
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public void EntityFramework_dependency_must_be_private(string file, string filePath)
    {
        ProjectFileUtils.CheckPrivateProject(filePath, "Ws.Database.EntityFramework")
            .Should().BeTrue(Path.GetFileName(file));
    }
}