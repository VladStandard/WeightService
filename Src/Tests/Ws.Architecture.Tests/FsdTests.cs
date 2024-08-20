using System.Reflection;

namespace Ws.Architecture.Tests;

public class FsdTests
{
    [Theory]
    [InlineData("DeviceControl", @"..\..\..\..\..\Apps\Web\DeviceControl\bin\Develop_x64\net8.0-windows10.0.19041.0\DeviceControl.dll")]
    [InlineData("ScalesDesktop", @"..\..\..\..\..\Apps\Desktop\ScalesDesktop\bin\Develop_x64\net8.0-windows10.0.19041.0\win10-x64\ScalesDesktop.dll")]
    [InlineData("ScalesMobile", @"..\..\..\..\..\Apps\Mobile\ScalesMobile\bin\Develop_x64\net8.0-android34.0\ScalesMobile.dll")]
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
    [InlineData("DeviceControl", @"..\..\..\..\..\Apps\Web\DeviceControl\bin\Develop_x64\net8.0-windows10.0.19041.0\DeviceControl.dll")]
    [InlineData("ScalesDesktop", @"..\..\..\..\..\Apps\Desktop\ScalesDesktop\bin\Develop_x64\net8.0-windows10.0.19041.0\win10-x64\ScalesDesktop.dll")]
    [InlineData("ScalesMobile", @"..\..\..\..\..\Apps\Mobile\ScalesMobile\bin\Develop_x64\net8.0-android34.0\ScalesMobile.dll")]
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
    [InlineData("DeviceControl", @"..\..\..\..\..\Apps\Web\DeviceControl\bin\Develop_x64\net8.0-windows10.0.19041.0\DeviceControl.dll")]
    [InlineData("ScalesDesktop", @"..\..\..\..\..\Apps\Desktop\ScalesDesktop\bin\Develop_x64\net8.0-windows10.0.19041.0\win10-x64\ScalesDesktop.dll")]
    [InlineData("ScalesMobile", @"..\..\..\..\..\Apps\Mobile\ScalesMobile\bin\Develop_x64\net8.0-android34.0\ScalesMobile.dll")]
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