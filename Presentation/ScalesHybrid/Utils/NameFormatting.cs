using Ws.StorageCore.Entities.SchemaRef1c.Boxes;
using Ws.StorageCore.Entities.SchemaRef1c.Bundles;
using Ws.StorageCore.Entities.SchemaScale.PlusNestingFks;

namespace ScalesHybrid.Utils;

public static class NameFormatting
{
    public static string GetPluNestingFormattedName(SqlPluNestingFkEntity sqlPluNestingEntity)
    {
        SqlBoxEntity box = sqlPluNestingEntity.Box;
        SqlBundleEntity bundle = sqlPluNestingEntity.Plu.Bundle;
        short bundleCount = sqlPluNestingEntity.BundleCount;
        return string.Join(" | ", new List<string> { FormatBoxName(box), FormatBundleName(bundle, bundleCount) });
    }
    
    private static string FormatBoxName(SqlBoxEntity box)
    {
        if (box.Uid1C == Guid.Empty) return string.Empty;
        string[] boxNameWords = box.Name.Split(" ");
        return $"{string.Join(" ", boxNameWords.Take(2))} {box.Weight}г";
    }

    private static string FormatBundleName(SqlBundleEntity bundle, short bundleCount)
    {
        if (bundle.Uid1C == Guid.Empty) return string.Empty;
        string firstBundleWord = bundle.Name.Split(" ").FirstOrDefault() ?? string.Empty;
        return $"{firstBundleWord} {(int)(bundle.Weight * 1000)}x{bundleCount}";
    }
}