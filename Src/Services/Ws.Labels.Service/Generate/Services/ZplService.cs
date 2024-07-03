using System.Text.RegularExpressions;
using Scriban;
using Scriban.Runtime;
using Ws.Labels.Service.Generate.Exceptions.LabelGenerate;
using Ws.Labels.Service.Generate.Models;

namespace Ws.Labels.Service.Generate.Services;

public partial class ZplService(CacheService cacheService)
{
    private Dictionary<string, string> _cachedResourcesVars = new();

    [GeneratedRegex(@"\{\*(.*?)\*\}")]
    private static partial Regex CommentsRegex();

    [GeneratedRegex(@"\{\{ ([a-zA-Z0-9_]+)_sql \}\}")]
    private static partial Regex ResourcesRegex();

    public string GenerateZpl(string template, TemplateVariables model)
    {
        try
        {
            if (_cachedResourcesVars.Count <= 0)
            {
                List<string> foundResourcesVariables = ResourcesRegex()
                    .Matches(template)
                    .Select(match => match.Groups[1].Value)
                    .ToList();

                _cachedResourcesVars = cacheService.GetResourcesFromCacheOrDb(foundResourcesVariables);
            }

            TemplateContext context = new() { StrictVariables = true };

            ScriptObject scriptObject1 = new();
            scriptObject1.Import(_cachedResourcesVars);
            scriptObject1.Import(model);

            context.PushGlobal(scriptObject1);

            Template? labelTemp = Template.Parse(template);

            string zpl = labelTemp.Render(context);

            zpl = CommentsRegex().Replace(zpl, m =>
            {
                string match = m.Groups[1].Value.Trim();
                return $"^FX {match}";
            });

            return zpl;
        }
        catch
        {
            throw new LabelGenerateException(LabelGenExceptions.BarcodeVarNotFound);
        }
    }
}