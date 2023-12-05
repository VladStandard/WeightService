using FluentValidation.Results;
using Ws.Labels;
using Ws.Labels.Dto;
using Ws.Services.Dto;
using Ws.Services.Exceptions;
using Ws.Services.Validators;

namespace Ws.Services.Services.PrintLabel;

public class PrintLabelService : IPrintLabelService
{
    public string GenerateLabel(LabelInfoDto labelInfo)
    {
        LabelInfoValidator validator = new();
        ValidationResult result = validator.Validate(labelInfo);
        if (!result.IsValid) throw new LabelException(result);
        LabelDto label = new LabelGenerator().GenerateLabel(labelInfo);
        return label.Context;
    }
}