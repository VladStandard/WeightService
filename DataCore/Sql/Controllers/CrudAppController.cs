// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using static DataCore.ShareEnums;

namespace DataCore.Sql.Controllers;

public class CrudAppController
{
    #region Public and private fields, properties, constructor

    public DataAccessHelper DataAccess { get; private set; } = DataAccessHelper.Instance;

    #endregion

    #region Constructor and destructor

    public CrudAppController()
    {
        //
    }

    #endregion

    #region Public and private methods

    public AppEntity? GetOrCreateNew(string? appName)
    {
        AppEntity? app = null;
        if (!string.IsNullOrEmpty(appName) && appName is string strName)
        {
            app = DataAccess.Crud.GetEntity<AppEntity>(
                new(new() { new(DbField.Name, DbComparer.Equal, appName),
                    new(DbField.IsMarked, DbComparer.Equal, false),
                }));
            if (app == null || app.EqualsDefault())
            {
                app = new()
                {
                    Name = strName,
                    CreateDt = DateTime.Now,
                    ChangeDt = DateTime.Now,
                    IsMarked = false,
                };
                DataAccess.Crud.SaveEntity(app);
            }
        }
        return app;
    }

    public AppEntity? GetEntity(string? appName)
    {
        AppEntity? app = null;
        if (!string.IsNullOrEmpty(appName) && appName is { })
        {
            app = DataAccess.Crud.GetEntity<AppEntity>(
                new(new() { new(DbField.Name, DbComparer.Equal, appName),
                    new(DbField.IsMarked, DbComparer.Equal, false),
                }));
        }
        return app;
    }

    #endregion
}
