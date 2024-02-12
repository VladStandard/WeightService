using ScalesDesktop.Features.Pallet;
using Ws.Database.Core.Entities.Print.Pallets;
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

    public PalletModel Pallet { get; private set; } = new();
    public IEnumerable<PalletModel> PalletEntities { get; private set; } = [];
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
        Pallet = new();
        PalletMan = new();
        PalletEntities = GetPallets();
        OnStateChanged?.Invoke();
    }

    public void SetPalletMan(PalletManEntity palletManEntity)
    {
        PalletMan.Identity.Uid = Guid.NewGuid();
        PalletMan.IdentityValueUid = Guid.NewGuid();
        PalletMan.IdentityValueId = long.MaxValue;
        PalletMan.Identity.Uid = Guid.NewGuid();
        PalletMan.Uid1C = Guid.NewGuid();
        OnStateChanged?.Invoke();
    }

    private IEnumerable<PalletModel> GetPallets()
    {
        List<PalletModel> pallets = [];
        IEnumerable<ViewPallet> palletsView = new PalletService().GetAllViewByWarehouse(Line.Warehouse);
        
        pallets.AddRange(palletsView.Select(pallet => new PalletModel()
        {
            CreateDt = pallet.CreateDt,
            Number = pallet.Counter,
            Uid = pallet.IdentityValueUid
        }));
        
        return pallets;
    }

    public void ChangePallet(PalletModel palletModel)
    {
        if (Pallet.Equals(palletModel)) return;
        Pallet = palletModel;
        OnStateChanged?.Invoke();
    }

    public void Dispose()
    {
        LineContext.OnLineChanged -= InitializeData;
        GC.SuppressFinalize(this);
    }
}