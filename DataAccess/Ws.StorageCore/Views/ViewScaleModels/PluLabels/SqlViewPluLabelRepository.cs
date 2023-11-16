namespace Ws.StorageCore.Views.ViewScaleModels.PluLabels;

public class SqlViewPluLabelRepository : IViewPluLabelRepository
{
    private SqlCoreHelper SqlCore => SqlCoreHelper.Instance;
    
    public IList<SqlViewPluLabelModel> GetList(SqlCrudConfigModel sqlCrudConfig)
    {
        IList<SqlViewPluLabelModel> result = new List<SqlViewPluLabelModel>();
        string query = SqlQueriesDiags.Views.GetPluLabels(sqlCrudConfig.SelectTopRowsCount, sqlCrudConfig.IsMarked);
        object[] objects = SqlCore.GetArrayObjects(query);
        foreach (object obj in objects)
        {
            int i = 0;
            if (obj is not object[] item || item.Length < 9 || !Guid.TryParse(Convert.ToString(item[i++]), out var uid)) break;
            result.Add(new()
            {
                IdentityValueUid = uid,
                CreateDt = Convert.ToDateTime(item[i++]),
                IsMarked = Convert.ToBoolean(item[i++]),
                ProductDate = Convert.ToDateTime(item[i++]),
                WeightingDate = Convert.ToDateTime(item[i++]),
                Line = item[i++] as string ?? string.Empty,
                PluName = item[i++] as string ?? string.Empty,
                PluNumber = Convert.ToInt32(item[i++]),
                WorkShop = item[i] as string ?? string.Empty
            });
        }
        return result;
    }
}