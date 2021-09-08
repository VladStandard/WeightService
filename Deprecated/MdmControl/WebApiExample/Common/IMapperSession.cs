// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Linq;
using System.Threading.Tasks;

namespace WebApiExample.Common
{
    public interface IMapperSession
    {
        void BeginTransaction();
        Task Commit();
        Task Rollback();
        void CloseTransaction();
        Task Save(object entity);
        Task Delete(object entity);

        IQueryable<object> Books { get; }
    }
}