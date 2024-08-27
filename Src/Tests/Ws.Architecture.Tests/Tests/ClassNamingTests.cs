namespace Ws.Architecture.Tests.Tests;

public class ClassNamingTests
{
    public static TheoryData<string, Assembly> TestData => SolutionUtils.GetAllAssemblies();

    [Theory]
    [MemberData(nameof(TestData))]
    public void All_Utils_Classes_Should_Be_Static(string _, Assembly project)
    {
        TestResult? result = Types.InAssembly(project)
            .That()
            .HaveNameEndingWith("Utils")
            .Should()
            .BeStatic()
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public void All_Enums_Must_End_On_Type(string projectName, Assembly project)
    {
        Types? types = Types.InAssembly(project);

        TestResult? result = types
            .That()
            .ResideInNamespace(projectName)
            .And()
            .AreEnums()
            .Should()
            .HaveNameEndingWith("Type")
            .GetResult();

        result.FailingTypes.Should().BeEmpty();
    }
}