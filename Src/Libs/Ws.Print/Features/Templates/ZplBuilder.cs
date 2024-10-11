using Scriban;
using Scriban.Runtime;
using Ws.Print.Features.Templates.Models;
using Ws.Print.Features.Templates.Utils;
using Ws.Print.Features.Templates.Variables;

namespace Ws.Print.Features.Templates;

public static class ZplBuilder
{
    public static string GenerateZpl(PrintSettings settings, TemplateVars vars)
    {
        if (settings.Resources.Any(var => !var.Key.EndsWith("_sql")))
            throw new InvalidOperationException("All resource keys must end with '_sql'.");

        TemplateContext context = new() { StrictVariables = true };

        ScriptObject scriptObject1 = new();

        scriptObject1.Import(vars);
        scriptObject1.Import(settings.Resources);
        context.PushGlobal(scriptObject1);

        string zpl = Template.Parse(settings.Settings.Template).Render(context);

        return TemplateUtils.FormatComments(zpl);
    }
}