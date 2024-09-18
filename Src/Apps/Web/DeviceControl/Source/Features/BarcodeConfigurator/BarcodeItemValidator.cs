using Ws.DeviceControl.Models.Features.References.Template.Queries;
using Ws.Shared.Extensions;

namespace DeviceControl.Source.Features.BarcodeConfigurator;

public class BarcodeItemValidator : AbstractValidator<ExtendedBarcodeItemDto>
{
    private readonly IList<BarcodeVarDto> _barcodeVariables;

    public BarcodeItemValidator(IList<BarcodeVarDto> barcodeVariables)
    {
        _barcodeVariables = barcodeVariables;
        RuleFor(x => x.Property).NotEmpty();
        RuleFor(x => x).Must(FormatStrIsCorrectIfDateTime).WithMessage("Тип с датой должен содержать корректную маску");
    }

    private bool FormatStrIsCorrectIfDateTime(ExtendedBarcodeItemDto item)
    {
        BarcodeVarDto? barcodeVariable = _barcodeVariables.FirstOrDefault(x => x.Name == item.Property);
        return barcodeVariable == null || barcodeVariable.Type != typeof(DateTime) || item.FormatStr.IsDateFormat();
    }
}