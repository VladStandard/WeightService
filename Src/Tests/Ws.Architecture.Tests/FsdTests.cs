using System.Reflection;

namespace Ws.Architecture.Tests;

public class FsdTests
{
    public static readonly TheoryData<string, string> TestData = new()
    {
        { "DeviceControl", DllConstants.DeviceControl },
        { "ScalesDesktop", DllConstants.ScalesDesktop },
        { "ScalesMobile", DllConstants.ScalesMobile }
    };

    [Theory]
    [MemberData(nameof(TestData))]
    public void Shared_Should_HaveNoDependencies(string projectName, string assemblyPath)
    {
        Types? types = Types.InAssembly(Assembly.LoadFrom(assemblyPath));

        TestResult? result = types
            .That()
            .ResideInNamespace($"{projectName}.Source.Shared")
            .ShouldNot()
            .HaveDependencyOn($"{projectName}.Source.Pages")
            .Or()
            .HaveDependencyOn($"{projectName}.Source.Widgets")
            .Or()
            .HaveDependencyOn($"{projectName}.Source.Features")
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public void Features_Should_OnlyReference_Shared(string projectName, string assemblyPath)
    {
        Types? assembly = Types.InAssembly(Assembly.LoadFrom(assemblyPath));

        TestResult? result = assembly
            .That()
            .ResideInNamespace($"{projectName}.Source.Features")
            .ShouldNot()
            .HaveDependencyOn($"{projectName}.Source.Pages")
            .Or()
            .HaveDependencyOn($"{projectName}.Source.Widgets")
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public void Widgets_Should_OnlyReference_Shared_And_Features(string projectName, string assemblyPath)
    {
        Types? assembly = Types.InAssembly(Assembly.LoadFrom(assemblyPath));

        TestResult? result = assembly
            .That()
            .ResideInNamespace($"{projectName}.Source.Widgets")
            .ShouldNot()
            .HaveDependencyOn($"{projectName}.Source.Pages")
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}