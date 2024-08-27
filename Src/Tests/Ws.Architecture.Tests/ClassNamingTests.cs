using System.Reflection;
using Ws.Architecture.Tests.Common;

namespace Ws.Architecture.Tests;

public class ClassNamingTests
{
    public static TheoryData<string, Assembly> TestData => SolutionReader.GetFrontendAssemblies();

    [Theory]
    [MemberData(nameof(TestData))]
    public void All_Utils_Classes_Should_Be_Static(string projectName, Assembly project)
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
            .ResideInNamespaceContaining(projectName)
            .And()
            .Inherit(typeof(Enum))
            .Should()
            .HaveNameEndingWith("Type")
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}