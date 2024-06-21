using Ws.Domain.Models.ValueTypes;
using Ws.Labels.Service.Generate.Models;
using Ws.Shared.Extensions;

namespace DeviceControl.Source.Features.BarcodeConfigurator;

public class BarcodeItemValidator : AbstractValidator<BarcodeItem>
{
    private readonly IList<BarcodeVariable> _barcodeVariables;

    public BarcodeItemValidator(IList<BarcodeVariable> barcodeVariables)
    {
        _barcodeVariables = barcodeVariables;
        RuleFor(x => x.Property).NotEmpty();
        RuleFor(x => x).Must(FormatStrIsCorrectIfDateTime).WithMessage("Тип с датой должен содержать корректную маску");
    }

    private bool FormatStrIsCorrectIfDateTime(BarcodeItem item)
    {
        BarcodeVariable? barcodeVariable = _barcodeVariables.FirstOrDefault(x => x.Name == item.Property);
        return barcodeVariable == null || barcodeVariable.Type != typeof(DateTime) || item.FormatStr.IsDateFormat();
    }
}