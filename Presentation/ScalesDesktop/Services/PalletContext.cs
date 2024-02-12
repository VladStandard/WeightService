using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.Pallet;

namespace ScalesDesktop.Services;

public class PalletContext : IDisposable
{
    private LineContext LineContext { get; }
    private IPalletService PalletService { get; }
    
    public LineEntity Line { get => LineContext.Line; }
    public PrinterEntity Printer { get => LineContext.PrinterEntity; }

    public ViewPallet CurrentPallet { get; private set; } = new();
    public IEnumerable<ViewPallet> PalletEntities { get; private set; } = [];
    public PalletManEntity PalletMan { get; private set; } = new();

    public event Action? OnStateChanged;

    public PalletContext(LineContext lineContext, IPalletService palletService)
    {
        PalletService = palletService;
        LineContext = lineContext;
        LineContext.OnLineChanged += InitializeData;
    }

    public void InitializeData()
    {
        CurrentPallet = new();
        PalletMan = new();
        PalletEntities = GetPallets();
        OnStateChanged?.Invoke();
    }

    public void SetPalletMan(PalletManEntity palletManEntity)
    {
        PalletMan.IdentityValueUid = Guid.NewGuid();
        OnStateChanged?.Invoke();
    }
    
    public void ResetContext()
    {
        CurrentPallet = new();
        PalletEntities = GetPallets();
        OnStateChanged?.Invoke();
    }

    public void ResetPalletMan()
    {
        PalletMan = new();
        OnStateChanged?.Invoke();
    }

    private IEnumerable<ViewPallet> GetPallets() => new PalletService().GetAllViewByWarehouse(Line.Warehouse);

    public void ChangePallet(ViewPallet palletView)
    {
        if (CurrentPallet.Equals(palletView)) return;
        CurrentPallet = palletView;
        OnStateChanged?.Invoke();
    }

    public void Dispose()
    {
        LineContext.OnLineChanged -= InitializeData;
        GC.SuppressFinalize(this);
    }
}