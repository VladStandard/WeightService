using FluentValidation;
using Ws.Domain.Models.Entities.Ref1c;

namespace DeviceControl.Source.Pages.References1C.Plus;

public class PluValidValidator : AbstractValidator<PluEntity>
{
    public PluValidValidator()
    {
        RuleFor(item => item.Nesting).Custom((obj, context) => {
            if (obj.IsNew)
                context.AddFailure("Вложенность по умолчанию отсутствует");
        });

        RuleFor(item => item.Characteristics).Custom((obj, context) => {
            if (obj.Count > 0)
                context.AddFailure("У весовой ПЛУ - присутствуют характеристики");
        }).When(i => i.IsCheckWeight);

        RuleFor(item => item.TemplateUid)
            .NotNull()
            .NotEqual(Guid.Empty).WithMessage("Шаблон не задан");
    }
}