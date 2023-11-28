using ScalesHybrid.Models;
using ScalesHybrid.Utils;
using Ws.Services.Services.Host;
using Ws.Services.Services.Line;
using Ws.Services.Services.Plu;
using Ws.StorageCore.Entities.SchemaRef.Hosts;
using Ws.StorageCore.Entities.SchemaRef1c.Plus;
using Ws.StorageCore.Entities.SchemaScale.PlusNestingFks;
using Ws.StorageCore.Entities.SchemaScale.Scales;

namespace ScalesHybrid.Services;

public class LineContext
{
    public SqlHostEntity Host { get; private set; }
    public SqlLineEntity Line { get; private set; }
    public SqlPluEntity Plu { get; private set; }
    public SqlPluNestingFkEntity PluNesting { get; set; }
    public WeightKneadingModel KneadingModel { get; set; }
    public IEnumerable<SqlLineEntity> LineEntities { get; set; }
    public IEnumerable<SqlPluEntity> PluEntities { get; set; }
    public IEnumerable<SqlPluNestingFkEntity> PluNestingEntities { get; set; }
    public event Action OnStateChanged;
    
    private IHostService HostService { get; }
    private ILineService LineService { get; }
    private IPluService PluService { get; }

    public LineContext(IHostService hostService, ILineService lineService, IPluService pluService)
    {
        HostService = hostService;
        LineService = lineService;
        PluService = pluService;
        InitData();
    }

    public async Task ChangeLine(SqlLineEntity sqlLineEntity)
    {
        if (Line.Equals(sqlLineEntity)) return;
        Line = sqlLineEntity;
        PluEntities = await Task.Run(GetPlus);
        Plu = new();
        PluNesting = new();
        NotifyStateChanged();
    }

    public async Task ChangePlu(SqlPluEntity sqlPluEntity)
    {
        if (Plu.Equals(sqlPluEntity)) return;
        Plu = sqlPluEntity;
        PluNestingEntities = await Task.Run(GetPluNestings);
        PluNesting = PluNestingEntities.FirstOrDefault(item => item.IsDefault) ?? new();
        NotifyStateChanged();
    }

    public void ChangePluNesting(SqlPluNestingFkEntity sqlPluNestingEntity)
    {
        if (PluNesting.Equals(sqlPluNestingEntity)) return;
        PluNesting = sqlPluNestingEntity;
        NameFormatting.GetPluNestingFormattedName(sqlPluNestingEntity);
        NotifyStateChanged();
    }

    private IEnumerable<SqlPluEntity> GetPlus() => LineService.GetLinePlus(Line);

    private IEnumerable<SqlPluNestingFkEntity> GetPluNestings() => PluService.GetPluNesting(Plu);

    private void InitData()
    {
        Host = HostService.GetCurrentHostOrCreate();
        Line = HostService.GetLineByHost(Host);
        LineEntities = LineService.GetLinesByWorkshop(Line.WorkShop);
        PluEntities = GetPlus();
        
        Plu = new();
        PluNesting = new();
        KneadingModel = new();
    }
    
    private void NotifyStateChanged() => OnStateChanged?.Invoke();
}