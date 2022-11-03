// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.Models;

public class SqlCrudConfigModel
{
    #region Public and private fields, properties, constructor

    public List<SqlFieldFilterModel> Filters { get; set; } = new();
	public List<SqlFieldOrderModel> Orders { get; set; } = new();
	public bool IsShowFilterAdditional { get; set; }
	public bool IsShowFilterMarked { get; set; }
	public bool IsShowFilterOnlyTop { get; set; }
	public bool IsShowMarked { get; set; }
	public bool IsShowOnlyTop { get; set; }
	public bool IsAddFieldNull { get; set; }
	public bool IsOrder { get; set; }
	public bool IsShowItemsCount { get; set; }
	public int MaxResults { get; set; }

    public SqlCrudConfigModel(List<SqlFieldFilterModel> filters, List<SqlFieldOrderModel> orders, 
	    bool isShowMarked, bool isShowOnlyTop, bool isAddFieldNull, bool isOrder, int maxResults)
    {
        Filters = filters;
        Orders = orders;
        IsShowMarked = isShowMarked;
        IsShowOnlyTop = isShowOnlyTop;
        IsAddFieldNull = isAddFieldNull;
        IsOrder = isOrder;
		MaxResults = maxResults;

		IsShowFilterOnlyTop = true;
		IsShowOnlyTop = true;
		IsShowItemsCount = false;
	}

    #endregion
}
