namespace Ws.StorageCore.Views.ViewRefModels.PluNestings;

public class SqlViewPluNestingRepository : IViewPluNestingRepository
{
    private SqlCoreHelper SqlCore => SqlCoreHelper.Instance;
    
    public IEnumerable<SqlViewPluNestingModel> GetEnumerable(ushort pluNumber = 0)
    {
        List<SqlViewPluNestingModel> result = new();
        
        string query = SqlQueriesDiags.Views.GetViewPlusNesting32(pluNumber);
        object[] objects = SqlCore.GetArrayObjects(query);
        foreach (object obj in objects)
        {
            if (obj is not object[] item || item.Length < 31) break;
            int i = 0;
            result.Add(new(Guid.Parse(Convert.ToString(item[i++])),
                Convert.ToBoolean(item[i++]), Convert.ToBoolean(item[i++]),
                Convert.ToInt16(item[i++]), Convert.ToDecimal(item[i++]), Convert.ToDecimal(item[i++]), 
                Convert.ToDecimal(item[i++]), Guid.Parse(Convert.ToString(item[i++])), 
                Guid.Parse(Convert.ToString(item[i++])), Convert.ToBoolean(item[i++]), 
                Convert.ToBoolean(item[i++]), Convert.ToBoolean(item[i++]), 
                Convert.ToUInt16(item[i++]), Convert.ToString(item[i++]), Convert.ToInt16(item[i++]), 
                Convert.ToString(item[i++]), Convert.ToString(item[i++]), Convert.ToString(item[i++]), 
                Guid.Parse(Convert.ToString(item[i++])), Guid.Parse(Convert.ToString(item[i++])), 
                Convert.ToBoolean(item[i++]), Convert.ToString(item[i++]), Convert.ToDecimal(item[i++]),
                Guid.Parse(Convert.ToString(item[i++])), Guid.Parse(Convert.ToString(item[i++])), 
                Convert.ToBoolean(item[i++]), Convert.ToString(item[i++]), Convert.ToDecimal(item[i++]), 
                Convert.ToDecimal(item[i++]), Convert.ToDateTime(item[i++]),
                Convert.ToBoolean(item[i++]), Convert.ToString(item[i++])));
        }

        if (!result.Any())
        {
            query = SqlQueriesDiags.Views.GetViewPlusNesting29(pluNumber);
            objects = SqlCore.GetArrayObjects(query);
            foreach (object obj in objects)
            {
                if (obj is not object[] item || item.Length < 28) break;
                int i = 0;
                result.Add(new(Guid.Parse(Convert.ToString(item[i++])),
                    Convert.ToBoolean(item[i++]), Convert.ToBoolean(item[i++]),
                    Convert.ToInt16(item[i++]), Convert.ToDecimal(item[i++]), Convert.ToDecimal(item[i++]),
                    Convert.ToDecimal(item[i++]), Guid.Parse(Convert.ToString(item[i++])),
                    Guid.Parse(Convert.ToString(item[i++])), Convert.ToBoolean(item[i++]),
                    Convert.ToBoolean(item[i++]), Convert.ToBoolean(item[i++]),
                    Convert.ToUInt16(item[i++]), Convert.ToString(item[i++]), Convert.ToInt16(item[i++]),
                    Convert.ToString(item[i++]), Convert.ToString(item[i++]), Convert.ToString(item[i++]),
                    Guid.Parse(Convert.ToString(item[i++])), Guid.Parse(Convert.ToString(item[i++])),
                    Convert.ToBoolean(item[i++]), Convert.ToString(item[i++]), Convert.ToDecimal(item[i++]),
                    Guid.Parse(Convert.ToString(item[i++])), Guid.Parse(Convert.ToString(item[i++])),
                    Convert.ToBoolean(item[i++]), Convert.ToString(item[i++]), Convert.ToDecimal(item[i++]),
                    Convert.ToDecimal(item[i++]), DateTime.MinValue, false, string.Empty));
            }
        }

        return result;
    }
}