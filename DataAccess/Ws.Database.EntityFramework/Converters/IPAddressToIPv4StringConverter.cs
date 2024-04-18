using System.Linq.Expressions;
using System.Net;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Ws.Database.EntityFramework.Converters;

internal class IpAddressToIPv4StringConverter(ConverterMappingHints? mappingHints)
    : ValueConverter<IPAddress?, string?>(ToString(),
    ToIpAddress(),
    DefaultHints.With(mappingHints))
{
    private static readonly ConverterMappingHints DefaultHints = new(size: 15);

    public IpAddressToIPv4StringConverter()
        : this(null)
    {
    }

    private new static Expression<Func<IPAddress?, string?>> ToString()
        => v => v!.MapToIPv4().ToString();

    private static Expression<Func<string?, IPAddress?>> ToIpAddress()
        => v => v != null ? IPAddress.Parse(v).MapToIPv4() : null;
}