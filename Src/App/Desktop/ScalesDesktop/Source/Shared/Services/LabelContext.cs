using ScalesDesktop.Source.Shared.Api;
using ScalesDesktop.Source.Shared.Models;
using Ws.Desktop.Models.Features.Plus.Output;

namespace ScalesDesktop.Source.Shared.Services;

public class LabelContext : IDisposable
{
    private ArmContext ArmContext { get; }
    private IDesktopApi DesktopApi { get; }

    public PluWeight? Plu { get; private set; }
    public WeightKneadingModel KneadingModel { get; private set; } = new();
    public IEnumerable<PluWeight> PluEntities { get; private set; } = [];
    public event Action? StateChanged;

    public LabelContext(ArmContext armContext, IDesktopApi desktopApi)
    {
        DesktopApi = desktopApi;
        ArmContext = armContext;
        ArmContext.ArmChanged += OnArmChanged;
    }

    private async void OnArmChanged() => await InitializeData();

    public async Task InitializeData()
    {
        PluEntities = ArmContext.Arm != null ? await GetWeightPlu(ArmContext.Arm.Id) : [];
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
        ArmContext.ArmChanged -= OnArmChanged;
        GC.SuppressFinalize(this);
    }
}