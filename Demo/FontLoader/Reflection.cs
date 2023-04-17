// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace FontLoader;

public class Reflection
{
    public Reflection()
    {
        AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
    }

    private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
    {
        if (args.Name.Contains("System.Web.Helpers"))
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Test.System.Web.Helpers.dll"))
            {
                byte[] assemblyData = new byte[stream.Length];
                stream.Read(assemblyData, 0, assemblyData.Length);
                return Assembly.Load(assemblyData);
            }
        }

        return null;
    }

    public string ShowMethods()
    {
        var outMsg = new StringBuilder();
        foreach (var method in typeof(Reflection).GetMethods())
        {
            if (method.IsPublic)
            {
                var parameters = method.GetParameters();
                var parameterDescriptions = string.Join
                (", ", method.GetParameters()
                    .Select(x => x.ParameterType + " " + x.Name)
                    .ToArray());
                outMsg.AppendLine($"{method.ReturnType} {method.Name} ({parameterDescriptions})");
            }
        }
        return outMsg.ToString();
    }

    public string ShowProperties()
    {
        var outMsg = new StringBuilder();
        foreach (var property in typeof(Reflection).GetProperties())
        {
            var r = property.CanRead ? "get;" : "";
            var w = property.CanWrite ? "set;" : "";
            outMsg.AppendLine($"{property.PropertyType} {property.Name} ({r} {w})");
        }
        return outMsg.ToString();
    }
}