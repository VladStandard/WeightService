using ScalesHybrid.Models;
using Ws.Services.Services.Host;
using Ws.StorageCore.Entities.SchemaRef.Hosts;
using Ws.StorageCore.Entities.SchemaRef1c.Plus;
using Ws.StorageCore.Entities.SchemaScale.Scales;

namespace ScalesHybrid.Services;

public class LineContext
{
    public SqlHostEntity Host { get; set; }
    public SqlLineEntity Line { get; set; }
    public SqlPluEntity Plu { get; set; }
    public WeightKneadingModel KneadingModel { get; set; }
    public PluTypeEnum PluType { get; set; }
    private IHostService HostService { get; }

    public LineContext(IHostService hostService)
    {
        HostService = hostService;
        InitData();
    }

    private void InitData()
    {
        Host = HostService.GetCurrentHostOrCreate();
        Line = HostService.GetLineByHost(Host);
        Plu = new();
        PluType = PluTypeEnum.Weight;
        KneadingModel = new()
        {
            PluName = "ПЛУ (вес) | 349 | Классическая (Светофор)",
            PluNesting = "15x45",
            ProductDate = DateOnly.FromDateTime(DateTime.Today),
            KneadingCount = 30,
            NetWeight = -1.504m,
            TareWeight = 1.504m
        };
    }
}