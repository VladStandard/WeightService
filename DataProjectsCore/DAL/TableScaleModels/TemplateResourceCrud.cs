// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore.DAL.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataProjectsCore.DAL.TableScaleModels
{
    public class TemplateResourceCrud : CrudController
    {
        #region Constructor and destructor

        public TemplateResourceCrud(DataAccessEntity dataAccess, ISessionFactory sessionFactory) : base(dataAccess, sessionFactory)
        {
            //
        }

        #endregion

        #region Public and private methods

        public int LoadResource(int id, string name, string description, string type, byte[] imagedata, bool marked = false)
        {
            Dictionary<string, object>? parameters = new()
            {
                { "id", id },
                { "name", name },
                { "description", description },
                { "type", type },
                { "imagedata", imagedata },
                { "marked", marked },
            };
            Console.WriteLine($"LoadResource imagedata.Length: {imagedata.Length}");
            return ExecQueryNative(
                "exec [db_scales].[LoadResourceToDB] :id, :name, :description, :type, :imagedata, :marked", parameters);
        }

        #endregion
    }
}
