namespace WsStorageCore.Views.ViewRefModels.PluNestings;

public class WsSqlViewPluNestingRepository : IViewPluNestingRepository
{
    private WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;
    
    public IEnumerable<WsSqlViewPluNestingModel> GetEnumerable(ushort pluNumber = 0)
    {
        List<WsSqlViewPluNestingModel> result = new();
        
        string query = WsSqlQueriesDiags.Views.GetViewPlusNesting32(pluNumber);
        object[] objects = SqlCore.GetArrayObjects(query);
        foreach (object obj in objects)
        {
            if (obj is not object[] item || item.Length < 32) break;
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
            query = WsSqlQueriesDiags.Views.GetViewPlusNesting29(pluNumber);
            objects = SqlCore.GetArrayObjects(query);
            foreach (object obj in objects)
            {
                if (obj is not object[] item || item.Length < 29) break;
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