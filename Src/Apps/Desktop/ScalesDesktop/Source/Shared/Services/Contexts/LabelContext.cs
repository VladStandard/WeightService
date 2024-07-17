using Ws.Desktop.Models.Features.Plus.Weight.Output;

namespace ScalesDesktop.Source.Shared.Services.Contexts;

public class LabelContext
{
    public PluWeight? Plu { get; private set; }
    public WeightKneadingModel KneadingModel { get; private set; } = new();
    public event Action? StateChanged;

    public void ResetData()
    {
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
}