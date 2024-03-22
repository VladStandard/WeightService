// using Ws.Database.Nhibernate.Entities.Ref.Templates;
// using Ws.Database.Nhibernate.Entities.Ref1c.Plus;
// using Ws.Domain.Models.Entities.Scale;
//
// namespace Ws.Database.Nhibernate.Entities.Scales.PlusTemplatesFks;
//
// public sealed class SqlPluTemplateFkValidator : SqlTableValidator<PluTemplateFkEntity>
// {
//
//     public SqlPluTemplateFkValidator(bool isCheckIdentity) : base(isCheckIdentity)
//     {
//         RuleFor(item => item.Plu)
//             .NotEmpty()
//             .NotNull()
//             .SetValidator(new SqlPluValidator(isCheckIdentity));
//         RuleFor(item => item.Template)
//             .NotEmpty()
//             .NotNull()
//             .SetValidator(new SqlTemplateValidator(isCheckIdentity));
//     }
// }