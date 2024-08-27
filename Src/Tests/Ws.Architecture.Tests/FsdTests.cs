using System.Reflection;

namespace Ws.Architecture.Tests;

public static class DllConstants
{
    public const string DeviceControl = @"..\..\..\..\..\Apps\Web\DeviceControl\bin\Develop_x64\net8.0-windows10.0.19041.0\DeviceControl.dll";
    public const string ScalesDesktop = @"..\..\..\..\..\Apps\Desktop\ScalesDesktop\bin\Develop_x64\net8.0-windows10.0.19041.0\win10-x64\ScalesDesktop.dll";
    public const string ScalesMobile = @"..\..\..\..\..\Apps\Mobile\ScalesMobile\bin\Develop_x64\net8.0-android34.0\ScalesMobile.dll";
}

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
    public void Shared_Should_HaveNoDependencies(string projectName, string assembly)
    {
        Types types = Types.InAssembly(Assembly.LoadFrom(assembly));

        TestResult result = types
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
    public void Features_Should_OnlyReference_Shared(string projectName, string assembly)
    {
        Types types = Types.InAssembly(Assembly.LoadFrom(assembly));

        TestResult result = types
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
    public void Widgets_Should_OnlyReference_Shared_And_Features(string projectName, string assembly)
    {
        Types types = Types.InAssembly(Assembly.LoadFrom(assembly));

        TestResult result = types
            .That()
            .ResideInNamespace($"{projectName}.Source.Widgets")
            .ShouldNot()
            .HaveDependencyOn($"{projectName}.Source.Pages")
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}