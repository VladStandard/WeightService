using ScalesDesktop.Source.Shared.Models;
using Ws.Domain.Models.Entities.Devices;
using Ws.Domain.Models.Entities.Devices.Arms;
using Ws.Domain.Models.Entities.Ref1c.Plu;
using Ws.Domain.Services.Features.Arms;

namespace ScalesDesktop.Source.Shared.Services;

public class LabelContext : IDisposable
{
    private IArmService ArmService { get; }
    private LineContext LineContext { get; }

    public Arm Line => LineContext.Line;
    public Printer Printer => LineContext.Printer;
    public Plu Plu { get; private set; } = new();
    public WeightKneadingModel KneadingModel { get; private set; } = new();
    public IEnumerable<Plu> PluEntities { get; private set; } = [];

    public event Action? OnStateChanged;

    public LabelContext(IArmService armService, LineContext lineContext)
    {
        ArmService = armService;
        LineContext = lineContext;
        LineContext.OnLineChanged += InitializeData;
    }

    public void InitializeData()
    {
        PluEntities = Line.IsExists ? ArmService.GetLineWeightPlus(Line) : [];
        Plu = new();
        KneadingModel = new();
        OnStateChanged?.Invoke();
    }

    public void ChangePlu(Plu sqlPlu)
    {
        // TODO: set default nesting
        if (Plu.Equals(sqlPlu)) return;
        Plu = sqlPlu;
        KneadingModel.KneadingCount = 1;
        OnStateChanged?.Invoke();
    }

    public void Dispose()
    {
        LineContext.OnLineChanged -= InitializeData;
        GC.SuppressFinalize(this);
    }
}