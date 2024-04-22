// using Ws.Domain.Models.Entities.Diag;
//
// namespace Ws.Database.Nhibernate.Entities.Diag.LogWebs;
//
// public sealed class SqlLogWebValidator : SqlTableValidator<LogWebEntity>
// {
//     public SqlLogWebValidator(bool isCheckIdentity) : base(isCheckIdentity)
//     {
//         RuleFor(item => item.StampDt)
//             .NotEmpty()
//             .NotNull()
//             .GreaterThanOrEqualTo(new DateTime(2020, 01, 01));
//         RuleFor(item => item.Version)
//             .NotNull();
//         RuleFor(item => item.Url)
//             .NotEmpty()
//             .NotNull();
//         RuleFor(item => item.DataRequest)
//             .NotNull();
//         RuleFor(item => item.DataResponse)
//             .NotNull();
//         RuleFor(item => item.CountAll)
//             .NotNull()
//             .GreaterThanOrEqualTo(0);
//         RuleFor(item => item.CountSuccess)
//             .NotNull()
//             .GreaterThanOrEqualTo(0);
//         RuleFor(item => item.CountErrors)
//             .NotNull()
//             .GreaterThanOrEqualTo(0);
//     }
// }