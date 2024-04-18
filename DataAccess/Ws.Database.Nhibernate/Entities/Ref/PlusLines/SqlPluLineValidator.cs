// using Ws.Database.Core.Entities.Ref.Lines;
// using Ws.Database.Core.Entities.Ref1c.Plus;
// using Ws.Domain.Models.Entities.Ref;
//
// namespace Ws.Database.Core.Entities.Ref.PlusLines;
//
// public sealed class SqlPluLineValidator : SqlTableValidator<PluLineEntity>
// {
//     public SqlPluLineValidator(bool isCheckIdentity) : base(isCheckIdentity)
//     {
//         RuleFor(item => item.Plu)
//             .NotEmpty()
//             .NotNull()
//             .SetValidator(new SqlPluValidator(isCheckIdentity));
//         RuleFor(item => item.Line)
//             .NotEmpty()
//             .NotNull()
//             .SetValidator(new SqlLineValidator(isCheckIdentity));
//     }
// }