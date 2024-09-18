using Ws.DeviceControl.Models.Features.References.Template.Queries;
using Ws.Labels.Service.Generate.Common;

namespace Ws.DeviceControl.Api.App.Shared.Utils;

public static class BarcodeUtils
{
    #region Private

    private class BarcodeLabelBaseTemp : IBarcodeLabel
    {
        public int LineNumber { get; init; }
        public int LineCounter { get; init; }
        public short Kneading { get; init; }
        public short PluNumber { get; init; }
        public string PluEan13 { get; init; } = null!;
        public string PluGtin { get; init; } = null!;
        public DateTime ProductDt { get; init; }
        public DateTime ExpirationDt { get; init; }
        public short BundleCount { get; init; }
        public decimal WeightNet { get; init; }
        public int ExpirationDay { get; init; }
    }

    private static BarcodeVarDto Build(Expression<Func<object>> expr, short length, bool isRepeatable = false)
    {
        Type type;
        string name;

        switch (expr.Body)
        {
            case MemberExpression memberExpression:
                type = memberExpression.Type;
                name = memberExpression.Member.Name;
                break;
            case UnaryExpression { Operand: MemberExpression nestedMemberExpr }:
                type = nestedMemberExpr.Type;
                name = nestedMemberExpr.Member.Name;
                break;
            default:
                throw new ArgumentException("Expression must be a member expression.");
        }
        return new()
        {
            Type = type,
            Name = name,
            Length = length,
            IsRepeatable = isRepeatable
        };
    }

    #endregion

    public static List<BarcodeVarDto> GetVariables()
    {
        BarcodeLabelBaseTemp data = new();
        return
        [
            Build(() => data.LineNumber, 5),
            Build(() => data.LineCounter, 6),
            Build(() => data.PluNumber, 3),
            Build(() => data.PluGtin, 14),
            Build(() => data.PluEan13, 13),
            Build(() => data.Kneading, 3),
            Build(() => data.ProductDt, 0, true),
            Build(() => data.ExpirationDt, 0, true),
            Build(() => data.WeightNet, 5),
            Build(() => data.BundleCount, 2),
            Build(() => data.ExpirationDay, 3)
        ];
    }
}