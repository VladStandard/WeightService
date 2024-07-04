using Ws.Domain.Models.Entities.Ref1c.Plus;

namespace DeviceControl.Source.Pages.References1C.Plus;

public class PluValidValidator : AbstractValidator<Plu>
{
    public PluValidValidator()
    {
        RuleFor(item => item.PluNesting).Custom((obj, context) =>
        {
            if (obj.IsNew)
                context.AddFailure("Вложенность по умолчанию отсутствует");
        });

        RuleFor(item => item.Characteristics).Custom((obj, context) =>
        {
            if (obj.Count > 0)
                context.AddFailure("У весовой ПЛУ - присутствуют характеристики");
        }).When(i => i.IsCheckWeight);

        RuleFor(item => item.TemplateUid)
            .NotNull()
            .NotEqual(Guid.Empty).WithMessage("Шаблон не задан");

        RuleFor(item => item.StorageMethod)
            .Must(value => value is "Замороженное" or "Охлаждённое")
            .WithMessage("Способ хранения - должен быть ['Замороженное', 'Охлаждённое']");
    }
}