//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using DataCore.DAL.Models;
//using NHibernate;
//using System.Collections.Generic;

//namespace DataCore.DAL.TableScaleModels
//{
//    public class TemplateResourceCrud : CrudController
//    {
//        #region Constructor and destructor

//        public TemplateResourceCrud(DataAccessEntity dataAccess, ISessionFactory sessionFactory) : base(dataAccess, sessionFactory)
//        {
//            //
//        }

//        #endregion

//        #region Public and private methods

//        public int LoadResource(long id, string name, string description, string type, byte[] imagedata, bool isMarked = false)
//        {
//            Dictionary<string, object>? parameters = new()
//            {
//                { "id", id },
//                { "name", name },
//                { "description", description },
//                { "type", type },
//                { "imagedata", imagedata },
//                { "marked", isMarked },
//            };
//            return ExecQueryNative(
//                "exec [db_scales].[LoadResourceToDB] :id, :name, :description, :type, :imagedata, :marked", parameters);
//        }

//        #endregion
//    }
//}
