using ScalesDesktop.Models;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Entities.Scale;
using Ws.Domain.Services.Features.Line;
using Ws.Domain.Services.Features.Plu;

namespace ScalesDesktop.Services;

public class LabelContext: IDisposable
{
    private ILineService LineService { get; }
    private IPluService PluService { get; }
    private LineContext LineContext { get; }
    
    public LineEntity Line { get => LineContext.Line; }
    public PrinterEntity Printer { get => LineContext.PrinterEntity; }
    public PluEntity Plu { get; private set; } = new();
    public TemplateEntity PluTemplate { get; private set; } = new();
    public PluNestingEntity PluNesting { get; private set; } = new();
    public WeightKneadingModel KneadingModel { get; private set; } = new();
    public IEnumerable<PluEntity> PluEntities { get; private set; } = [];
    
    public event Action? OnStateChanged;
    private Timer Timer { get; set; }

    public LabelContext(ILineService lineService, IPluService pluService, LineContext lineContext)
    {
        PluService = pluService;
        LineService = lineService;
        LineContext = lineContext;
        
        LineContext.OnLineChanged += InitializeData;
        Timer = new(_ => LineContext.RequestScale(), null, TimeSpan.Zero, TimeSpan.FromSeconds(0.5));
    }

    public void InitializeData()
    {
        PluEntities = LineService.GetLineWeightPlus(Line);
        
        Plu = new();
        PluNesting = new();
        PluTemplate = new();
        KneadingModel = new();
        
        OnStateChanged?.Invoke();
    }
    
    public void ChangePlu(PluEntity sqlPluEntity)
    {
        if (Plu.Equals(sqlPluEntity)) return;
        Plu = sqlPluEntity;
        PluTemplate = PluService.GetPluTemplate(Plu);
        IEnumerable<PluNestingEntity> pluNestingEntities = PluService.GetAllPluNestings(Plu);
        PluNesting = pluNestingEntities.FirstOrDefault(item => item.IsDefault) ?? new();
        KneadingModel.KneadingCount = 1;
        if (Plu.IsCheckWeight)
            LineContext.ConnectScale();
        else
            LineContext.DisconnectScale();
        OnStateChanged?.Invoke();
    }

    public void Dispose()
    {
        LineContext.OnLineChanged -= InitializeData;
        GC.SuppressFinalize(this);
    }
}