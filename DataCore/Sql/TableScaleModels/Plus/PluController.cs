// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Models;
using DataCore.Sql.TableScaleFkModels.PlusTemplatesFks;

namespace DataCore.Sql.TableScaleModels.Plus;

[Obsolete(@"Move it to DataContext")]
public static class PluController
{
    private static WsDataContextModel DataContext { get; } = new();

    public static bool IsFullValid(PluModel pluModel)
    {
        if (pluModel.Gtin == "" || pluModel.Ean13 == "" || pluModel.Itf14 == "")
            return false;

        List<SqlFieldFilterModel> sqlFilters = SqlCrudConfigModel.GetFiltersIdentity(nameof(PluTemplateFkModel.Plu), pluModel.IdentityValueUid);
        SqlCrudConfigModel sqlCrudConfig = new(sqlFilters, 
            true, false, false, true, false);

        List<PluTemplateFkModel> pluTemplateFks = DataContext.GetListNotNullablePlusTemplatesFks(sqlCrudConfig);
        
        return pluTemplateFks.Count != 0;
    }
}