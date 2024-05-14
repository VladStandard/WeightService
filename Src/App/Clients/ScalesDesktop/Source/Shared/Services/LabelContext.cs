using ScalesDesktop.Source.Shared.Models;
using Ws.Domain.Models.Entities.Devices;
using Ws.Domain.Models.Entities.Devices.Arms;
using Ws.Domain.Models.Entities.Ref1c.Plu;
using Ws.Domain.Services.Features.Line;

namespace ScalesDesktop.Source.Shared.Services;

public class LabelContext : IDisposable
{
    private ILineService LineService { get; }
    private LineContext LineContext { get; }

    public Arm Line => LineContext.Line;
    public Printer Printer => LineContext.Printer;
    public PluEntity Plu { get; private set; } = new();
    public WeightKneadingModel KneadingModel { get; private set; } = new();
    public IEnumerable<PluEntity> PluEntities { get; private set; } = [];

    public event Action? OnStateChanged;

    public LabelContext(ILineService lineService, LineContext lineContext)
    {
        LineService = lineService;
        LineContext = lineContext;
        LineContext.OnLineChanged += InitializeData;
    }

    public void InitializeData()
    {
        PluEntities = Line.IsExists ? LineService.GetLineWeightPlus(Line) : [];
        Plu = new();
        KneadingModel = new();
        OnStateChanged?.Invoke();
    }

    public void ChangePlu(PluEntity sqlPluEntity)
    {
        // TODO: set default nesting
        if (Plu.Equals(sqlPluEntity)) return;
        Plu = sqlPluEntity;
        KneadingModel.KneadingCount = 1;
        OnStateChanged?.Invoke();
    }

    public void Dispose()
    {
        LineContext.OnLineChanged -= InitializeData;
        GC.SuppressFinalize(this);
    }
}