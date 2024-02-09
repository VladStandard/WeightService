using ScalesDesktop.Features.Pallet;
using Ws.Domain.Models.Entities.Ref;

namespace ScalesDesktop.Services;

public class PalletContext : IDisposable
{
    private LineContext LineContext { get; }

    public LineEntity Line { get => LineContext.Line; }
    public PrinterEntity Printer { get => LineContext.PrinterEntity; }

    public PalletModel Pallet { get; private set; } = new();
    public IEnumerable<PalletModel> PalletEntities { get; private set; } = [];

    public event Action? OnStateChanged;

    public PalletContext(LineContext lineContext)
    {
        LineContext = lineContext;
        LineContext.OnLineChanged += InitializeData;
    }

    public void InitializeData()
    {
        Pallet = new();
        PalletEntities = GetPallets();
        OnStateChanged?.Invoke();
    }

    private static IEnumerable<PalletModel> GetPallets() =>
    [
        new()
        {
            Uid = Guid.NewGuid(), Number = 9919998, Labels =
            [
                new() { IdentityValueUid = Guid.NewGuid() },
                new() { IdentityValueUid = Guid.NewGuid() },
                new() { IdentityValueUid = Guid.NewGuid() },
                new() { IdentityValueUid = Guid.NewGuid() },
                new() { IdentityValueUid = Guid.NewGuid() },
                new() { IdentityValueUid = Guid.NewGuid() },
                new() { IdentityValueUid = Guid.NewGuid() },
                new() { IdentityValueUid = Guid.NewGuid() },
                new() { IdentityValueUid = Guid.NewGuid() },
                new() { IdentityValueUid = Guid.NewGuid() },
                new() { IdentityValueUid = Guid.NewGuid() },
                new() { IdentityValueUid = Guid.NewGuid() },
                new() { IdentityValueUid = Guid.NewGuid() },
                new() { IdentityValueUid = Guid.NewGuid() },
                new() { IdentityValueUid = Guid.NewGuid() },
                new() { IdentityValueUid = Guid.NewGuid() }

            ]
        },
        new() { Uid = Guid.NewGuid(), Number = 9929999, Labels = [new() { IdentityValueUid = Guid.NewGuid() }] },
        new()
        {
            Uid = Guid.NewGuid(), Number = 9119998,
            Labels = [new() { IdentityValueUid = Guid.NewGuid() }, new() { IdentityValueUid = Guid.NewGuid() }]
        },
        new() { Uid = Guid.NewGuid(), Number = 9229999, Labels = [new() { IdentityValueUid = Guid.NewGuid() }] },
        new()
        {
            Uid = Guid.NewGuid(), Number = 9319998,
            Labels = [new() { IdentityValueUid = Guid.NewGuid() }, new() { IdentityValueUid = Guid.NewGuid() }]
        },
        new() { Uid = Guid.NewGuid(), Number = 9429999, Labels = [new() { IdentityValueUid = Guid.NewGuid() }] },
        new()
        {
            Uid = Guid.NewGuid(), Number = 9519698,
            Labels = [new() { IdentityValueUid = Guid.NewGuid() }, new() { IdentityValueUid = Guid.NewGuid() }]
        },
        new() { Uid = Guid.NewGuid(), Number = 9629999, Labels = [new() { IdentityValueUid = Guid.NewGuid() }] },
        new()
        {
            Uid = Guid.NewGuid(), Number = 9719998,
            Labels = [new() { IdentityValueUid = Guid.NewGuid() }, new() { IdentityValueUid = Guid.NewGuid() }]
        },
        new() { Uid = Guid.NewGuid(), Number = 9829999, Labels = [new() { IdentityValueUid = Guid.NewGuid() }] },
        new()
        {
            Uid = Guid.NewGuid(), Number = 9979998,
            Labels = [new() { IdentityValueUid = Guid.NewGuid() }, new() { IdentityValueUid = Guid.NewGuid() }]
        },
        new() { Uid = Guid.NewGuid(), Number = 9959999, Labels = [new() { IdentityValueUid = Guid.NewGuid() }] }
    ];

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