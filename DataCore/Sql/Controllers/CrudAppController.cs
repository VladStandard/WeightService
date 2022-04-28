// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using DataCore.Sql.TableScaleModels;
using System;
using System.Collections.Generic;
using static DataCore.ShareEnums;

namespace DataCore.Sql.Controllers
{
    public class CrudAppController
    {
        #region Public and private fields and properties

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
                    new FieldListEntity(new Dictionary<DbField, object?> {
                        { DbField.Name, appName },
                        { DbField.IsMarked, false },
                    }));
                if (app == null || app.EqualsDefault())
                {
                    app = new AppEntity()
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

        #endregion
    }
}
