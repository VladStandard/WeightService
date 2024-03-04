using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Entities.Scale;

namespace ScalesDesktop.Source.Shared.Utils;

public static class NameFormatting
{
    public static string GetPluNestingFormattedName(PluNestingEntity pluNestingEntity)
    {
        BoxEntity box = pluNestingEntity.Box;
        BundleEntity bundle = pluNestingEntity.Plu.Bundle;
        short bundleCount = pluNestingEntity.BundleCount;
        return string.Join(" | ", new List<string> { GetFormatBoxName(box), GetFormatBundleName(bundle, bundleCount) });
    }

    public static string GetFormatBoxName(BoxEntity box)
    {
        if (box.Uid1C == Guid.Empty) return string.Empty;
        string[] boxNameWords = box.Name.Split(" ");
        return $"{string.Join(" ", boxNameWords.Take(2))} {box.Weight}кг";
    }

    public static string GetFormatBundleName(BundleEntity bundle, short bundleCount)
    {
        if (bundle.Uid1C == Guid.Empty) return string.Empty;
        string firstBundleWord = bundle.Name.Split(" ").FirstOrDefault() ?? string.Empty;
        return $"{firstBundleWord} {bundle.Weight}кг * {bundleCount}шт";
    }
}