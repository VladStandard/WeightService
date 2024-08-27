using System.Reflection;
using Microsoft.Build.Construction;

namespace Ws.Architecture.Tests.Common;

public class SolutionReader
{
    public static TheoryData<string, Assembly> GetAllAssemblies()
    {
        const string relativePath = @"..\..\..\..\..\..\WeightService.sln";
        string absolutePath = Path.GetFullPath(relativePath);
        SolutionFile solutionFile = SolutionFile.Parse(absolutePath);

        TheoryData<string, Assembly> assemblies = [];

        foreach (ProjectInSolution project in solutionFile.ProjectsInOrder)
        {
            if (!project.AbsolutePath.EndsWith(".csproj", StringComparison.OrdinalIgnoreCase)) continue;
            if (project.AbsolutePath.Contains("Test", StringComparison.OrdinalIgnoreCase)) continue;

            string outputDirectory = Path.GetDirectoryName(project.AbsolutePath) + @"\bin\Develop_x64";
            string projectName = Path.GetFileName(project.AbsolutePath).Replace(".csproj", "");
            string assemblyString = Directory.GetFiles(outputDirectory, $"{projectName}.dll", SearchOption.AllDirectories).First();

            assemblies.Add(projectName, Assembly.LoadFrom(assemblyString));
        }
        return assemblies;
    }

    public static TheoryData<string, Assembly> GetFrontendAssemblies()
    {
        const string relativePath = @"..\..\..\..\..\..\WeightService.sln";
        string absolutePath = Path.GetFullPath(relativePath);
        SolutionFile solutionFile = SolutionFile.Parse(absolutePath);

        TheoryData<string, Assembly> assemblies = [];

        foreach (ProjectInSolution project in solutionFile.ProjectsInOrder)
        {
            if (project.AbsolutePath.Contains("Ws", StringComparison.OrdinalIgnoreCase)) continue;
            if (project.AbsolutePath.Contains("Test", StringComparison.OrdinalIgnoreCase)) continue;
            if (project.AbsolutePath.Contains("Api", StringComparison.OrdinalIgnoreCase)) continue;

            if (!project.AbsolutePath.EndsWith(".csproj", StringComparison.OrdinalIgnoreCase)) continue;
            if (!project.AbsolutePath.Contains("Apps", StringComparison.OrdinalIgnoreCase)) continue;
            if (!project.AbsolutePath.Contains('.', StringComparison.OrdinalIgnoreCase)) continue;

            string outputDirectory = Path.GetDirectoryName(project.AbsolutePath) + @"\bin\Develop_x64";
            string projectName = Path.GetFileName(project.AbsolutePath).Replace(".csproj", "");
            string assemblyPath = Directory.GetFiles(outputDirectory, $"{projectName}.dll", SearchOption.AllDirectories).First();

            assemblies.Add(projectName, Assembly.LoadFrom(assemblyPath));
        }
        return assemblies;
    }
}