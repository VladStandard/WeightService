using ScalesDesktop.Source.Shared.Api;
using ScalesDesktop.Source.Shared.Models;
using Ws.Desktop.Models.Features.Plus.Output;

namespace ScalesDesktop.Source.Shared.Services;

public class LabelContext : IDisposable
{
    private LineContext LineContext { get; }
    private IDesktopApi DesktopApi { get; }

    public PluWeight? Plu { get; private set; }
    public WeightKneadingModel KneadingModel { get; private set; } = new();
    public IEnumerable<PluWeight> PluEntities { get; private set; } = [];
    public event Action? StateChanged;

    public LabelContext(LineContext lineContext, IDesktopApi desktopApi)
    {
        DesktopApi = desktopApi;
        LineContext = lineContext;
        LineContext.LineChanged += OnLineChanged;
    }

    private async void OnLineChanged() => await InitializeData();

    public async Task InitializeData()
    {
        PluEntities = LineContext.Line != null ? await GetWeightPlu(LineContext.Line.Id) : [];
        Plu = null;
        KneadingModel = new();
        StateChanged?.Invoke();
    }

    public void ChangePlu(PluWeight newPlu)
    {
        if (Plu != null && Plu.Equals(newPlu)) return;
        Plu = newPlu;
        KneadingModel.KneadingCount = 1;
        StateChanged?.Invoke();
    }

    private async Task<PluWeight[]> GetWeightPlu(Guid armUid)
    {
        try
        {
            return await DesktopApi.GetPlusByArm(armUid);
        }
        catch
        {
            return [];
        }
    }

    public void Dispose()
    {
        LineContext.LineChanged -= OnLineChanged;
        GC.SuppressFinalize(this);
    }
}