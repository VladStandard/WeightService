// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.Models;

public class SqlCrudConfigModel
{
    #region Public and private fields, properties, constructor

    public List<SqlFieldFilterModel> Filters { get; set; }
    public List<SqlFieldOrderModel> Orders { get; set; }
    public int MaxResults { get; set; }

    public SqlCrudConfigModel(List<SqlFieldFilterModel> filters, List<SqlFieldOrderModel> orders, int maxResults)
    {
	    Filters = filters;
        Orders = orders;
        MaxResults = maxResults;
    }

    public SqlCrudConfigModel(SqlFieldFilterModel filter, List<SqlFieldOrderModel> orders, int maxResults)
    {
	    Filters = new() { filter };
        Orders = orders;
        MaxResults = maxResults;
    }

    public SqlCrudConfigModel(List<SqlFieldFilterModel> filters, SqlFieldOrderModel order, int maxResults)
    {
	    Filters = filters;
        Orders = new() { order };
        MaxResults = maxResults;
    }

    public SqlCrudConfigModel(List<SqlFieldFilterModel> filters)
    {
	    Filters = filters;
        Orders = new();
        MaxResults = 0;
    }

    public SqlCrudConfigModel(SqlFieldFilterModel filter)
    {
        Filters = new() { filter };
        Orders = new();
        MaxResults = 0;
    }

    public SqlCrudConfigModel(SqlFieldFilterModel filter, SqlFieldOrderModel order, int maxResults)
    {
	    Filters = new() { filter };
        Orders = new() { order };
        MaxResults = maxResults;
    }

    #endregion
}
