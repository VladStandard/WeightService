using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Ws.Labels.Service.Extensions;

internal static class StringBuilderExtensions
{
    internal static void AppendStrWithPadding(this StringBuilder sb, string? value, int totalLength)
    {
        ArgumentNullException.ThrowIfNull(value);

        if (value.Length > totalLength || value.IsNullOrEmpty())
            throw new ArgumentException("Value length exceeds total length.", nameof(value));

        int zeroCount = totalLength - value.Length;

        if (zeroCount > 0)
        {
            sb.EnsureCapacity(totalLength);
            sb.Append('0', zeroCount);
        }
        sb.Append(value);
    }
}