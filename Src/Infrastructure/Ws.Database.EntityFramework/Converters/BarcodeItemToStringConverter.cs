using System.Text.Json;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Ws.Shared.ValueTypes;

// ReSharper disable ConvertToPrimaryConstructor
// ReSharper disable MemberCanBePrivate.Global

namespace Ws.Database.EntityFramework.Converters;

public class BarcodeItemListComparer : ValueComparer<List<BarcodeItem>>
{
    public BarcodeItemListComparer()
        : base(
        (c1, c2) => SequenceEqual(c1, c2),
        c => c.Aggregate(0, HashCode.Combine),
        c => new(c)
        )
    { }

    private static bool SequenceEqual(List<BarcodeItem>? c1, List<BarcodeItem>? c2)
    {
        if (c1 == null || c2 == null) return c1 == c2;
        return c1.Count == c2.Count && !c1.Where((t, i) => !t.Equals(c2[i])).Any();
    }
}

public class BarcodeItemListConverter : ValueConverter<List<BarcodeItem>, string?>
{
    private static readonly ConverterMappingHints DefaultHints = new(size: 2048);

    public BarcodeItemListConverter(ConverterMappingHints? mappingHints)
        : base(
        list => SerializeJson(list),
        json => DeserializeJson(json),
        DefaultHints.With(mappingHints)
        ) {}

    public BarcodeItemListConverter() : this(DefaultHints) {}

    private static List<BarcodeItem> DeserializeJson(string? json)
    {
        if (string.IsNullOrEmpty(json)) return [];
        return JsonSerializer.Deserialize<List<BarcodeItem>>(json) ?? [];
    }

    private static string SerializeJson(List<BarcodeItem> list) => JsonSerializer.Serialize(list);
}