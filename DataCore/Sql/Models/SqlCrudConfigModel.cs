// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Fields;

namespace DataCore.Sql.Models;

public class SqlCrudConfigModel
{
    #region Public and private fields, properties, constructor

    public List<FieldFilterModel>? Filters { get; set; }
    public FieldOrderModel? Order { get; set; }
    public int MaxResults { get; set; }


	public SqlCrudConfigModel() : this(null, null, 0)
    {
        //
    }

    public SqlCrudConfigModel(List<FieldFilterModel>? filters, FieldOrderModel? order, int maxResults)
    {
	    Filters = filters;
        Order = order;
        MaxResults = maxResults;
    }

    #endregion

    #region Public and private methods



    #endregion
}
