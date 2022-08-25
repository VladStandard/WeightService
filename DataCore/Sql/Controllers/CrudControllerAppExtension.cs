// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using static DataCore.ShareEnums;

namespace DataCore.Sql.Controllers;

public static class CrudControllerAppExtension
{
    #region Public and private methods

    public static AppEntity? GetOrCreateNewApp(this CrudController crud, string? appName)
    {
        AppEntity? app = null;
        if (!string.IsNullOrEmpty(appName) && appName is { })
        {
            app = crud.GetEntity<AppEntity>(
                new(new() { new(DbField.Name, DbComparer.Equal, appName),
                    new(DbField.IsMarked, DbComparer.Equal, false),
                }));
            if (app.EqualsDefault())
            {
                app = new()
                {
                    Name = appName,
                    CreateDt = DateTime.Now,
                    ChangeDt = DateTime.Now,
                    IsMarked = false,
                };
                crud.SaveEntity(app);
            }
        }
        return app;
    }

    public static AppEntity? GetApp(this CrudController crud, string? appName)
    {
        AppEntity? app = null;
        if (!string.IsNullOrEmpty(appName) && appName is { })
        {
            app = crud.GetEntity<AppEntity>(
                new(new() { new(DbField.Name, DbComparer.Equal, appName),
                    new(DbField.IsMarked, DbComparer.Equal, false),
                }));
        }
        return app;
    }

    #endregion
}
